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
            MoveList["Idle"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Idle"].EndFrame = 2;
            MoveList["Idle"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock | ActionProperties.AllowGrab;

            MoveList.Add("Walk", new Action(4));
            MoveList["Walk"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Walk"].AddDisplacementKeyFrame(0, new Vector2(8, 0));
            MoveList["Walk"].EndFrame = 2;
            MoveList["Walk"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock | ActionProperties.AllowGrab;

            MoveList.Add("BackWalk", new Action(5));
            MoveList["BackWalk"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["BackWalk"].AddDisplacementKeyFrame(0, new Vector2(-8, 0));
            MoveList["BackWalk"].EndFrame = 2;
            MoveList["BackWalk"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock | ActionProperties.AllowGrab;

            MoveList.Add("Crouch", new Action(2));
            MoveList["Crouch"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Crouch"].EndFrame = 2;
            MoveList["Crouch"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock | ActionProperties.AllowGrab;

            MoveList.Add("Precrouch", new Action(1));
            MoveList["Precrouch"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Precrouch"].EndFrame = 2;

            MoveList.Add("CrouchGetup", new Action(3));
            MoveList["CrouchGetup"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["CrouchGetup"].EndFrame = 2;

            MoveList.Add("PreJump", new Action(6));
            MoveList["PreJump"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["PreJump"].MiscBehaviors = new List<Tuple<int, SimpleBehavior>> { new Tuple<int, SimpleBehavior>(0, Behavior.SetJumpHeight(this, 120)) };
            MoveList["PreJump"].EndFrame = 2;

            MoveList.Add("JumpIdle", new Action(8));
            MoveList["JumpIdle"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["JumpIdle"].EndFrame = 2;
            MoveList["JumpIdle"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock | ActionProperties.AllowGrab;

            MoveList.Add("Jump", new Action(7));
            MoveList["Jump"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Jump"].MiscBehaviors = new List<Tuple<int, SimpleBehavior>> { new Tuple<int, SimpleBehavior>(-1, Behavior.OffGround(this)), new Tuple<int, SimpleBehavior>(-1, Behavior.FreeMoveX(this, 6)) };
            MoveList["Jump"].EndFrame = 2;
            MoveList["Jump"].AddDisplacementKeyFrame(0, new Vector2(0, -5));
            MoveList["Jump"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock | ActionProperties.AllowGrab;

            MoveList.Add("Attack", new Action(0));
            MoveList["Attack"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Attack"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Attack"].EndFrame = 2;
            MoveList["Attack"].MaxHits = 1;
            MoveList["Attack"].StunOnHit = 10;
            MoveList["Attack"].StunOnBlock = 5;



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
            //throw new NotImplementedException();
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
