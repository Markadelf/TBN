using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace TBN
{
    public enum InputButton
    {
        DownBack = 0,
        Down = 1,
        DownForward = 2,
        Back = 3,
        Neutral = 4,
        Forward = 5,
        UpBack = 6,
        Up = 7,
        UpForward = 8,
        Light = 9,
        Medium = 10,
        Heavy = 11,
        Special = 12,
        Short = 13,
        RoundHouse = 14,
        Start = 15,
        Select = 16
    }
    public abstract class InputController
    {
        /// <summary>
        /// This lets the controller know if it should flip the stick
        /// </summary>
        public bool FaceRight { get; set; }
        /// <summary>
        /// Should always be between -1 and 1 on both axis.
        /// </summary>
        public Vector2 StickPos { get; set; }
        /// <summary>
        /// As Stick Pos but is flipped if facing left
        /// </summary>
        /// <returns></returns>
        public Vector2 GetStickPos()
        {
            if (FaceRight)
                return StickPos;
            else
            {
                Vector2 temp = StickPos;
                temp.X *= -1;
                return temp;
            }
        }

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
        /// As InputHistory but is flipped if facing left
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int GetInput(InputButton input)
        {
            if (!FaceRight)
            {
                if (input == InputButton.DownBack)
                    input = InputButton.DownForward;
                else if (input == InputButton.Back)
                    input = InputButton.Forward;
                else if (input == InputButton.UpBack)
                    input = InputButton.UpForward;
                else if (input == InputButton.DownForward)
                    input = InputButton.DownBack;
                else if (input == InputButton.Forward)
                    input = InputButton.Back;
                else if (input == InputButton.UpForward)
                    input = InputButton.UpBack;
            }
            return InputHistory[(int)input];
        }

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
            FaceRight = true;
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
