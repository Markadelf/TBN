using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBN
{
    public delegate bool SimpleLogic();
    public delegate bool InputLogic(InputController control);
    public class ActionCondition
    {
        /// <summary>
        /// The first frame permitted 
        /// Inclusive
        /// </summary>
        public int FirstFrame { get; set; }
        /// <summary>
        /// The last frame permitted 
        /// Inclusive
        /// </summary>
        public int LastFrame { get; set; }
        /// <summary>
        /// True if a hit must connect in order to enter this move
        /// </summary>
        public bool MustHit { get; set; }
        /// <summary>
        /// True if the input for the move has been entered. Most of these will use static methods from InputManager
        /// Null will presume true
        /// </summary>
        public InputLogic InputCondition { get; set; }
        /// <summary>
        /// True if the move should combo
        /// Null will presume true
        /// </summary>
        public SimpleLogic MiscCondition { get; set; }


        public ActionCondition(int first, int last, bool mustHit, InputLogic input, SimpleLogic misc)
        {
            FirstFrame = first;
            LastFrame = last;
            MustHit = mustHit;
            InputCondition = input;
            MiscCondition = misc;
        }

        /// <summary>
        /// Evaluates the condition
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool Evaluate(Character c)
        {
            return(
                    //Have we entered into the first possible frame for this yet?
                    (FirstFrame <= c.CurrentActionFrame) &&
                    //Did we fufill our hit obligations and are we before the last frame
                    ((!MustHit) || (c.CurrentActionHits > 0) && LastFrame >= c.CurrentActionFrame) &&
                    //Has the input been entered / misc condition met?
                    ((InputCondition == null || InputCondition(c.Input)) && (MiscCondition == null || MiscCondition()))
                );
        }
        


    }
}
