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
            previous = current;
            current = Keyboard.GetState();
            StickPos = new Vector2(
                /*start of vector definition*/
                
                /*start of x coord definition*/
                (Keyboard.GetState().IsKeyDown(InpCodes[0])) ? //is the player pressing left
                (Keyboard.GetState().IsKeyDown(InpCodes[3]) ? //if true check if the player is pressing right
                0 : -1) //here pressing right gives you a 0 for x location and a -1 otherwise 
                : (Keyboard.GetState().IsKeyDown(InpCodes[3]) ?//if false we check if the player is pressing right with other outcomes
                1 : 0) //here pushing right gives you a 1 for x location and a 0 otherwise
                /*end of x coord definition*/
                ,
                /*start of y coord definition*/
                (Keyboard.GetState().IsKeyDown(InpCodes[1])) ?//checks if we are pressing down
                (Keyboard.GetState().IsKeyDown(InpCodes[4]) ?//if true check if the player is pressing up 
                0 : -1)//pressing up sets y to 0 and sets it to -1 otherwise.
                : ((Keyboard.GetState().IsKeyDown(InpCodes[4]) ?//if false check if the player is pressing up with alternate outcomes. 
                1 : 0)//pressing up sets y to 1 and sets it to 0 otherwise.
                /*end of y coordinate*/
                )
                /*end of vector definition*/
                );
            
            for (int i = 0; i < InputHistory.Length; i++)
            {
                if (i < 9)
                {
                    int NotationNum = i + 1;//just a number i use to convert to a notation usable for y axis calculation using integer division
                    InputHistory[i] = (StickPos == new Vector2(((i % 3) - 1), ((NotationNum / 3)-2))) ? //compares the stickpos to the stickposition required generated mathematically
                        0 : (InputHistory[i] < 60) ? //if we meet the position return 0 and if not we check if the value has hit 60 yet
                        InputHistory[i] + 1 : 60; //if the value is 60 it stays 60 and if not it gets added to
                }
                else
                {
                    InputHistory[i] = (ButtonPressed(i)) ? //is the key pressed 
                    0 : (InputHistory[i] < 60) ?//if so set it to 0 and if not check if it is less than 60
                    InputHistory[i] + 1 : 60;//if so add one to the value if not it remains at 60
                }
            }
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
