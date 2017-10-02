﻿using System;
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

           
            MoveList.Add("Idle",new Action(0,2,0,0,new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] {0,1},"/0*0*32*32//0*0*32*32/%",32),new List<Tuple<int, Vector2>>(), 0,new List<Tuple<int,SimpleBehavior>>()));
            MoveList.Add("Walk", new Action(1,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, "/0*0*32*32//0*0*32*32/%", 32), new List<Tuple<int, Vector2>> { new Tuple<int, Vector2>(1,new Vector2(4,0)), new Tuple<int,Vector2>(2,new Vector2(4,0)) }, 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("BackWalk", new Action(2,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, "/0*0*32*32//0*0*32*32/%", 32), new List<Tuple<int, Vector2>> { new Tuple<int, Vector2>(1, new Vector2(-4, 0)), new Tuple<int, Vector2>(2, new Vector2(-4, 0)) }, 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("Crouch", new Action(3,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, "/0*0*32*32//0*0*32*32/%", 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("Precrouch", new Action(4,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, "/0*0*32*32//0*0*32*32/%", 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("CrouchGetup", new Action(5,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, "/0*0*32*32//0*0*32*32/%", 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("PreJump", new Action(6,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, "/0*0*32*32//0*0*32*32/%", 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("JumpIdle", new Action(7,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, "/0*0*32*32//0*0*32*32/%", 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList.Add("Jump", new Action(8,2, 0, 0, new List<Tuple<int, Rectangle>>(), HitboxGenerator(new int[] { 0, 1 }, "/0*0*32*32//0*0*32*32/%", 32), new List<Tuple<int, Vector2>>(), 0, new List<Tuple<int, SimpleBehavior>>()));
            MoveList["Idle"].ComboList = new List<Tuple<ActionCondition, Action>> { new Tuple<ActionCondition, Action>(new ActionCondition(0, 1, false, InputManager., null),MoveList["Walk"]) };
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
