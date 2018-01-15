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
        /// Returns the current Hitbox of the projectile with a global position
        /// </summary>
        /// <returns>The Hitbox relative to the world</returns>
        Rectangle GetCurrentHitbox();
        
        /// <summary>
        /// The event that checks for collisions and handles them
        /// </summary>
        /// <param name="other">The thing you are trying to hit</param>
        void TryHit(Character other);

        /// <summary>
        /// Returns true if the projectile is dead
        /// </summary>
        /// <returns>True if the projectile is dead</returns>
        bool Dead();

    }
}
