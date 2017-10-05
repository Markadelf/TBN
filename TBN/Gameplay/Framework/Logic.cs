using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBN.Gameplay.Framework
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
                if (c.AnchorPoint.Y < height)
                {
                    return true;
                }
                return false;
            };
            
        }
        public static SimpleLogic OnGround(Character character)
        {
            return () => { return character.OnGround; };
        }
        #endregion
    }
}
