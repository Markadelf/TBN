using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TBN.Gameplay.Input
{
    class KeyboardController : InputController
    {
        /// <summary>
        /// reads entries as left, down, right, up, light punch, medium punch, heavy punch, special, short, roundhouse,  
        /// </summary>
        Keys[] InpCodes;
        /// <summary>
        /// If true, the Input Manager will stop updating the controller
        /// </summary>
        public override bool Dispose
        {
            get; set;
        }
        /// <summary>
        /// keeps track of the history of inputs on the controller
        ///0-8 are stick inputs
        ///9, 10, and 11 are light medium and heavy punch respectively
        ///12, 13, and 14 are special short and roundhouse respectively
        ///15 and 16 are start and select
        /// </summary>
        public override int[] InputHistory
        {
            get; set;   
        }
        /// <summary>
        /// keeps track of the current stick position
        /// </summary>
        public override Vector2 StickPos
        {
            get; set;
        }
        /// <summary>
        /// constructor...
        /// </summary>
        public KeyboardController()
        {
            InpCodes = new Keys[] {Keys.A, Keys.S, Keys.D, Keys.W, Keys.I, Keys.O, Keys.P, Keys.J, Keys.K, Keys.L, Keys.Enter,Keys.RightShift };

            InputManager.ActiveInputs.Add(this);
            Dispose = false;
            StickPos = new Vector2(0, 0);
            InputHistory = new int[17];
            for (int i = 0; i < InputHistory.Length; i++)
            {
                InputHistory[i] = 60;
            }
        }
        public override void Update()
        {
            for (int i = 0; i < InputHistory.Length; i++)
            {
                
            }
        }
    }
}
