using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBN
{
    public class StandardCharacter : Character
    {
        //Properties
        public override float DamageMultiplier { get { return 1; } }

        public StandardCharacter(Vector2 anchor, InputController input, SpriteSheet sheet, float health = 100) : base(anchor, input, sheet, health)
        {
            //Define States
            MoveList.Add("Idle", new Action(0));
            MoveList["Idle"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock | ActionProperties.AllowGrab;

            MoveList.Add("Walk", new Action(4));
            MoveList["Walk"].AddDisplacementKeyFrame(0, new Vector2(8, 0));
            MoveList["Walk"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock | ActionProperties.AllowGrab;

            MoveList.Add("BackWalk", new Action(5));
            MoveList["BackWalk"].AddDisplacementKeyFrame(0, new Vector2(-8, 0));
            MoveList["BackWalk"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock | ActionProperties.AllowGrab;

            MoveList.Add("Crouch", new Action(2));
            MoveList["Crouch"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock | ActionProperties.AllowGrab;

            MoveList.Add("Precrouch", new Action(1));

            MoveList.Add("CrouchGetup", new Action(3));

            MoveList.Add("PreJump", new Action(6));
            MoveList["PreJump"].MiscBehaviors = new List<Tuple<int, SimpleBehavior>> { new Tuple<int, SimpleBehavior>(0, Behavior.SetJumpHeight(this, 120)) };

            MoveList.Add("JumpIdle", new Action(8));
            MoveList["JumpIdle"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock | ActionProperties.AllowGrab;

            MoveList.Add("Jump", new Action(7));
            MoveList["Jump"].MiscBehaviors = new List<Tuple<int, SimpleBehavior>> { new Tuple<int, SimpleBehavior>(-1, Behavior.OffGround(this)), new Tuple<int, SimpleBehavior>(-1, Behavior.FreeMoveX(this, 6)) };
            MoveList["Jump"].AddDisplacementKeyFrame(0, new Vector2(0, -5));
            MoveList["Jump"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock | ActionProperties.AllowGrab;

            MoveList.Add("HitStun", new Action(1));
            MoveList["HitStun"].MyProperties = ActionProperties.AllowGrab;

            MoveList.Add("Attack", new Action(0));


            //Connections
            MoveList["Idle"].ComboList = new List<Tuple<ActionCondition, Action>>{
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(9, 1), null), MoveList["Attack"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,1), null),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,1), null), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,1,1), null),MoveList["Precrouch"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, 
                    InputManager.Or(InputManager.GenerateLogic(Input,6,1),InputManager.GenerateLogic(Input,7,1),InputManager.GenerateLogic(Input,8,1)), 
                    null),MoveList["PreJump"]) };
            MoveList["Walk"].ComboList = new List<Tuple<ActionCondition, Action>> {
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input, 9, 1), null), MoveList["Attack"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,4,1), null),MoveList["Idle"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,1), null), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,1,1), null),MoveList["Precrouch"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, 
                    InputManager.Or(InputManager.GenerateLogic(Input,6,1),InputManager.GenerateLogic(Input,7,1),InputManager.GenerateLogic(Input,8,1)), 
                    null),MoveList["PreJump"]) };
            MoveList["BackWalk"].ComboList = new List<Tuple<ActionCondition, Action>> {
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input, 9, 1), null), MoveList["Attack"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,1), null),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,4,1), null), MoveList["Idle"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,1,1), null),MoveList["Precrouch"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, 
                    InputManager.Or(InputManager.GenerateLogic(Input,6,1),InputManager.GenerateLogic(Input,7,1),InputManager.GenerateLogic(Input,8,1)), 
                    null),MoveList["PreJump"]) };
            MoveList["Crouch"].ComboList = new List<Tuple<ActionCondition, Action>> {
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,1), null),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,1), null), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,4,1), null), MoveList["CrouchGetup"])};
            MoveList["Precrouch"].ComboList = new List<Tuple<ActionCondition, Action>> {
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,1), null),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,1), null), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,4,1), null), MoveList["CrouchGetup"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(2, 2, false, null, null), MoveList["Crouch"])};
            MoveList["CrouchGetup"].ComboList = new List<Tuple<ActionCondition, Action>> {
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,1), null),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,1), null), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,1,1), null),MoveList["Precrouch"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, 
                    InputManager.Or(InputManager.GenerateLogic(Input,6,1),InputManager.GenerateLogic(Input,7,1),InputManager.GenerateLogic(Input,8,1)), 
                    null), MoveList["PreJump"])};
            MoveList["PreJump"].ComboList = new List<Tuple<ActionCondition, Action>> {
                new Tuple<ActionCondition, Action>(new ActionCondition(2, 2, false, null, null), MoveList["Jump"]) };
            MoveList["JumpIdle"].ComboList = new List<Tuple<ActionCondition, Action>> {
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, null, Logic.OnGround(this)), MoveList["Idle"]) };
            MoveList["Jump"].ComboList = new List<Tuple<ActionCondition, Action>> {
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, null, Logic.OverJumpHeight(this)), MoveList["JumpIdle"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, 
                    InputManager.Not(InputManager.Or(InputManager.GenerateLogic(Input,6,1),InputManager.GenerateLogic(Input,7,1),InputManager.GenerateLogic(Input,8,1))),
                    null), MoveList["JumpIdle"])
            };
            CurrentAction = MoveList["JumpIdle"];
        }




        //Inherited neccesities
        public override void ApplyStrike()
        {
            switch (Struck.MyType)
            {
                case AttackType.Strike:
                    CurrentAction = MoveList["HitStun"];
                    CurrentAction.EndFrame = Struck.StunOnHit;
                    Struck = null;
                    break;
                case AttackType.GrabVertical:
                    break;
                case AttackType.GrabHorizontal:
                    break;
                case AttackType.Stagger:
                    break;
                default:
                    break;
            }
        }

        public override void GoIdle()
        {
            if (OnGround)
            {
                CurrentAction = MoveList["Idle"];
            }
            else
            {
                CurrentAction = MoveList["JumpIdle"];
            }
        }
    }
}
