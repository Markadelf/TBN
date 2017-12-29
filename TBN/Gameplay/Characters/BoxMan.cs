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
        public override float DamageMultiplier
        {
            get { return 1; }
        }

        Point TopLeft(int x, int y)
        {
          return new Point(x + (int)AnchorPoint.X - 16, y + (int)AnchorPoint.Y - 32);         
            
        }
        
        public BoxMan(Vector2 anchor, InputController input, SpriteSheet sheet) : base(anchor, input, sheet)
        {

           
            //add moves to movelist
            MoveList.Add("Idle",new Action(0,2,0,0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }), new List<Tuple<int, Vector2>>(), maxHits: 0, miscBehaviors: new List<Tuple<int,SimpleBehavior>>()));
            MoveList.Add("Walk", new Action(4,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }), new List<Tuple<int, Vector2>> { new Tuple<int, Vector2>(1,new Vector2(8,0)), new Tuple<int,Vector2>(2,new Vector2(8,0)) }, 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("BackWalk", new Action(5,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }), new List<Tuple<int, Vector2>> { new Tuple<int, Vector2>(1, new Vector2(-8, 0)), new Tuple<int, Vector2>(2, new Vector2(-8, 0)) }, 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("Crouch", new Action(2,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("Precrouch", new Action(1,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("CrouchGetup", new Action(3,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("PreJump", new Action(6,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>> { new Tuple<int, SimpleBehavior>(0,Behavior.SetJumpHeight(this,120))}));
            MoveList.Add("JumpIdle", new Action(8,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("Jump", new Action(7,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }), new List<Tuple<int, Vector2>> { new Tuple<int, Vector2>(0,new Vector2(0,-6)), new Tuple<int, Vector2>(1, new Vector2(0, -10)) }, 0, new List<Tuple<int, SimpleBehavior>> { new Tuple<int,SimpleBehavior>(-1, Behavior.OffGround(this)),new Tuple<int, SimpleBehavior>(-1,Behavior.FreeMoveX(this,6))}));
            MoveList.Add("Attack", new Action(0, 2, 0, 0, HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(32, 0, 32, 32), new Rectangle(32, 0, 32, 32) }), new List<Tuple<int, Rectangle[]>>(), new List<Tuple<int, Vector2>>(), maxHits: 1, miscBehaviors: new List<Tuple<int, SimpleBehavior>>()));

            
            //set up transitions
            
            MoveList["Idle"].ComboList = new List<Tuple<ActionCondition, Action>>{ new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(9, 1), Logic.None()), MoveList["Attack"]), new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,1), Logic.None()),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,1), Logic.None()), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,1,1), Logic.None()),MoveList["Precrouch"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,6,1)+InputManager.GenerateLogic(Input,7,1)+InputManager.GenerateLogic(Input,8,1), Logic.None()),MoveList["PreJump"]) };
            MoveList["Walk"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input, 9, 1), Logic.None()), MoveList["Attack"]), new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,4,1), Logic.None()),MoveList["Idle"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,1), Logic.None()), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,1,1), Logic.None()),MoveList["Precrouch"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,6,1)+InputManager.GenerateLogic(Input,7,1)+InputManager.GenerateLogic(Input,8,1), Logic.None()),MoveList["PreJump"]) };
            MoveList["BackWalk"].ComboList = new List<Tuple<ActionCondition, Action>> {new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input, 9, 1), Logic.None()), MoveList["Attack"]), new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,1), Logic.None()),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,4,1), Logic.None()), MoveList["Idle"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,1,1), Logic.None()),MoveList["Precrouch"]),
            new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,6,1)+InputManager.GenerateLogic(Input,7,1)+InputManager.GenerateLogic(Input,8,1), Logic.None()),MoveList["PreJump"]) };
            MoveList["Crouch"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,1), Logic.None()),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,1), Logic.None()), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(1, 1, false, InputManager.GenerateLogic(Input,1,1), Logic.None()),MoveList["Crouch"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,4,1), Logic.None()), MoveList["CrouchGetup"])};
            MoveList["Precrouch"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,1), Logic.None()),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,1), Logic.None()), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,4,1), Logic.None()), MoveList["CrouchGetup"])};
            MoveList["CrouchGetup"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,1), Logic.None()),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,1), Logic.None()), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,1,1), Logic.None()),MoveList["Precrouch"]),
            new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,6,1)+InputManager.GenerateLogic(Input,7,1)+InputManager.GenerateLogic(Input,8,1), Logic.None()),MoveList["PreJump"])};
            MoveList["PreJump"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>( new ActionCondition(0, 1, false, null, null), MoveList["Jump"]) };
            MoveList["JumpIdle"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.Yes(), Logic.OnGround(this)), MoveList["Idle"]) };
            MoveList["Jump"].ComboList = new List<Tuple<ActionCondition, Action>> {new Tuple<ActionCondition, Action>(new ActionCondition(1,1,false,InputManager.GenerateLogic(Input,7,1), Logic.UnderHeight(this,JumpHeight)),MoveList["Jump"]),
            //new Tuple<ActionCondition, Action>(new ActionCondition(0,1,false,InputManager.Yes(), Logic.OnGround(this)),MoveList["Idle"])
            };
            CurrentAction = MoveList["JumpIdle"];
        }

        public override void GoIdle()
        {
            if (OnGround)
            {

                if (Input.GetInput(InputButton.Down) <= 1)
                {
                    CurrentAction = MoveList["Crouch"];
                }
                else if (Input.GetInput(InputButton.Back) <= 1)
                {
                    CurrentAction = MoveList["BackWalk"];

                }
                else if (Input.GetInput(InputButton.Neutral) <= 1)
                {
                    CurrentAction = MoveList["Idle"];
                }
                else if (Input.GetInput(InputButton.Forward) <= 1)
                {
                    CurrentAction = MoveList["Walk"];
                }
                else if (Input.InputHistory[6] <= 1 || Input.InputHistory[7] <= 1 || Input.InputHistory[8] <= 1)
                {
                    CurrentAction = MoveList["PreJump"];
                }


            }

            else if (CurrentAction.ActionId == 6||CurrentAction.ActionId==7 && AnchorPoint.Y > JumpHeight)
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
