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
        #region Debug Values
        float MaxHeight = 0;
        #endregion
        #region Modifiers and Multipliers
        public abstract float DamageMultiplier{ get; }
        #endregion

        /// <summary>
        /// keeps track of previous anchor
        /// </summary>
        Vector2 PrevAnchor;
        /// <summary>
        /// used for simple inertia calculation
        /// </summary>
        public Vector2 PreviousMovement;
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
        /// floor height
        /// </summary>
        public int Floor { get; set; }
        /// <summary>
        /// The length in frames that have passed since the action was primed
        /// </summary>
        public int CurrentActionFrame { get; set; }

        /// <summary>
        /// Number of times the action has hit
        /// </summary>
        public int CurrentActionHits { get; set; }

        /// <summary>
        /// Max Height of a jump used by go Idle 
        /// </summary>
        public int JumpHeight { get; set; }
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
            PrevAnchor = anchor;
            PreviousMovement = Vector2.Zero;
            MoveList = new Dictionary<string, Action>();
            Floor = 300;
            AnchorPoint = anchor;
            Input = input;
            MySheet = sheet;
            OnGround = false;
            LastHitLight = false;
            JuggleMeter = 0;
            CurrentActionFrame = 0;
            CurrentActionHits = 0;
            
        }
        /// <summary>
        /// make a set of hitboxes or hurtboxes
        /// 
        /// to get a valid output right now you need to have the same number of ints as rectangles
        /// 
        /// to make a rectangle in "boxcode" use this format "/posx*posy*width*height//posx*posy*width*height/%"
        /// k
        /// basically you start and end every rectangle with '/' and separate parameters by anything but '/' or '%' 
        /// 
        /// you can end the method after finishing a rectangle by inputting
        /// </summary>
        /// <param name="frameoccurances">the frames in which the hitboxes appear in order</param>
        /// <param name="boxcode">hitboxes in order of appearance</param>
        /// <returns></returns>
        public List<Tuple<int, Rectangle[]>> HitboxGenerator(int[] frameoccurances,Rectangle[] rect,int tilesize)
        {

            List<Tuple<int, Rectangle[]>> final = new List<Tuple<int, Rectangle[]>>();
            int u = 0;
            for (int i = 0; i < rect.Length; i++)
            {
                Tuple<int, Rectangle[]> Temp = new Tuple<int, Rectangle[]>(frameoccurances[i], new Rectangle[]{ rect[i] });
                final.Add(Temp);
            }
            
            
            return final;
        }

        /// <summary>
        /// A function that should cause the character to got to the apropriate action post-combo.
        /// Called when CurrentActionFrame >= CurrentAction.EndFrame
        /// </summary>
        public abstract void GoIdle();

        /// <summary>
        /// Called once a frame Handles the current action, inputs and comboing.
        /// </summary>
        public void UpdateAction()
        {
            CurrentActionFrame++;
            Input.Update();
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
                        if ((condition.InputCondition == null || condition.InputCondition(Input)) && (condition.MiscCondition == null || condition.MiscCondition()))
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
            if (CurrentAction.MiscBehaviors != null)
            {
                for (int i = 0; i < CurrentAction.MiscBehaviors.Count; i++)
                {
                    if (CurrentAction.MiscBehaviors[i].Item1 == CurrentActionFrame)
                    {
                        CurrentAction.MiscBehaviors[i].Item2();
                    }
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

                if (CurrentAction.FrameDisplacement[i].Item1 == CurrentActionFrame)
                {
                    AnchorPoint += CurrentAction.FrameDisplacement[i].Item2;

                }
                else if(CurrentAction.FrameDisplacement.Count<1)
                {//apply gravity friction and inertia
                    if (OnGround)
                    {

                        if (Math.Abs(PreviousMovement.X) > 3)//are we going fast enough to slide
                        {
                            //if so truncate the previous movement into something usable
                            Vector2 Movement = new Vector2(PreviousMovement.X / 2, 0);//friction
                            AnchorPoint += Movement;//add the movement


                        }

                    }
                    else
                    {
                        Vector2 Movement = new Vector2(PreviousMovement.X, PreviousMovement.Y + 1f);//gravity 
                        AnchorPoint += Movement;//add to movement
                    }
                }
               
            }
            if (1 > CurrentAction.FrameDisplacement.Count)
            {
                if (OnGround)
                {

                    if (Math.Abs(PreviousMovement.X) > 3)//are we going fast enough to slide
                    {
                        //if so truncate the previous movement into something usable
                        Vector2 Movement = new Vector2(PreviousMovement.X / 2, 0);//friction
                        AnchorPoint += Movement;//add the movement


                    }

                }
                else
                {
                    Vector2 Movement = new Vector2(PreviousMovement.X, PreviousMovement.Y + 1f);//gravity 
                    AnchorPoint += Movement;//add to movement
                }
            }
            if (AnchorPoint.Y >= Floor)
            {
                OnGround = true;
                AnchorPoint = new Vector2(AnchorPoint.X, Floor);
            }
            PreviousMovement = AnchorPoint -PrevAnchor;
            PrevAnchor = AnchorPoint;
            #endregion
        }



        public virtual void Debug(SpriteBatch sb)
        {
            Texture2D tex = MySheet.Sheet;
            string s = "";
            for(int i = 9;i<Input.InputHistory.Length;i++)
            {
                if (Input.InputHistory[i] <= 3)
                {
                    s += i + " ";
                }
                
            }
            if (AnchorPoint.Y < 300 - MaxHeight)
            {
                MaxHeight = 300 - AnchorPoint.Y;
            }
            sb.Draw(tex,new Rectangle(20 + (int)Input.StickPos.X * 10, 20 + -10* (int)Input.StickPos.Y, 10, 10), color: Color.Black);
            sb.DrawString(Game1.Font, "Anchor Point "+AnchorPoint.ToString()+"\nButton pushed "+ s + "\nMax Height " + MaxHeight + "\nIntended Height" + (300-JumpHeight), new Vector2(40, 40), Color.Black);
        }
        public virtual void Draw(SpriteBatch sb)
        {
            FrameDrawInfo drawInfo = MySheet.FrameInfo[CurrentAction.ActionId][CurrentActionFrame];


            sb.Draw(MySheet.Sheet,
                AnchorPoint,
                drawInfo.SourceRectangle,
                Color.White,
                0f,
                new Vector2(),
               // new Vector2(16+drawInfo.SourceRectangle.Width*CurrentActionFrame,32+drawInfo.SourceRectangle.Height*CurrentAction.ActionId),
                1.0f,
                SpriteEffects.None,
                0);
            DrawLiterals(sb);
            /* sb.Draw(MySheet.Sheet,
                position: AnchorPoint,
                sourceRectangle: drawInfo.SourceRectangle,
                origin: drawInfo.Origin); */
        }

        /// <summary>
        /// Draws all of the hitboxes and hurtboxes.
        /// </summary>
        /// <param name="sb"></param>
        public virtual void DrawLiterals(SpriteBatch sb)
        {
            Rectangle[] hit = GetCurrentHitboxs();
            for (int i = 0; i < hit.Length; i++)
            {
                sb.Draw(SpriteSheet.WhitePixel,
                    hit[i],
                    Color.Blue);
            }
            Rectangle[] hurt = GetCurrentHurtboxs();
            for (int i = 0; i < hurt.Length; i++)
            {
                sb.Draw(SpriteSheet.WhitePixel,
                    hurt[i],
                    Color.Red);
            }
        }

        /// <summary>
        /// Gets the current Hitboxes in World Space
        /// </summary>
        /// <returns></returns>
        public Rectangle[] GetCurrentHitboxs()
        {
            int place = -1;
            for(int i = 0; i < CurrentAction.Hitboxes.Count; i++)
            {
                if(CurrentAction.Hitboxes[i].Item1 <= CurrentActionFrame)
                {
                    place = i;
                }
                else
                {
                    break;
                }
            }


            if (place == -1)
            {
                return new Rectangle[] { };

            }
            Rectangle[] ret = new Rectangle[(CurrentAction.Hitboxes[place].Item2).Length]; ;
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = ConvertToWorldSpace(AnchorPoint, CurrentAction.Hitboxes[place].Item2[i]);
            }
            return ret;
        }

        /// <summary>
        /// Gets the current Hurtboxes in World Space
        /// </summary>
        /// <returns></returns>
        public Rectangle[] GetCurrentHurtboxs()
        {
            int place = -1;
            for (int i = 0; i < CurrentAction.Hurtboxes.Count; i++)
            {
                if (CurrentAction.Hurtboxes[i].Item1 <= CurrentActionFrame)
                {
                    place = i;
                }
                else
                {
                    break;
                }
            }


            if (place == -1)
            {
                return new Rectangle[] { };

            }
            Rectangle[] ret = new Rectangle[(CurrentAction.Hurtboxes[place].Item2).Length]; ;
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = ConvertToWorldSpace(AnchorPoint, CurrentAction.Hurtboxes[place].Item2[i]);
            }
            return ret;
        }

        static Rectangle ConvertToWorldSpace(Vector2 anchor, Rectangle rect)
        {
            rect.X += (int)anchor.X;
            rect.Y += (int)anchor.Y;
            return rect;
        }

        static Rectangle ConvertToAnchorSpace(Vector2 anchor, Rectangle rect)
        {
            rect.X -= (int)anchor.X;
            rect.Y -= (int)anchor.Y;
            return rect;
        }

    }
}
