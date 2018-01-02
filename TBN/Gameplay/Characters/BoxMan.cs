using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Threading;

namespace TBN
{



    class BoxMan : StandardCharacter
    {
        public override float DamageMultiplier
        {
            get { return 1; }
        }

        Point TopLeft(int x, int y)
        {
            return new Point(x + (int)AnchorPoint.X - 16, y + (int)AnchorPoint.Y - 32);

        }

        public BoxMan(Vector2 anchor, InputController input, SpriteSheet sheet) : base(anchor, input, sheet)
        {





        }
    }
}
