using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TBN
{
    /// <summary>
    /// Implementation is still ongoing
    /// </summary>
    public static class FightManager
    {
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }


        public static Character PlayerOneMain { get; set; }


        public static Character PlayerTwoMain { get; set; }

        static FightManager()
        {
            ScreenWidth = 480;
            ScreenHeight = 360;
        }




        public static void Prime(Character a, Character b)
        {
            PlayerOneMain = a;
            PlayerTwoMain = b;
            PlayerOneMain.Target = PlayerTwoMain;
            PlayerTwoMain.Target = PlayerOneMain;
        }


        public static void Frame()
        {
            //Debug
            PlayerOneMain.Reface();
            PlayerTwoMain.Reface();

            //Update the input manager
            InputManager.Update();

            //Update actions
            PlayerOneMain.UpdateAction();
            PlayerTwoMain.UpdateAction();

            //Hit Logic
            PlayerOneMain.TryApplyStrike();
            PlayerTwoMain.TryApplyStrike();

            //Misc Action Logic
            PlayerOneMain.MiscActions();
            PlayerTwoMain.MiscActions();

            //Move the characters
            PlayerOneMain.Move();
            PlayerTwoMain.Move();

            //Move Projectiles
            PlayerOneMain.MoveProjectiles();
            PlayerTwoMain.MoveProjectiles();

            //Try Attacking
            PlayerTwoMain.Struck = PlayerOneMain.TryAttack(PlayerTwoMain);
            PlayerOneMain.Struck = PlayerTwoMain.TryAttack(PlayerOneMain);

            //Try attacking with projectiles
            PlayerOneMain.ProjectilesTryHit(PlayerTwoMain);
            PlayerTwoMain.ProjectilesTryHit(PlayerOneMain);

        }

        /// <summary>
        /// Draws all of the characters and projectiles on the screen
        /// </summary>
        /// <param name="sb"></param>
        public static void Draw(SpriteBatch sb)
        {
            PlayerOneMain.Draw(sb);
            PlayerTwoMain.Draw(sb);


            sb.Draw(SpriteSheet.WhitePixel, new Rectangle(10, 10, (int)(200 * PlayerOneMain.Health / PlayerOneMain.MaxHealth), 10), Color.Red);
            sb.Draw(SpriteSheet.WhitePixel, new Rectangle(280, 10, (int)(200 * PlayerTwoMain.Health / PlayerTwoMain.MaxHealth), 10), Color.Red);

        }




    }
}
