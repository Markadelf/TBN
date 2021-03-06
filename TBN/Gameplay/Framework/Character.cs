﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace TBN
{
    public enum JuggleState
    {
        StageZero = 0,
        StageOne = 1,
        StageTwo = 2,
        StageThree = 3,
        StageFour = 4
    }
    public abstract class Character
    {
        #region Debug Values
        float MaxHeight = 0;
        #endregion
        #region Modifiers and Multipliers
        public abstract float DamageMultiplier{ get; }
        #endregion

        #region Properties
        Vector2 FrameDisplacement;//total amount of movement for the frame used only outside of psudoinertia
        /// <summary>
        /// keeps track of previous anchor
        /// </summary>
        Vector2 PrevAnchor;
        /// <summary>
        /// used for simple inertia calculation
        /// </summary>
        public Vector2 PreviousMovement;
        /// <summary>
        /// The point that is moved when the character is moved. Corresponds to the origin on the draw information.
        /// </summary>
        public Vector2 AnchorPoint { get; set; }
        /// <summary>
        /// True if the player is facing right.
        /// </summary>
        public bool FaceRight { get; set; }
        /// <summary>
        /// The input controller that controls this character
        /// </summary>
        public InputController Input { get; set; }
        /// <summary>
        /// The spritesheet used to draw this character
        /// </summary>
        public SpriteSheet MySheet { get; set; }

        /// <summary>
        /// A list of all the projectiles belonging to this character.
        /// </summary>
        public List<Projectile> MyProjectiles { get; set; }

        /// <summary>
        /// this will be what we use to display the moves in a list and also helps when writing the individual character's classes
        /// </summary>
        public Dictionary<String, Action> MoveList;

        
        /// <summary>
        /// floor height
        /// </summary>
        public int Floor { get; set; }

        #region currentActionInfo
        /// <summary>
        /// Current action storage. This variable should not be changed in a direct manner.
        /// </summary>
        private Action _currentAction;
        /// <summary>
        /// The current action the player is in
        /// </summary>
        public Action CurrentAction { get { return _currentAction; } set { _currentAction = value; CurrentActionFrame = 0; CurrentActionHits = 0; CurrentActionLastHit = -1; } }
        /// <summary>
        /// The length in frames that have passed since the action was primed
        /// </summary>
        public int CurrentActionFrame { get; set; }
        /// <summary>
        /// Number of times the action has hit
        /// </summary>
        public int CurrentActionHits { get; set; }
        /// <summary>
        /// The index of the last key frame that has hit the opponent
        /// </summary>
        public int CurrentActionLastHit { get; set; }
        /// <summary>
        /// The index of the keyframe that was last used to calculate Hitboxes.
        /// </summary>
        private int _hitboxKeyFrameIndex;
        #endregion

        /// <summary>
        /// Max Height of a jump used by go Idle 
        /// </summary>
        public int JumpHeight { get; set; }
        /// <summary>
        /// True if the last hit recieved was a light hit
        /// </summary>
        public bool LastHitLight { get; set; }

        /// <summary>
        /// returns true if the character is on the ground
        /// </summary>
        public bool OnGround { get; set; }

        /// <summary>
        /// The state of Juggle we are currently in.
        /// </summary>
        public JuggleState CurrentJuggleState {
            get
            {
                if (CurrentAction.MyProperties.HasFlag(ActionProperties.Incapacitated))
                    return (JuggleState)((JuggleMeter / 100) + 1);
                else
                    return JuggleState.StageZero;
            }
        }

        
        /// <summary>
        /// The value that directly influences the Juggle state.
        /// </summary>
        public int JuggleMeter { get; set; }

        #region lifeForce
        /// <summary>
        /// The Max health a character can have
        /// </summary>
        public float MaxHealth { get; set; }
        /// <summary>
        /// Do not directly meddle with this value
        /// </summary>
        private float _health;
        /// <summary>
        /// The health of the character
        /// </summary>
        public float Health
        {
            get { return _health; }
            set {
                if (value < 0)
                {
                    _health = 0;
                    return;
                }
                _health = value;
                if (_health > MaxHealth)
                {
                    _health = MaxHealth;
                }
                if (MaxHealth < _redHealth + _health)
                    _redHealth = MaxHealth - _health;
            }
        }
        /// <summary>
        /// Do not touch this variable
        /// </summary>
        private float _redHealth;
        /// <summary>
        /// The Red Health that the character has on top of their current health
        /// </summary>
        public float RedHealth
        {
            get { return _redHealth; }
            set {
                _redHealth = value;
                if (MaxHealth < _redHealth + _health)
                    _redHealth = MaxHealth - _health;
                if (_redHealth < 0)
                    _redHealth = 0;
            }
        }
        /// <summary>
        /// Scalling on the damage this character takes
        /// </summary>
        public float Scaling { get; set; }

        /// <summary>
        /// True if you have a tech
        /// </summary>
        public bool Tech { get; set; }

        /// <summary>
        /// True if you can be bounced
        /// </summary>
        public bool Bounce { get; set; }


        #endregion

        /// <summary>
        /// When the character is struck the info is left here
        /// </summary>
        public AttackInfo Struck { get; set; }
        /// <summary>
        /// The character this one is facing
        /// </summary>
        public Character Target { get; set; }
        #endregion

        #region
        public List<Projectile> Projectiles { get; set; }
        #endregion

        #region DebugDisplay
        /// <summary>
        /// Tells the literal drawer what color to draw the Hitboxes.
        /// </summary>
        public bool[] hitCollision { get; set; }


        #endregion



        public Character(Vector2 anchor, InputController input, SpriteSheet sheet, float health = 100)
        {
            FrameDisplacement = Vector2.Zero;
            PrevAnchor = anchor;
            PreviousMovement = Vector2.Zero;
            MoveList = new Dictionary<string, Action>();
            Floor = 300;
            AnchorPoint = anchor;
            Input = input;
            MySheet = sheet;
            OnGround = false;
            LastHitLight = false;
            JuggleMeter = 0;
            CurrentActionFrame = 0;
            CurrentActionHits = 0;
            CurrentActionLastHit = -1;
            MaxHealth = health;
            _health = MaxHealth;
            Struck = null;
            FaceRight = true;
            Target = null;
            _hitboxKeyFrameIndex = 0;
            Projectiles = new List<Projectile>();
            Tech = true;
            Bounce = true;
        }
        /// <summary>
        /// make a set of Hurtboxes or Hitboxes
        /// 
        /// to get a valid output right now you need to have the same number of ints as rectangles
        /// 
        /// to make a rectangle in "boxcode" use this format "/posx*posy*width*height//posx*posy*width*height/%"
        /// k
        /// basically you start and end every rectangle with '/' and separate parameters by anything but '/' or '%' 
        /// 
        /// you can end the method after finishing a rectangle by inputting
        /// </summary>
        /// <param name="frameoccurances">the frames in which the Hurtboxes appear in order</param>
        /// <param name="boxcode">Hurtboxes in order of appearance</param>
        /// <returns></returns>
        public List<Tuple<int, Rectangle[]>> HurtboxGenerator(int[] frameoccurances,Rectangle[] rect)
        {

            List<Tuple<int, Rectangle[]>> final = new List<Tuple<int, Rectangle[]>>();
            for (int i = 0; i < rect.Length; i++)
            {
                Tuple<int, Rectangle[]> Temp = new Tuple<int, Rectangle[]>(frameoccurances[i], new Rectangle[]{ rect[i] });
                final.Add(Temp);
            }
            return final;
        }

        public List<Tuple<int, Rectangle[]>> HurtboxGenerator(int[] frameoccurances, Rectangle[] rect,int tilesize)
        {

            List<Tuple<int, Rectangle[]>> final = new List<Tuple<int, Rectangle[]>>();
            int u = 0;
            for (int i = 0; i < rect.Length; i++)
            {
                Tuple<int, Rectangle[]> Temp = new Tuple<int, Rectangle[]>(frameoccurances[i], new Rectangle[] { rect[i] });
                final.Add(Temp);
            }


            return final;
        }
        /// <summary>
        /// A function that should cause the character to got to the apropriate action post-combo.
        /// Called when CurrentActionFrame >= CurrentAction.EndFrame
        /// </summary>
        public abstract void GoIdle();

        /// <summary>
        /// Called once a frame Handles the current action, inputs and comboing.
        /// </summary>
        public void UpdateAction()
        {
            CurrentActionFrame++;
            FrameDisplacement = Vector2.Zero;//reset desplacement for this frame
            //Input.Update();
            #region ActionTransition
            //Check if we are moving to a new Action
            for(int i = 0; i < CurrentAction.ComboList.Count; i++)
            {
                //If we meet the conditon, transition into the new state
                if (CurrentAction.ComboList[i].Item1.Evaluate(this))
                {
                    CurrentAction = CurrentAction.ComboList[i].Item2;
                    break;
                }
            }

            //If we fell out of the combo, Go to the apropriate action idle
            if(CurrentAction.EndFrame <= CurrentActionFrame)
            {
                CurrentActionFrame = 0;
                CurrentActionHits = 0;
                CurrentActionLastHit = -1;
                if (CurrentAction.MyProperties.HasFlag(ActionProperties.Loops))
                {
                    CurrentAction = CurrentAction;
                }
                else { GoIdle(); }
            }
            #endregion

            
        }

        public void MiscActions()
        {
            #region MiscBehaviors
            //Check if there are any misc behaviors for this frame
            if (CurrentAction.MiscBehaviors != null)
            {
                for (int i = 0; i < CurrentAction.MiscBehaviors.Count; i++)
                {
                    if (CurrentAction.MiscBehaviors[i].Item1 == CurrentActionFrame || CurrentAction.MiscBehaviors[i].Item1 == -1)
                    {
                        CurrentAction.MiscBehaviors[i].Item2();
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// Move the character this frame
        /// </summary>
        public void Move()
        {
            #region Movement

            //bool to see if we are in the realm of inertia
            bool psuedoInertia = CurrentAction.FrameDisplacement.Count == 0;

            //Check if there are any movement behaviors for this frame

            for (int i = 0; i < CurrentAction.FrameDisplacement.Count; i++)
            {
                //If we are after that key frame, and either the next key frame doesn't exist or we are still before it.
                if (CurrentAction.FrameDisplacement[i].Item1 <= CurrentActionFrame
                    &&
                    (i == CurrentAction.FrameDisplacement.Count - 1 || CurrentAction.FrameDisplacement[i + 1].Item1 > CurrentActionFrame))
                {
                    if (!float.IsNaN(CurrentAction.FrameDisplacement[i].Item2.X))
                    {
                        Vector2 temp = CurrentAction.FrameDisplacement[i].Item2;
                        if (!FaceRight)
                            temp.X = -temp.X;
                        AnchorPoint += temp;
                    }
                    else
                        psuedoInertia = true;
                    break;
                }
               
            }
            //If we have no valid movent forced on us
            if (psuedoInertia)
            {//apply gravity friction and inertia
                if (OnGround)
                {

                    if (Math.Abs(PreviousMovement.X) > 3)//are we going fast enough to slide
                    {
                        //if so truncate the previous movement into something usable
                        Vector2 Movement = new Vector2(PreviousMovement.X * .9f, 0);//friction
                        AnchorPoint += Movement;//add the movement
                    }

                }
                else
                {
                    Vector2 Movement = new Vector2(PreviousMovement.X, PreviousMovement.Y + 1f);//gravity 
                    AnchorPoint += Movement;//add to movement
                }
            }
            if (AnchorPoint.Y >= Floor)
            {
                OnGround = true;
                AnchorPoint = new Vector2(AnchorPoint.X, Floor);
            }
            else
            {
                OnGround = false;
            }
            PreviousMovement = AnchorPoint -PrevAnchor;
            PrevAnchor = AnchorPoint;
            #endregion
        }

        /// <summary>
        /// This is where all on hit logic occurs
        /// </summary>
        public void TryApplyStrike()
        {
            if(Struck != null)
            #region Apply Hit
            {
                ApplyStrike();
            }
            #endregion
        }

        public abstract void ApplyStrike();

        public virtual void Debug(SpriteBatch sb)
        {
            Texture2D tex = MySheet.Sheet;
            string s = "";
            for(int i = 9;i<Input.InputHistory.Length;i++)
            {
                if (Input.InputHistory[i] <= 3)
                {
                    s += i + " ";
                }
                
            }
            if (AnchorPoint.Y < Floor - MaxHeight)
            {
                MaxHeight = Floor - AnchorPoint.Y;
            }
            sb.Draw(tex,new Rectangle(20 + (int)Input.StickPos.X * 10, 20 + -10* (int)Input.StickPos.Y, 10, 10), color: Color.Black);
            sb.DrawString(Game1.Font, "Anchor Point "+AnchorPoint.ToString()+"\nButton pushed "+ s + "\nMax Height " + MaxHeight + "\nIntended Height" + (Floor-JumpHeight), new Vector2(40, 40), Color.Black);
        }
        public virtual void Draw(SpriteBatch sb)
        {
            FrameDrawInfo drawInfo = MySheet.FrameInfo[CurrentAction.ActionId][CurrentActionFrame % MySheet.FrameInfo[CurrentAction.ActionId].Length];
            SpriteEffects flip = SpriteEffects.None;
            if (!FaceRight)
                flip = SpriteEffects.FlipHorizontally;

            sb.Draw(MySheet.Sheet,
                AnchorPoint,
                drawInfo.SourceRectangle,
                Color.White,
                0f,
                drawInfo.Origin,
                1.0f,
                flip,
                0);
            DrawLiterals(sb);
        }

        /// <summary>
        /// Draws all of the Hurtboxes and Hitboxes.
        /// </summary>
        /// <param name="sb"></param>
        public virtual void DrawLiterals(SpriteBatch sb)
        {
            Tuple<float, Rectangle>[] hit = GetCurrentHurtboxs();
            for (int i = 0; i < hit.Length; i++)
            {
                sb.Draw(SpriteSheet.WhitePixel,
                    hit[i].Item2,
                    new Color(Color.Green, .1f));
            }
            Tuple<float, Rectangle>[] hurt = GetCurrentHitboxs();
            for (int i = 0; i < hurt.Length; i++)
            {
                if (hitCollision == null || !hitCollision[i])
                {
                    sb.Draw(SpriteSheet.WhitePixel,
                        hurt[i].Item2,
                        new Color(Color.Red, .1f));
                }
                else
                {
                    sb.Draw(SpriteSheet.WhitePixel,
                        hurt[i].Item2,
                        new Color(Color.Purple, .1f));
                }
            }
        }

        /// <summary>
        /// This character attempts to hurt the other character.
        /// </summary>
        public virtual AttackInfo TryAttack(Character other)
        {
            int directionalMultiplier = 1;
            if (!FaceRight)
                directionalMultiplier = -1;
            AttackInfo hit = null;
            Tuple<float, Rectangle>[] hurtboxes = other.GetCurrentHurtboxs();
            Tuple<float, Rectangle>[] hitboxes = GetCurrentHitboxs();
            hitCollision = new bool[hitboxes.Count()];
            for(int i = 0; i < hurtboxes.Length; i++)
            {
                for (int j = 0; j < hitboxes.Length; j++)
                {
                    if (hitboxes[j].Item2.Intersects(hurtboxes[i].Item2))
                    {
                        // This if statement will be expanded to determine whether or not a move hits.
                        // An Attack info should never go through if the other player doesn't respond in some manner to the strike.
                        if (
                            //We haven't already hit too much
                            CurrentActionHits < CurrentAction.MaxHits && 
                            //We get the go ahead from the juggler
                            JuggleMeter < CurrentAction.JuggleNumber &&
                            //Either we aren't grabbing or we can grab
                            ((CurrentAction.MyType != AttackType.GrabHorizontal && CurrentAction.MyType != AttackType.GrabVertical) 
                                || !other.CurrentAction.MyProperties.HasFlag(ActionProperties.UnGrabbable))
                            
                            )
                        {
                            // Create attack info here
                            AttackInfo temp = new AttackInfo(
                                CurrentAction.MyType, CurrentAction.MyProperties, (int)(CurrentAction.JuggleMod * hitboxes[j].Item1 * hurtboxes[i].Item1),
                                CurrentAction.Damage * DamageMultiplier * hitboxes[j].Item1 * hurtboxes[i].Item1,
                                CurrentAction.ScalingMod,
                                CurrentAction.RedHealth,
                                CurrentAction.StunOnHit,
                                CurrentAction.StunOnBlock,
                                new Vector2(CurrentAction.Knockback.X * directionalMultiplier, CurrentAction.Knockback.Y)
                                );
                            if (hit == null)
                            {
                                CurrentActionHits++;
                                hit = temp;
                                CurrentActionLastHit = _hitboxKeyFrameIndex;
                            }
                            else if (hit.Damage < temp.Damage)
                            {
                                hit = temp;
                            }
                        }
                        hitCollision[j] = true;
                    }

                }
            }
            return hit;
        }

        /// <summary>
        /// A simple function that converts Red Health to regular health
        /// </summary>
        /// <param name="val"></param>
        public void StabilizeRedHealth(float val)
        {
            if (val > _redHealth)
                val = _redHealth;
            _redHealth -= val;
            Health += val;
        }


        #region Projectile Logic
        /// <summary>
        /// Moves all of your projectiles
        /// </summary>
        public void MoveProjectiles()
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i].UpdatePosition();
            }
        }


        /// <summary>
        /// Trys to hit with all of your projectiles
        /// </summary>
        public void ProjectilesTryHit(Character other)
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i].TryHit(other);
                if (Projectiles[i].Dead())
                {
                    Projectiles.RemoveAt(i);
                    i--;
                }
            }
        }


        #endregion



        #region orientation
        public void Reface()
        {
            FaceRight = Target.AnchorPoint.X > AnchorPoint.X;
            Input.FaceRight = FaceRight;
        }

        /// <summary>
        /// Gets the current Hurtboxes in World Space
        /// </summary>
        /// <returns></returns>
        public Tuple<float, Rectangle>[] GetCurrentHurtboxs()
        {
            int place = -1;
            for(int i = 0; i < CurrentAction.Hurtboxes.Count; i++)
            {
                if(CurrentAction.Hurtboxes[i].Item1 <= CurrentActionFrame)
                {
                    place = i;
                }
                else
                {
                    break;
                }
            }
            if (place == -1)
            {
                return new Tuple<float, Rectangle>[] { };

            }
            int pos = 0;
            Tuple<float, Rectangle>[] ret = new Tuple<float, Rectangle>[CurrentAction.Hurtboxes[place].Item2.Length];
            for (int i = 0; i < (CurrentAction.Hurtboxes[place].Item2).Length; i++)
            {
                if(FaceRight)
                    ret[pos] = new Tuple<float, Rectangle>(CurrentAction.HurtboxMultipliers[place][i], ConvertToWorldSpace(AnchorPoint, CurrentAction.Hurtboxes[place].Item2[i]));
                else
                    ret[pos] = new Tuple<float, Rectangle>(CurrentAction.HurtboxMultipliers[place][i], ConvertToWorldSpace(AnchorPoint, FaceLeft(CurrentAction.Hurtboxes[place].Item2[i])));
                pos++;
            }
            return ret;
        }

        /// <summary>
        /// Gets the current Hitboxes in World Space
        /// </summary>
        /// <returns></returns>
        public Tuple<float, Rectangle>[] GetCurrentHitboxs()
        {
            int place = -1;
            for (int i = 0; i < CurrentAction.Hitboxes.Count; i++)
            {
                if (CurrentAction.Hitboxes[i].Item1 <= CurrentActionFrame)
                {
                    if(CurrentActionLastHit < i)
                        place = i;
                }
                else
                {
                    break;
                }
            }
            if (place == -1)
            {
                return new Tuple<float, Rectangle>[] { };

            }
            _hitboxKeyFrameIndex = place;
            int pos = 0;
            Tuple<float, Rectangle>[] ret = new Tuple<float, Rectangle>[CurrentAction.Hitboxes[place].Item2.Length];
            for (int i = 0; i < (CurrentAction.Hitboxes[place].Item2).Length; i++)
            {
                if (FaceRight)
                    ret[pos] = new Tuple<float, Rectangle>(CurrentAction.HitboxMultipliers[place][i], ConvertToWorldSpace(AnchorPoint, CurrentAction.Hitboxes[place].Item2[i]));
                else
                    ret[pos] = new Tuple<float, Rectangle>(CurrentAction.HitboxMultipliers[place][i], ConvertToWorldSpace(AnchorPoint, FaceLeft(CurrentAction.Hitboxes[place].Item2[i])));
                pos++;
            }
            return ret;
        }

        static Rectangle ConvertToWorldSpace(Vector2 anchor, Rectangle rect)
        {
            rect.X += (int)anchor.X;
            rect.Y += (int)anchor.Y;
            return rect;
        }

        static Rectangle ConvertToAnchorSpace(Vector2 anchor, Rectangle rect)
        {
            rect.X -= (int)anchor.X;
            rect.Y -= (int)anchor.Y;
            return rect;
        }

        static Rectangle FaceLeft(Rectangle before)
        {
            return new Rectangle(-before.X - before.Width, before.Y, before.Width, before.Height);
        }
        
        #endregion


    }
}
