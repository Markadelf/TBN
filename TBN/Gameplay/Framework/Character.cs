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
            LastHitLight = false;
            JuggleMeter = 0;
            CurrentActionFrame = 0;
            CurrentActionHits = 0;
            GoIdle();
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

        public void Move()
        {

        }


        public void Draw(SpriteBatch sb)
        {

        }



    }
}
