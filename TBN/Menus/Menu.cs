using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TBN
{
    public class Menu
    {
        /// <summary>
        /// A list of all the buttons this menu has
        /// </summary>
        public List<MenuButton> Buttons { get; set; }

        private MouseState _last;

        /// <summary>
        /// Sets up a basic menu
        /// </summary>
        public Menu()
        {
            Buttons = new List<MenuButton>();
            _last = Mouse.GetState();
        }

        /// <summary>
        /// Updates the menu. Checks for button events
        /// </summary>
        public void Update()
        {
            MouseState current = Mouse.GetState();
            //Check if a click event is present
            if (current.LeftButton == ButtonState.Pressed && _last.LeftButton == ButtonState.Released)
            {
                //Check if a button has been clicked on
                for (int i = 0; i < Buttons.Capacity; i++)
                {
                    if (Buttons[i].Contains(current.X, current.Y))
                    {
                        Buttons[i].Execute();
                    }
                }
            }
            _last = current;
        }

        /// <summary>
        /// Draws the menu
        /// </summary>
        /// <param name="sb">The sprite batch to draw on</param>
        public void Draw(SpriteBatch sb)
        {
            //Draw all the buttons
            for (int i = 0; i < Buttons.Capacity; i++)
            {
                Buttons[i].Draw(sb);
            }
        }

    }
}
