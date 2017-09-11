using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TBN
{
    public class Character
    {
        public Vector2 AnchorPoint { get; set; }
        public InputController Input { get; set; }
        public bool LastHitLight { get; set; }
        public int JuggleMeter { get; set; }


        


    }
}
