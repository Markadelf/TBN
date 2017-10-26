using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TBN.Gameplay.Input
{
    class GamepadController : InputController
    {


        public GamepadController(int gamePadIndex)
        {
            GamePadCapabilities gamepad = GamePad.GetCapabilities(gamePadIndex);

            //Ill need to talk with someone about how to implement this
        }

        public override bool ButtonHeld(int HistoryLocation)
        {
            return false;
        }

        public override bool ButtonPressed(int HistoryLocation)
        {
            return false;
        }

        public override void RemapControls()
        {
            //throw new NotImplementedException();
        }

        public override void Update()
        {
            //throw new NotImplementedException();
        }
    }
}
