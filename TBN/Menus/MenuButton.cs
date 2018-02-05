using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TBN
{
    public abstract class MenuButton
    {
        /// <summary>
        /// The box the button has when it is on screen.
        /// </summary>
        public Rectangle Box { get; set; }
        /// <summary>
        /// The order the button executes when it is called.
        /// </summary>
        public SimpleBehavior Order { get; set; }

        /// <summary>
        /// Simple constructor that gives a button everything it needs to operate
        /// </summary>
        /// <param name="box">The hitbox for the button relative to the top left</param>
        /// <param name="order">The function the button executes</param>
        public MenuButton(Rectangle box, SimpleBehavior order)
        {
            Box = box;
            Order = order;
        }

        /// <summary>
        /// Checks if a button contains a point.
        /// </summary>
        /// <param name="x">X axis position</param>
        /// <param name="y">Y axis position</param>
        /// <returns></returns>
        public virtual bool Contains(int x, int y)
        {
            return Box.Contains(x, y);
        }

        /// <summary>
        /// Tells the button to execute its order
        /// </summary>
        public virtual void Execute()
        {
            Order();
        }

        /// <summary>
        /// Tells the button to render a visual representation of itself.
        /// </summary>
        /// <param name="sb">The sprite batch to render to</param>
        public abstract void Draw(SpriteBatch sb);

    }
}
