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

        public virtual float WalkSpeed { get { return 5; } }
        public virtual float BackWalkSpeed { get { return WalkSpeed; } }
        public virtual int StandardJumpHeight { get { return 120; } }
        public virtual int FreeMoveJump { get { return 6; } }
        public virtual float JumpSpeed { get { return 5; } }



        public StandardCharacter(Vector2 anchor, InputController input, SpriteSheet sheet, float health = 100) : base(anchor, input, sheet, health)
        {
            //Define States
            MoveList.Add("Idle", new Action(0));
            MoveList["Idle"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock;

            MoveList.Add("Walk", new Action(4));
            MoveList["Walk"].AddDisplacementKeyFrame(0, new Vector2(WalkSpeed, 0));
            MoveList["Walk"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock;

            MoveList.Add("BackWalk", new Action(5));
            MoveList["BackWalk"].AddDisplacementKeyFrame(0, new Vector2(-BackWalkSpeed, 0));
            MoveList["BackWalk"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock;

            MoveList.Add("Crouch", new Action(2));
            MoveList["Crouch"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock;
            MoveList["Crouch"].AddDisplacementKeyFrame(0, new Vector2(0, 0));

            MoveList.Add("Precrouch", new Action(1));

            MoveList.Add("CrouchGetup", new Action(3));

            MoveList.Add("PreJump", new Action(6));
            MoveList["PreJump"].AddMiscBehavior(0, Behavior.SetJumpHeight(this, StandardJumpHeight));

            MoveList.Add("JumpIdle", new Action(8));
            MoveList["JumpIdle"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock;

            MoveList.Add("Jump", new Action(7));
            MoveList["Jump"].AddMiscBehavior(-1, Behavior.OffGround(this));
            MoveList["Jump"].AddMiscBehavior(-1, Behavior.FreeMoveX(this, FreeMoveJump));
            MoveList["Jump"].AddDisplacementKeyFrame(0, new Vector2(0, -JumpSpeed));
            MoveList["Jump"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock;

            //Damaged states
            MoveList.Add("HitStun", new Action(3));
            MoveList["HitStun"].MyProperties = ActionProperties.UnGrabbable | ActionProperties.Incapacitated;
            MoveList["HitStun"].AddDisplacementKeyFrame(0, new Vector2(0, 0));
            MoveList["HitStun"].AddDisplacementKeyFrame(1, new Vector2(float.NaN, float.NaN));

            MoveList.Add("BlockStun", new Action(3));
            MoveList["BlockStun"].MyProperties = ActionProperties.UnGrabbable | ActionProperties.Incapacitated;
            MoveList["BlockStun"].AddDisplacementKeyFrame(0, new Vector2(0,0));
            MoveList["BlockStun"].AddDisplacementKeyFrame(1, new Vector2(0,0));

            MoveList.Add("Stagger", new Action(3));
            MoveList["Stagger"].MyProperties = ActionProperties.UnGrabbable | ActionProperties.Incapacitated;
            MoveList["Stagger"].AddMiscBehavior(-1, () => { if (InputManager.Mashing()(this.Input)) this.CurrentAction.EndFrame--; });

            MoveList.Add("AirStagger", new Action(3));
            MoveList["AirStagger"].MyProperties = ActionProperties.UnGrabbable | ActionProperties.Loops | ActionProperties.Incapacitated;

            MoveList.Add("GrabbedH", new Action(3));
            MoveList["GrabbedH"].MyProperties = ActionProperties.UnGrabbable | ActionProperties.Incapacitated;

            MoveList.Add("GrabbedV", new Action(3));
            MoveList["GrabbedV"].MyProperties = ActionProperties.UnGrabbable | ActionProperties.Incapacitated;

            MoveList.Add("Light", new Action(5));
            MoveList.Add("Medium", new Action(5));
            MoveList.Add("Heavy", new Action(5));


            #region Combo List
            MoveList["Idle"].ComboList = new List<Tuple<ActionCondition, Action>>{
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(9, 1), null), MoveList["Light"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(10, 1), null), MoveList["Medium"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(11, 1), null), MoveList["Heavy"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,1), null),MoveList["Walk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,1), null), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,1,1), null),MoveList["Precrouch"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, 
                    InputManager.Or(InputManager.GenerateLogic(Input,6,1),InputManager.GenerateLogic(Input,7,1),InputManager.GenerateLogic(Input,8,1)), 
                    null),MoveList["PreJump"]) };

            MoveList["Walk"].ComboList = new List<Tuple<ActionCondition, Action>> {
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input, 9, 1), null), MoveList["Light"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(10, 1), null), MoveList["Medium"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(11, 1), null), MoveList["Heavy"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,4,1), null),MoveList["Idle"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,3,1), null), MoveList["BackWalk"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,1,1), null),MoveList["Precrouch"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, 
                    InputManager.Or(InputManager.GenerateLogic(Input,6,1),InputManager.GenerateLogic(Input,7,1),InputManager.GenerateLogic(Input,8,1)), 
                    null),MoveList["PreJump"]) };

            MoveList["BackWalk"].ComboList = new List<Tuple<ActionCondition, Action>> {
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input, 9, 1), null), MoveList["Light"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(10, 1), null), MoveList["Medium"]),
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(11, 1), null), MoveList["Heavy"]),
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

            MoveList["AirStagger"].ComboList = new List<Tuple<ActionCondition, Action>> {
                new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, null, Logic.OnGround(this)), MoveList["Stagger"]) };

            #endregion


            CurrentAction = MoveList["JumpIdle"];
        }




        //Inherited neccesities
        public override void ApplyStrike()
        {
            //Things done always if we reach this point
            JuggleMeter += Struck.JuggleMod;
            Scaling *= Struck.ScalingMod;
            LastHitLight = Struck.MyProperties.HasFlag(ActionProperties.Light);

            int direction = 1;
            if (!FaceRight)
                direction = -1;


            if(Scaling < .2f)
            {
                Scaling = .2f;
            }

            //If blocked
            if (Input.GetStickPos().X < 0 && !Struck.MyProperties.HasFlag(ActionProperties.Unblockable) && CurrentAction.MyProperties.HasFlag(ActionProperties.AllowBlock))
            {
                CurrentAction = MoveList["BlockStun"];
                CurrentAction.EndFrame = Struck.StunOnBlock;
                CurrentAction.FrameDisplacement[0] = new Tuple<int,Vector2>(0, new Vector2(Struck.Knockback.X * .6f * direction, 0));
                Struck = null;
            }
            //If not blocked
            else
            {
                Health -= Struck.Damage * Scaling;
                RedHealth += Struck.RedHealth * Scaling * Scaling;
                switch (Struck.MyType)
                {
                    case AttackType.Strike:
                        CurrentAction = MoveList["HitStun"];
                        CurrentAction.EndFrame = Struck.StunOnHit;
                        CurrentAction.FrameDisplacement[0] = new Tuple<int, Vector2>(0, new Vector2(Struck.Knockback.X * direction, Struck.Knockback.Y));
                        Struck = null;
                        break;
                    case AttackType.GrabVertical:
                        CurrentAction = MoveList["GrabbedV"];
                        CurrentAction.EndFrame = Struck.StunOnHit;
                        Struck = null;
                        break;
                    case AttackType.GrabHorizontal:
                        CurrentAction = MoveList["GrabbedH"];
                        CurrentAction.EndFrame = Struck.StunOnHit;
                        Struck = null;
                        break;
                    case AttackType.Stagger:
                        if (OnGround)
                        {
                            CurrentAction = MoveList["Stagger"];
                        }
                        else
                        {
                            CurrentAction = MoveList["AirStagger"];
                        }
                        MoveList["Stagger"].EndFrame = Struck.StunOnHit;
                        Struck = null;
                        break;
                    default:
                        break;
                }
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
            Scaling = 1;
        }
    }
}
