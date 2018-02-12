using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TBN
{
    class GamepadController : InputController
    {
        /// <summary>
        /// reads entries as left, down, right, up, light punch, medium punch, heavy punch, special, short, roundhouse,  
        /// </summary>
        Buttons[] InpCodes;
        float x;
        float y;
        GamePadState previous;
        GamePadState current;
        int index;

        public GamepadController(int gamePadIndex)
        {
            GamePadCapabilities gamepad = GamePad.GetCapabilities(gamePadIndex);
            index = gamePadIndex;
            //Ill need to talk with someone about how to implement this
        }

        public override bool ButtonHeld(int HistoryLocation)
        {
            return current.IsButtonDown(InpCodes[HistoryLocation]);
        }

        public override bool ButtonPressed(int HistoryLocation)
        {
            return current.IsButtonDown(InpCodes[HistoryLocation]) && previous.IsButtonUp(InpCodes[HistoryLocation]);
        }

        public override void RemapControls()
        {
            //throw new NotImplementedException();
        }

        public override void Update()
        {
            current = GamePad.GetState(index);
            StickPos = current.ThumbSticks.Left;

            for (int i = 0; i < InputHistory.Length; i++)
            {
                if (i < 9)
                {
                    InputHistory[i]++;
                    int stickindex = (int)(4 + StickPos.X + 3 * StickPos.Y);
                    InputHistory[stickindex] = 0;

                }
                else
                {
                    InputHistory[i] = (ButtonPressed(i)) ? //is the key pressed 
                    0 : //if so set it to 0 
                    InputHistory[i] + 1;//if not add one to the value if not it remains at 60
                }
            }
            previous = current;
        }
    }
}
