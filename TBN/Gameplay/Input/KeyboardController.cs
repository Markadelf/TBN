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
        float x;
        float y;
        KeyboardState previous;
        KeyboardState current;
       
       
        
        /// <summary>
        /// constructor...
        /// </summary>
        public KeyboardController()
        {
            InpCodes = new Keys[] {Keys.A, Keys.S, Keys.D, Keys.W, Keys.I, Keys.O, Keys.P, Keys.J, Keys.K, Keys.L, Keys.Enter,Keys.RightShift };
            current = Keyboard.GetState();
            previous = current;
            InputManager.ActiveInputs.Add(this);
            Dispose = false;
            StickPos = new Vector2(0, 0);
            InputHistory = new int[17];
            for (int i = 0; i < InputHistory.Length; i++)
            {
                InputHistory[i] = 60;
            }
        }
        public override void RemapControls()
        {
            throw new NotImplementedException();
        }
        
        public override void Update()
        {
            
            current = Keyboard.GetState();
            x = 0;
            y = 0;
            if (current.IsKeyDown(InpCodes[0]))
            {
                x--;
            }
            if (current.IsKeyDown(InpCodes[2]))
            {
                x++;
            }
            if (current.IsKeyDown(InpCodes[3]))
            {
                y++;
            }
            if (current.IsKeyDown(InpCodes[1]))
            {
                y--;
            }
            StickPos = new Vector2(x,y);
            
            for (int i = 0; i < InputHistory.Length; i++)
            {
                if (i < 8)
                {
                    InputHistory[i]++;
                    int stickindex = (int)(4 + StickPos.X + 3 * StickPos.Y);
                    InputHistory[stickindex] = 0;
                    
                }
                else
                {
                    InputHistory[i] = (ButtonPressed(i)) ? //is the key pressed 
                    0 : //if so set it to 0 
                    InputHistory[i] + 1;//if so add one to the value if not it remains at 60
                }
            }
            previous = current;
        }
        /// <summary>
        /// returns true if the specified button is held.
        /// </summary>
        /// <param name="HistoryLocation">location in InputHistory (see Input/InputController)</param>
        /// <returns></returns>
        public override bool ButtonHeld(int HistoryLocation)
        {
            if(HistoryLocation>8)
            return current.IsKeyDown(InpCodes[HistoryLocation - 5]);

            return InputHistory[HistoryLocation] == 0;
        }
        /// <summary>
        /// returns true if the specified button is pressed.
        /// </summary>
        /// <param name="HistoryLocation">location in InputHistory (see Input/InputController)</param>
        /// <returns></returns>
        public override bool ButtonPressed(int HistoryLocation)
        {
            if (current.IsKeyDown(InpCodes[HistoryLocation - 5]))
            {
                return !(previous.IsKeyDown(InpCodes[HistoryLocation - 5]));
            }
            return false;
        }
    }
}
