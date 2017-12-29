using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TBN
{
    /// <summary>
    /// Implementation is still ongoing
    /// </summary>
    public static class FightManager
    {
        public static Character PlayerOneMain { get; set; }


        public static Character PlayerTwoMain { get; set; }




        public static void Prime(Character a, Character b)
        {
            PlayerOneMain = a;
            PlayerTwoMain = b;
        }


        public static void Frame()
        {
            //Hit Logic
            PlayerOneMain.ApplyStrike();
            PlayerTwoMain.ApplyStrike();

            //Update the input manager
            InputManager.Update();
            
            //Update actions
            PlayerOneMain.UpdateAction();
            PlayerTwoMain.UpdateAction();

            //Move the characters
            PlayerOneMain.Move();
            PlayerTwoMain.Move();

            //Try Attacking
            PlayerTwoMain.Struck = PlayerOneMain.TryAttack(PlayerTwoMain);
            PlayerOneMain.Struck = PlayerTwoMain.TryAttack(PlayerOneMain);

        }

        /// <summary>
        /// Draws all of the characters and projectiles on the screen
        /// </summary>
        /// <param name="sb"></param>
        public static void Draw(SpriteBatch sb)
        {
            PlayerOneMain.Draw(sb);
            PlayerTwoMain.Draw(sb);
        }




    }
}
