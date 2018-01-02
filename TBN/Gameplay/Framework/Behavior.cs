using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBN
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
                List<Tuple<int, Vector2>> k = character.CurrentAction.FrameDisplacement;
                for (int i = 0;i<k.Count;i++)
                {
                    k[i] =new Tuple<int,Vector2> (k[i].Item1,new Vector2(character.Input.GetStickPos().X * speed, k[i].Item2.Y));
                }
            };
        }
        #endregion
    }
}
