﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TBN.Gameplay.Framework;
using System.Threading;

namespace TBN.Gameplay.Characters
{

    class AltBoxMan : Character
    {
        public override float DamageMultiplier { get { return 1; } }



        public AltBoxMan(Vector2 anchor, InputController input, SpriteSheet sheet) : base(anchor, input, sheet)
        {
        
            //add moves to movelist
            MoveList.Add("Idle", Idle);
            MoveList.Add("Walk", Walk);
            MoveList.Add("BackWalk", new Action(5,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>> { new Tuple<int, Vector2>(1, new Vector2(-8, 0)), new Tuple<int, Vector2>(2, new Vector2(-8, 0)) }, 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("Crouch", new Action(2,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("Precrouch", new Action(1,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("CrouchGetup", new Action(3,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("PreJump", new Action(6,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>> { new Tuple<int, SimpleBehavior>(1,Behavior.SetJumpHeight(this,70))}));
            MoveList.Add("JumpIdle", new Action(8,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("Jump", new Action(7,2, 0, 0, new List<Tuple<int, Rectangle[]>>(), HitboxGenerator(new int[] { 0, 1 }, new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32) }, 32), new List<Tuple<int, Vector2>> { new Tuple<int, Vector2>(0,new Vector2(0,-5)), new Tuple<int, Vector2>(1, new Vector2(0, -5)) }, 0, new List<Tuple<int, SimpleBehavior>> { new Tuple<int,SimpleBehavior>(0,Behavior.OffGround(this)), new Tuple<int, SimpleBehavior>(1, Behavior.OffGround(this)),new Tuple<int, SimpleBehavior>(0,Behavior.FreeMoveX(this,8)),new Tuple<int, SimpleBehavior>(1,Behavior.FreeMoveX(this,8)) }));
            
            
            //set up transitions
            MoveList["BackWalk"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input,5,1), Logic.None()),MoveList["Walk"]),
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
            MoveList["PreJump"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>( new ActionCondition(0, 1, false, InputManager.GenerateLogic(Input, 7, 1), Logic.UnderHeight(this, JumpHeight)), MoveList["Jump"]) };
            MoveList["JumpIdle"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager.Yes(), Logic.OnGround(this)), MoveList["Idle"]) };
            MoveList["Jump"].ComboList = new List<Tuple<ActionCondition, Action>> {new Tuple<ActionCondition, Action>(new ActionCondition(0,1,false,InputManager.GenerateLogic(Input,7,1), Logic.UnderHeight(this,JumpHeight)),MoveList["Jump"]),
            new Tuple<ActionCondition, Action>(new ActionCondition(0,1,false,InputManager.Yes(), Logic.OnGround(this)),MoveList["Idle"])
            };
            CurrentAction = MoveList["JumpIdle"];
        }


        public override void GoIdle()
        {
            if (OnGround)
            {
                
                            if (Input.InputHistory[1]<=1)
                                CurrentAction = MoveList["Crouch"];
                        
                        
                            if (Input.InputHistory[3] <= 1)
                                CurrentAction = MoveList["BackWalk"];
                        
                       
                            if (Input.InputHistory[4] <= 1)
                                CurrentAction = MoveList["Idle"];
                        
                            if (Input.InputHistory[5] <= 1)
                                CurrentAction = MoveList["Walk"];
                        
                        
                            if (Input.InputHistory[6] <= 1 || Input.InputHistory[7] <= 1 || Input.InputHistory[8] <= 1)
                                CurrentAction = MoveList["PreJump"];
                            if (CurrentAction.ActionId == 6 || CurrentAction.ActionId == 7 && AnchorPoint.Y > JumpHeight)
                            {
                            CurrentAction = MoveList["Jump"];
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


        #region Actions

        private Action idle;
        public Action Idle
        {
            get {
                if(idle == null)
                {
                    idle = new Action(
                        //Action ID
                        0,
                        //Length in Frames
                        2,
                        //Juggle Num
                        0,
                        //juggleMod
                        0,
                        //Hitboxes for taking damage
                        new List<Tuple<int, Rectangle[]>> { },
                        //Hitboxes for dealing damage
                        new List<Tuple<int, Rectangle[]>> {
                            new Tuple<int, Rectangle[]>(0, new Rectangle[]{ new Rectangle(0, 0, 32, 32) }),
                            new Tuple<int, Rectangle[]>(0, new Rectangle[]{ new Rectangle(0, 0, 32, 32) })
                        },
                        //Displacements
                        new List<Tuple<int, Vector2>> { },
                        //max hits
                        0,
                        //Misc behaviors
                        null);
                    #region combolist
                    idle.ComboList = new List<Tuple<ActionCondition, Action>>{
                            new Tuple<ActionCondition, Action>(
                                new ActionCondition(
                                    //Start frame
                                    0, 
                                    //End frame
                                    1,
                                    //Must hit
                                    false, 
                                    //Controller Logic
                                    InputManager.GenerateLogic(Input,5,1), 
                                    //Misc Logic
                                    null
                                ),
                                Walk
                            ),

                        };
                        #endregion
                }
                return idle;
            }
        }
        private Action walk;
        public Action Walk
        {
            get
            {
                if (walk == null)
                {
                    walk = new Action(
                        //Action ID
                        4,
                        //Length in Frames
                        2,
                        //Juggle Num
                        0,
                        //juggleMod
                        0,
                        //Hitboxes for taking damage
                        new List<Tuple<int, Rectangle[]>> { },
                        //Hitboxes for dealing damage
                        new List<Tuple<int, Rectangle[]>> {
                            new Tuple<int, Rectangle[]>(0, new Rectangle[]{ new Rectangle(0, 0, 32, 32) }),
                            new Tuple<int, Rectangle[]>(0, new Rectangle[]{ new Rectangle(0, 0, 32, 32) })
                        },
                        //Displacements
                        new List<Tuple<int, Vector2>> {
                            new Tuple<int, Vector2>(0, new Vector2(8, 0)),
                            new Tuple<int, Vector2>(2, new Vector2(8, 0))
                        },
                        //max hits
                        0,
                   
                        //Misc behaviors
                        null);
                    #region combolist
                        walk.ComboList = new List<Tuple<ActionCondition, Action>>{
                            new Tuple<ActionCondition, Action>(new ActionCondition(0,0,false, null, null), Walk)
                        };
                    #endregion

                }
                return walk;
            }
        }
        #endregion



    }//End Class
}