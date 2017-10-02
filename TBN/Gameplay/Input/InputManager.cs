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






        #region input methods

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
