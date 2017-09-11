using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace TBN
{
    public interface Projectile
    {
        /// <summary>
        /// Updates Position
        /// </summary>
        void UpdatePosition();
        /// <summary>
        /// Draws the projectile. Params need to be updated when we know what we need to pass in
        /// </summary>
        void Draw();
        /// <summary>
        /// Returns the current hitbox of the projectile with a global position
        /// </summary>
        /// <returns>The hitbox relative to the world</returns>
        Rectangle GetCurrentHitbox();
        /// <summary>
        /// The event that occurs when a collision occurs
        /// </summary>
        /// <param name="other"></param>
        void Hit(Character other);

    }
}
