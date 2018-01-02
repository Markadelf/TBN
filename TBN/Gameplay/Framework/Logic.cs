using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBN
{
    class Logic
    {
        #region simple logic
        public static SimpleLogic None()
        {
           
                return ()=>true;
            
        }
        public static SimpleLogic UnderHeight(Character c, int height)
        {
            return () =>
            {
                return (c.AnchorPoint.Y < height);
            }; 
        }
        public static SimpleLogic OverHeight(Character c, int height)
        {
            return () =>
            {
                return (c.AnchorPoint.Y > height);
            };
        }
        public static SimpleLogic UnderJumpHeight(Character c)
        {
            return () =>
            {
                return (c.AnchorPoint.Y > c.JumpHeight);
            };
        }
        public static SimpleLogic OverJumpHeight(Character c)
        {
            return () =>
            {
                return (c.AnchorPoint.Y < c.JumpHeight);
            };
        }
        public static SimpleLogic OnGround(Character character)
        {
            return () => { return character.OnGround; };
        }
        #endregion
    }
}
