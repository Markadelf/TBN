using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBN
{
    public static class InputManager
    {
        /// <summary>
        /// A list of all active inputs.
        /// </summary>
        public static List<InputController> ActiveInputs { get; set; }

        static InputManager()
        {
            ActiveInputs = new List<InputController>();
        }

        /// <summary>
        /// Updates all active inputs and removes disposed inputs
        /// </summary>
        public static void Update()
        {
            for(int i = 0; i < ActiveInputs.Count; i++)
            {
                if (ActiveInputs[i].Dispose)
                {
                    ActiveInputs.RemoveAt(i);
                    i--;
                }
                else
                {
                    ActiveInputs[i].Update();
                }
            }
        }





        #region simple logic
        public static SimpleLogic None()
        {
            return () 
                =>
            {
                return true;
            };
        }
        public static SimpleLogic OnGround(Character character)
        {
            return () => { return character.OnGround; };
        }
        #endregion
        #region input methods
        /// <summary>
        /// Generates an Input logic delegate from parameters.
        /// I'm only 90% sure this won't blow up horribly.
        /// </summary>
        /// <param name="buttonIndex">Index of the button.</param>
        /// <param name="buffer">Amount of acceptable buffer.</param>
        /// <returns>Your new input condition.</returns>
        public static InputLogic GenerateLogic(InputController ctrl,int buttonIndex, int buffer)
        {
            int index = buttonIndex;
            int buff = buffer;
            return (InputController control) => { return ctrl.InputHistory[index] < buff; };
        }
        public static InputLogic Yes()
        {
            return (InputController control) => true;
        }
        /// <summary>
        /// Check if light has been pressed in the last three frames
        /// </summary>
        public static bool Light(InputController control)
        {
            return control.InputHistory[9] < 3;
        }

        


        #endregion


    }
}
