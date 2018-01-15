using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Threading;

namespace TBN
{



    class BoxMan : StandardCharacter
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
            MoveList["Idle"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Idle"].EndFrame = 2;

            MoveList["Walk"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Walk"].EndFrame = 2;

            MoveList["BackWalk"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["BackWalk"].EndFrame = 2;

            MoveList["Crouch"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Crouch"].EndFrame = 2;

            MoveList["Precrouch"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Precrouch"].EndFrame = 2;

            MoveList["CrouchGetup"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["CrouchGetup"].EndFrame = 2;

            MoveList["PreJump"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["PreJump"].EndFrame = 2;

            MoveList["JumpIdle"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["JumpIdle"].EndFrame = 2;

            MoveList["Jump"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Jump"].EndFrame = 2;

            #region Light
            //Action level Properties
            MoveList["Light"].MyType = AttackType.Strike;
            MoveList["Light"].MyProperties = ActionProperties.Light;

            //Move Specifics
            MoveList["Light"].EndFrame = 2;
            MoveList["Light"].MaxHits = 1;
            MoveList["Light"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Light"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(16, -32, 32, 32) }, new float[] { 1 });

            //On Hit Properties
            MoveList["Light"].Damage = 1;
            MoveList["Light"].JuggleNumber = 300;
            MoveList["Light"].JuggleMod = 0;
            MoveList["Light"].StunOnHit = 30;
            MoveList["Light"].StunOnBlock = 5;
            MoveList["Light"].ScalingMod = 1;
            MoveList["Light"].RedHealth = 0;
            MoveList["Light"].Knockback = new Vector2(20, -20);
            #endregion

            #region Medium
            //Action level Properties
            MoveList["Medium"].MyType = AttackType.Strike;
            MoveList["Medium"].MyProperties = ActionProperties.None;

            //Move Specifics
            MoveList["Medium"].EndFrame = 2;
            MoveList["Medium"].MaxHits = 1;
            MoveList["Medium"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Medium"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(32, -24, 16, 16) }, new float[] { 1 });

            //On Hit Properties
            MoveList["Medium"].Damage = 2;
            MoveList["Medium"].JuggleNumber = 300;
            MoveList["Medium"].JuggleMod = 0;
            MoveList["Medium"].StunOnHit = 30;
            MoveList["Medium"].StunOnBlock = 5;
            MoveList["Medium"].ScalingMod = 1;
            MoveList["Medium"].RedHealth = 0;
            #endregion

            #region Heavy
            //Action level Properties
            MoveList["Heavy"].MyType = AttackType.Strike;
            MoveList["Heavy"].MyProperties = ActionProperties.None;

            //Move Specifics
            MoveList["Heavy"].EndFrame = 2;
            MoveList["Heavy"].MaxHits = 1;
            MoveList["Heavy"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Heavy"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(16, -24, 16, 16) }, new float[] { 1 });

            //On Hit Properties
            MoveList["Heavy"].Damage = 3;
            MoveList["Heavy"].JuggleNumber = 300;
            MoveList["Heavy"].JuggleMod = 0;
            MoveList["Heavy"].StunOnHit = 30;
            MoveList["Heavy"].StunOnBlock = 5;
            MoveList["Heavy"].ScalingMod = 1;
            MoveList["Heavy"].RedHealth = 0;
            #endregion

        }
    }
}
