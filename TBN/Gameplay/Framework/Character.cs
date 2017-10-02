using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TBN
{
    public enum JuggleState
    {
        StageZero,
        StageOne,
        StageTwo,
        StageThree,
        StageFour
    }
    public abstract class Character
    {
        #region Modifiers and Multipliers
        protected float DamageMultiplier { get; private set; }
        #endregion



        /// <summary>
        /// The point that is moved when the character is moved. Corresponds to the origin on the draw information.
        /// </summary>
        public Vector2 AnchorPoint { get; set; }
        /// <summary>
        /// The input controller that controls this character
        /// </summary>
        public InputController Input { get; set; }
        /// <summary>
        /// The spritesheet used to draw this character
        /// </summary>
        public SpriteSheet MySheet { get; set; }
        /// <summary>
        /// A list of all the projectiles belonging to this character.
        /// </summary>
        public List<Projectile> MyProjectiles { get; set; }

        /// <summary>
        /// this will be what we use to display the moves in a list and also helps when writing the individual character's classes
        /// </summary>
        public Dictionary<String, Action> MoveList;
        /// <summary>
        /// The current action the player is in
        /// </summary>
        public Action CurrentAction { get; set; }
        /// <summary>
        /// The length in frames that have passed since the action was primed
        /// </summary>
        public int CurrentActionFrame { get; set; }
        /// <summary>
        /// Number of times the action has hit
        /// </summary>
        public int CurrentActionHits { get; set; }

        /// <summary>
        /// True if the last hit recieved was a light hit
        /// </summary>
        public bool LastHitLight { get; set; }

        /// <summary>
        /// returns true if the character is on the ground
        /// </summary>
        public bool OnGround { get; set; }

        /// <summary>
        /// NOT YET IMPLEMENTED
        /// </summary>
        public JuggleState CurrentJuggleState {
            get
            {
                return JuggleState.StageZero;
            }
        }
        /// <summary>
        /// The value that directly influences the Juggle state.
        /// </summary>
        public int JuggleMeter { get; set; }


        public Character(Vector2 anchor, InputController input, SpriteSheet sheet)
        {
            AnchorPoint = anchor;
            Input = input;
            MySheet = sheet;
            OnGround = false;
            LastHitLight = false;
            JuggleMeter = 0;
            CurrentActionFrame = 0;
            CurrentActionHits = 0;
            GoIdle();
        }
        /// <summary>
        /// make a set of hitboxes or hurtboxes
        /// 
        /// to get a valid output right now you need to have the same number of ints as rectangles
        /// 
        /// to make a rectangle in "boxcode" use this format "/posx*posy*width*height//posx*posy*width*height/%"
        /// 
        /// basically you start and end every rectangle with '/' and separate parameters by anything but '/' or '%' 
        /// 
        /// you can end the method after finishing a rectangle by inputting
        /// </summary>
        /// <param name="frameoccurances">the frames in which the hitboxes appear in order</param>
        /// <param name="boxcode">hitboxes in order of appearance</param>
        /// <returns></returns>
        public List<Tuple<int, Rectangle>> HitboxGenerator(int[] frameoccurances,string boxcode,int tilesize)
        {

            List<Tuple<int, Rectangle>> final = new List<Tuple<int, Rectangle>>();
            int u = 0;
            Tuple<int, Rectangle> Temp;
            for ( int i = 0; i < boxcode.Length; i++)
            {
                
                if (boxcode[i] == '/')
                {
                    List<int> Values = new List<int>();
                    for (i++; i < boxcode.Length; i++)
                    {
                        
                        int number=0;
                        if ("1234567890".Contains(boxcode[i]))
                        {
                            int startpoint = i;
                            int endpoint = 0;
                            for (i++; i < boxcode.Length; i++)
                            {
                                if (!("1234567890".Contains(boxcode[i])))
                                {
                                    endpoint = i;
                                    break;
                                }
                            }
                            int.TryParse(boxcode.Substring(startpoint, (endpoint - startpoint)), out number);
                            Values.Add(number);
                        }
                        else if ('/' == boxcode[i])
                        {
                            break;
                        }
                    }
                    Temp = new Tuple<int, Rectangle>(frameoccurances[u], new Rectangle((int)AnchorPoint.X-tilesize/2+Values[0], (int)AnchorPoint.Y - tilesize + Values[1], Values[2], Values[3]));
                    u++;
                    final.Add(Temp);
                }
                if (boxcode[i] == '%')
                {
                    break;
                }
            }
            return final;
        }

        /// <summary>
        /// A function that should cause the character to got to the apropriate action post-combo.
        /// Called when CurrentActionFrame >= CurrentAction.EndFrame
        /// </summary>
        public abstract void GoIdle();

        /// <summary>
        /// Called once a frame.Handles the current action, inputs and comboing.
        /// </summary>
        public void UpdateAction()
        {
            CurrentActionFrame++;

            #region ActionTransition
            //Check if we are moving to a new Action
            for(int i = 0; i < CurrentAction.ComboList.Count; i++)
            {
                ActionCondition condition = CurrentAction.ComboList[i].Item1;
                //Have we entered into the first possible frame for this yet?
                if(condition.FirstFrame <= CurrentActionFrame)
                {
                    //Did we fufill our hit obligations and are we before the last frame
                    if ((!condition.MustHit) || (CurrentActionHits > 0) && condition.LastFrame >= CurrentActionFrame)
                    {
                        //Has the input been entered / other?
                        if (condition.InputCondition(Input) && condition.MiscCondition())
                        {
                            CurrentActionFrame = 0;
                            CurrentActionHits = 0;
                            CurrentAction = CurrentAction.ComboList[i].Item2;
                        }
                    }
                }
                else
                {
                    //This break will only function if the Action Transitions are sorted.
                    break;
                }
            }

            //If we fell out of the combo, Go to the apropriate action idle
            if(CurrentAction.EndFrame <= CurrentActionFrame)
            {
                CurrentActionFrame = 0;
                CurrentActionHits = 0;
                GoIdle();
            }

            #endregion

            #region MiscBehaviors
            //Check if there are any misc behaviors for this frame
            for (int i = 0; i < CurrentAction.MiscBehaviors.Count; i++)
            {
                if(CurrentAction.MiscBehaviors[i].Item1 == CurrentActionFrame)
                {
                    CurrentAction.MiscBehaviors[i].Item2();
                }
                if (CurrentAction.MiscBehaviors[i].Item1 > CurrentActionFrame)
                {
                    break;
                }
            }



            #endregion
        }

        /// <summary>
        /// Move the character this frame
        /// </summary>
        public void Move()
        {
            #region Movement
            //Check if there are any misc behaviors for this frame
            for (int i = 0; i < CurrentAction.FrameDisplacement.Count; i++)
            {
                if (CurrentAction.FrameDisplacement[i].Item1 < CurrentActionFrame && 
                    (i == CurrentAction.FrameDisplacement.Count || CurrentAction.FrameDisplacement[i].Item1 > CurrentActionFrame))
                {
                    AnchorPoint += CurrentAction.FrameDisplacement[i].Item2;
                }
            }
            #endregion
        }




        public virtual void Draw(SpriteBatch sb)
        {
            FrameDrawInfo drawInfo = MySheet.FrameInfo[CurrentAction.ActionId][CurrentActionFrame];
            sb.Draw(MySheet.Sheet, position: AnchorPoint, sourceRectangle: drawInfo.SourceRectangle, origin: drawInfo.Origin);
        }



    }
}
