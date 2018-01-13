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
            MoveList["Idle"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock;

            MoveList.Add("Walk", new Action(4));
            MoveList["Walk"].AddDisplacementKeyFrame(0, new Vector2(8, 0));
            MoveList["Walk"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock;

            MoveList.Add("BackWalk", new Action(5));
            MoveList["BackWalk"].AddDisplacementKeyFrame(0, new Vector2(-8, 0));
            MoveList["BackWalk"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock;

            MoveList.Add("Crouch", new Action(2));
            MoveList["Crouch"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock;

            MoveList.Add("Precrouch", new Action(1));

            MoveList.Add("CrouchGetup", new Action(3));

            MoveList.Add("PreJump", new Action(6));
            MoveList["PreJump"].AddMiscBehavior(0, Behavior.SetJumpHeight(this, 120));

            MoveList.Add("JumpIdle", new Action(8));
            MoveList["JumpIdle"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock;

            MoveList.Add("Jump", new Action(7));
            MoveList["Jump"].AddMiscBehavior(-1, Behavior.OffGround(this));
            MoveList["Jump"].AddMiscBehavior(-1, Behavior.FreeMoveX(this, 6));
            MoveList["Jump"].AddDisplacementKeyFrame(0, new Vector2(0, -5));
            MoveList["Jump"].MyProperties = ActionProperties.Loops | ActionProperties.AllowBlock;

            //Damaged states
            MoveList.Add("HitStun", new Action(1));
            MoveList["HitStun"].MyProperties = ActionProperties.UnGrabbable;

            MoveList.Add("BlockStun", new Action(1));
            MoveList["BlockStun"].MyProperties = ActionProperties.UnGrabbable;

            MoveList.Add("Stagger", new Action(1));
            MoveList["Stagger"].MyProperties = ActionProperties.UnGrabbable;
            MoveList["Stagger"].AddMiscBehavior(-1, () => { if (InputManager.Mashing()(this.Input)) this.CurrentAction.EndFrame--; });

            MoveList.Add("AirStagger", new Action(1));
            MoveList["AirStagger"].MyProperties = ActionProperties.UnGrabbable | ActionProperties.Loops;

            MoveList.Add("GrabbedH", new Action(1));
            MoveList["GrabbedH"].MyProperties = ActionProperties.UnGrabbable;

            MoveList.Add("GrabbedV", new Action(1));
            MoveList["GrabbedV"].MyProperties = ActionProperties.UnGrabbable;

            MoveList.Add("Attack", new Action(0));


            #region Combo List
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

            if(Scaling < .2f)
            {
                Scaling = .2f;
            }

            //If blocked
            if (Input.GetStickPos().X < 0 && !Struck.MyProperties.HasFlag(ActionProperties.Unblockable) && CurrentAction.MyProperties.HasFlag(ActionProperties.AllowBlock))
            {
                switch (Struck.MyType)
                {
                    case AttackType.Strike:
                        CurrentAction = MoveList["BlockStun"];
                        CurrentAction.EndFrame = Struck.StunOnBlock;
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
                        CurrentAction = MoveList["BlockStun"];
                        CurrentAction.EndFrame = Struck.StunOnBlock;
                        Struck = null;
                        break;
                    default:
                        break;
                }
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
        }
    }
}
