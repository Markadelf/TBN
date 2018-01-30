using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TBN
{




    public class AttackInfo
    {
        /// <summary>
        /// The classification for a given attack
        /// </summary>
        public AttackType MyType { get; set; }
        /// <summary>
        /// The properties of a given attack
        /// </summary>
        public ActionProperties MyProperties { get; set; }
        /// <summary>
        /// The value we add to the jugglemeter upon a hit
        /// </summary>
        public int JuggleMod { get; set; }
        /// <summary>
        /// The damage the move does.
        /// </summary>
        public float Damage { get; set; }
        /// <summary>
        /// The modifier to scaling that the move applies
        /// </summary>
        public float ScalingMod { get; set; }
        /// <summary>
        /// Amount of RedHealth this move will add to its opponents healthbar
        /// </summary>
        public float RedHealth { get; set; }
        /// <summary>
        /// Amount of Hitstun on Hit
        /// Also frame count for grabs
        /// Also stagger on hit
        /// </summary>
        public int StunOnHit { get; set; }
        /// <summary>
        /// Amount of Hitstun on block
        /// Also Stagger on block
        /// </summary>
        public int StunOnBlock { get; set; }
        /// <summary>
        /// Knockback on the move.
        /// </summary>
        public Vector2 Knockback { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">The classification for a given attack</param>
        /// <param name="prop">The properties of a given attack</param>
        /// <param name="jMod">The value we add to the jugglemeter upon a hit</param>
        /// <param name="dam">The damage the move does.</param>
        /// <param name="sMod">The modifier to scaling that the move applies</param>
        /// <param name="rHealth"> Amount of RedHealth this move will add to its opponents healthbar</param>
        /// <param name="stunOnHit"> Amount of Hitstun on Hit. Also frame count for grabs. Also stagger on hit</param>
        /// <param name="stunOnBlock">Amount of Hitstun on block. Also Stagger on block</param>
        public AttackInfo(AttackType type, ActionProperties prop, int jMod, float dam, float sMod, float rHealth, int stunOnHit, int stunOnBlock = 0, Vector2 knock  = new Vector2())
        {
            MyType = type;
            MyProperties = prop;
            JuggleMod = jMod;
            Damage = dam;
            ScalingMod = sMod;
            RedHealth = rHealth;
            StunOnHit = stunOnHit;
            StunOnBlock = stunOnBlock;
            Knockback = knock;
        }
    }
}
