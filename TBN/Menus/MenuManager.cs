using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBN
{
    /// <summary>
    /// Each menu should have its own type.
    /// </summary>
    public enum MenuType
    {
        Main,
        CharacterSelect,
        Pause,

    }

    public static class MenuManager
    {
        public static Dictionary<MenuType, Menu> Menus { get; set; }

        public static Menu Active { get; set; }

        static MenuManager()
        {
            Menus = new Dictionary<MenuType, Menu>();

            //Add a main menu
            Menus.Add(MenuType.Main, new Menu());
        }

        public static void Update()
        {
            Active.Update();
        }

        public static void Draw(SpriteBatch sb)
        {
            Active.Draw(sb);
        }



        public static SimpleBehavior SetMenu(MenuType menu)
        {
            return () => { Active = Menus[menu]; };
        }
        public static SimpleBehavior UnPause()
        {
            return () => { Game1.menu = false; };
        }
        public static SimpleBehavior Pause()
        {
            return () => { Game1.menu = true; };
        }
        public static SimpleBehavior StartGame()
        {
            return () => { Game1.menu = false; };
        }

    }
}
