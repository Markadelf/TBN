using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBN.Gameplay.Framework
{
    public static class Behavior
    {
        #region simple behavior
        public static SimpleBehavior SetJumpHeight(Character character,int height)
        {
            return () =>
            {
                character.OnGround = false;
                character.JumpHeight = (int)character.AnchorPoint.Y - height;
            };
        }
        /// <summary>
        /// this is a behavior that sets OnGround to false
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static SimpleBehavior OffGround(Character character)
        {
            return () =>
            {
                character.OnGround = false;
            };
        }
        public static SimpleBehavior FreeMoveX(Character character,int speed)
        {
            return () =>
            {
                character.AnchorPoint = new Vector2(character.AnchorPoint.X+speed*character.Input.StickPos.X,character.AnchorPoint.Y);
            };
        }
        #endregion
    }
}
