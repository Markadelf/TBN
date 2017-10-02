using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace TBN
{
    public abstract class InputController
    {
        
        /// <summary>
        /// Should always be between -1 and 1 on both axis.
        /// </summary>
        public Vector2 StickPos { get; set; }

        /// <summary>
        /// The History of inputs on this controller
        /// 
        /// denoted as such
        /// 
        /// joystick
        /// 678
        /// 345 where 4 is the sticks resting position
        /// 012
        /// 
        /// buttons
        ///         15 16 start/select
        /// 9 10 11 Light/Medium/Heavy
        /// 12 13 14 Special/Short/RoundHouse
        /// </summary>
        public int[] InputHistory { get; set; }

        /// <summary>
        /// If true, the Input Manager will stop updating the controller
        /// </summary>
        public bool Dispose { get; set; }


        public InputController()
        {
            InputManager.ActiveInputs.Add(this);
            Dispose = false;
            StickPos = new Vector2(0, 0);
            InputHistory = new int[17];
            for(int i = 0; i < InputHistory.Length; i++)
            {
                InputHistory[i] = 100;
            }
        }
        /// <summary>
        /// Returns true if the button at the InputHistory index is being held
        /// </summary>
        /// <returns></returns>
        public abstract bool ButtonHeld(int HistoryLocation);
        /// <summary>
        /// Returns true if the button at the InputHistory index is being held
        /// </summary>
        /// <returns></returns>
        public abstract bool ButtonPressed(int HistoryLocation);
        /// <summary>
        /// Updates the controls
        /// </summary>
        public abstract void Update();
        /// <summary>
        /// remaps the controls
        /// </summary>
        public abstract void RemapControls();

    }
}
