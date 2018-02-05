using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TBN
{
    public class SimpleButton : MenuButton
    {
        public static Texture2D SimpleButtonTexture;
        public static SpriteFont SimpleButtonFont;
    
        /// <summary>
        /// The text on the button
        /// </summary>
        public String Text { get; set; }

        /// <summary>
        /// Create a simple button with text
        /// </summary>
        /// <param name="box">The hitbox of the button</param>
        /// <param name="order">The behavior of the button on click</param>
        /// <param name="text">The text on the button</param>
        public SimpleButton(Rectangle box, SimpleBehavior order, String text): base(box, order)
        {
            Text = text;
        }



        /// <summary>
        /// Draws the button and the text on top of it.
        /// </summary>
        /// <param name="sb"></param>
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SimpleButtonTexture, Box, Color.White);
            sb.DrawString(SimpleButtonFont, Text, new Vector2(Box.X, Box.Y), Color.Black);
        }
    }
}
