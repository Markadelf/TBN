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
            Menus[MenuType.Main].Buttons.Add(new SimpleButton(new Microsoft.Xna.Framework.Rectangle(0, 0, 100, 100), StartGame, "Start"));
            Active = Menus[MenuType.Main];
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
        public static void UnPause()
        {
            Game1.menu = false;
        }
        public static void Pause()
        {
            Game1.menu = true;
        }
        public static void StartGame()
        {
            Game1.menu = false;
        }

    }
}
