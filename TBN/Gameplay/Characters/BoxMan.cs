using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TBN.Gameplay.Characters
{
    class BoxMan : Character
    {
        

        public BoxMan(Vector2 anchor, InputController input, SpriteSheet sheet) : base(anchor, input, sheet)
        {
           
           // MoveList.Add("Idle",new Action(2,0,0,new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] {1,2},"/0*0*32*32//0*0*32*32/%",32),new List<Tuple<int, Vector2>>(), 0,new List<Tuple<ActionCondition, ));
        }

        public override void GoIdle()
        {
            if (OnGround)
            {
                CurrentAction.ActionId = 0;
                CurrentActionFrame = 0;
            }
           
        }

    }
}
