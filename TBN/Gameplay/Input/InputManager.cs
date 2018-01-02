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




        public static InputLogic GenerateLogic(int buttonIndex, int buffer)
        {
            int index = buttonIndex;
            int buff = buffer;
            return (InputController control) => { return control.GetInput((InputButton)index) < buff; };
        }

        public static InputLogic GenerateLogic(InputButton button, int buffer)
        {
            InputButton index = button;
            int buff = buffer;
            return (InputController control) => { return control.GetInput(index) < buff; };
        }

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
            return (InputController control) => { return control.GetInput((InputButton)index) < buff; };
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
            return control.GetInput(InputButton.Light) < 3;
        }
        public static InputLogic And(InputLogic a, InputLogic b, InputLogic c = null, InputLogic d = null)
        {
            return (InputController control) => { return a(control) && b(control) && (c == null || c(control)) && (d == null || d(control)); };
        }
        public static InputLogic Or(InputLogic a, InputLogic b, InputLogic c = null, InputLogic d = null)
        {
            return (InputController control) => { return a(control) || b(control) || (c != null && c(control)) || (d != null && d(control)); };
        }
        public static InputLogic Not(InputLogic a)
        {
            return (InputController control) => { return !a(control); };
        }


        #endregion


    }
}
