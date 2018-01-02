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
            MoveList["Idle"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Idle"].EndFrame = 2;

            MoveList["Walk"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Walk"].EndFrame = 2;

            MoveList["BackWalk"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["BackWalk"].EndFrame = 2;

            MoveList["Crouch"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Crouch"].EndFrame = 2;

            MoveList["Precrouch"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Precrouch"].EndFrame = 2;

            MoveList["CrouchGetup"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["CrouchGetup"].EndFrame = 2;

            MoveList["PreJump"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["PreJump"].EndFrame = 2;

            MoveList["JumpIdle"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["JumpIdle"].EndFrame = 2;

            MoveList["Jump"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Jump"].EndFrame = 2;

            MoveList["Attack"].AddHitboxKeyFrame(0, new Rectangle[] { new Rectangle(-16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Attack"].AddHurtboxKeyFrame(0, new Rectangle[] { new Rectangle(16, -32, 32, 32) }, new float[] { 1 });
            MoveList["Attack"].EndFrame = 2;
            MoveList["Attack"].MaxHits = 1;
            MoveList["Attack"].StunOnHit = 30;
            MoveList["Attack"].StunOnBlock = 5;

        }
    }
}
