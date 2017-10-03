using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TBN.Gameplay.Framework;
using System.Threading;

namespace TBN.Gameplay.Characters
{

    class BoxMan : Character
    {
        
        
        
        public BoxMan(Vector2 anchor, InputController input, SpriteSheet sheet) : base(anchor, input, sheet)
        {
        
            
            MoveList.Add("Idle",new Action(0,2,0,0,new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>>(), maxHits: 0, miscBehaviors: new List<Tuple<int,SimpleBehavior>>()));
            MoveList.Add("Walk", new Action(4,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>> { new Tuple<int, Vector2>(1,new Vector2(4,0)), new Tuple<int,Vector2>(2,new Vector2(4,0)) }, 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("BackWalk", new Action(5,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>> { new Tuple<int, Vector2>(1, new Vector2(-4, 0)), new Tuple<int, Vector2>(2, new Vector2(-4, 0)) }, 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("Crouch", new Action(2,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("Precrouch", new Action(1,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("CrouchGetup", new Action(3,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("PreJump", new Action(6,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>> { new Tuple<int, SimpleBehavior>(1,Behavior.SetJumpHeight(this,90))}));
            MoveList.Add("JumpIdle", new Action(8,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("Jump", new Action(7,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>> { new Tuple<int, Vector2>(0,new Vector2(3f*Input.StickPos.X,17)), new Tuple<int, Vector2>(1, new Vector2(3f * Input.StickPos.X, 17)) }, 0, new List<Tuple<int, SimpleBehavior>>()));
            
            

            MoveList["Idle"].ComboList = new List<Tuple<ActionCondition, Action>>{ new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,0), InputManager.None()),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,0), InputManager.None()), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,2,0), InputManager.None()),MoveList["Precrouch"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,6,0)+InputManager.GenerateLogic(Input,7,0)+InputManager.GenerateLogic(Input,8,0), InputManager.None()),MoveList["PreJump"]) };
            MoveList["Walk"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,4,0), InputManager.None()),MoveList["Idle"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,0), InputManager.None()), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,2,0), InputManager.None()),MoveList["Precrouch"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,6,0)+InputManager.GenerateLogic(Input,7,0)+InputManager.GenerateLogic(Input,8,0), InputManager.None()),MoveList["PreJump"]) };
            MoveList["BackWalk"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,0), InputManager.None()),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,4,0), InputManager.None()), MoveList["Idle"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,2,0), InputManager.None()),MoveList["Precrouch"]),
            new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,6,0)+InputManager.GenerateLogic(Input,7,0)+InputManager.GenerateLogic(Input,8,0), InputManager.None()),MoveList["PreJump"]) };
            MoveList["Crouch"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,0), InputManager.None()),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,0), InputManager.None()), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(1, 1, false, InputManager.GenerateLogic(Input,2,0), InputManager.None()),MoveList["Crouch"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,4,0), InputManager.None()), MoveList["CrouchGetup"])};
            MoveList["Precrouch"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,0), InputManager.None()),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,0), InputManager.None()), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,4,0), InputManager.None()), MoveList["CrouchGetup"])};
            MoveList["CrouchGetup"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,0), InputManager.None()),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,0), InputManager.None()), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,2,0), InputManager.None()),MoveList["Precrouch"]),
            new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,6,0)+InputManager.GenerateLogic(Input,7,0)+InputManager.GenerateLogic(Input,8,0), InputManager.None()),MoveList["PreJump"])};
            MoveList["PreJump"].ComboList = new List<Tuple<ActionCondition, Action>>();
            MoveList["JumpIdle"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.Yes(), InputManager.OnGround(this)), MoveList["Idle"]) };
            MoveList["Jump"].ComboList = new List<Tuple<ActionCondition, Action>> {new Tuple<ActionCondition, Action>(new ActionCondition(0,1,false,InputManager.GenerateLogic(Input,9,4), InputManager.None()),MoveList["JumpIdle"]),
            new Tuple<ActionCondition, Action>(new ActionCondition(0,1,false,null, InputManager.OnGround(this)),MoveList["Idle"])
            };
            CurrentAction = MoveList["JumpIdle"];
        }

        public override void GoIdle()
        {
            if (OnGround)
            {
                for (int i = 0; i < Input.InputHistory.Length; i++)
                {
                    if (i < 9)
                    {
                        if (i < 3)
                        {
                            if(Input.InputHistory[i]<=0)
                                CurrentAction = MoveList["Crouch"];
                        }
                        if (i == 3)
                        {
                            if (Input.InputHistory[i] <= 0)
                                CurrentAction = MoveList["BackWalk"];
                        }
                        if (i == 4)
                        {
                            if (Input.InputHistory[i] <= 0)
                                CurrentAction = MoveList["Idle"];
                        }
                        if (i == 5)
                        {
                            if (Input.InputHistory[i] <= 0)
                                CurrentAction = MoveList["Walk"];
                        }
                        if (i > 5)
                        {
                            if (Input.InputHistory[i] <= 0)
                                CurrentAction = MoveList["PreJump"];
                        }
                    }
                }
                CurrentAction = MoveList["Idle"];
            }
            else if (CurrentAction.ActionId == 7&&AnchorPoint.Y<JumpHeight)
            {
                CurrentAction = MoveList["Jump"];
            }
            else
            {
                CurrentAction = MoveList["JumpIdle"];
            }
           
        }

    }
}
