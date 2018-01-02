using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TBN
{
    /// <summary>
    /// A controller that produces no input
    /// </summary>
    class DummyController : InputController
    {
        /// <summary>
        /// Creates a controller that produces no input
        /// </summary>
        public DummyController()
        {
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
        }

        
        public override void Update()
        {
        }
    }
}
