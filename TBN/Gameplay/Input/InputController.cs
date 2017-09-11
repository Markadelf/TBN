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
        /// Updates the controls
        /// </summary>
        public abstract void Update();



    }
}
