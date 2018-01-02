using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBN
{


    public enum AttackType
    {
        Strike,
        GrabVertical,
        GrabHorizontal,
        Stagger
    }

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
        /// Fuck if I know =P -Mark (Will update when I have a greater understanding of how red health works, feel free to update this if you know)
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
        /// 
        /// </summary>
        /// <param name="type">The classification for a given attack</param>
        /// <param name="prop">The properties of a given attack</param>
        /// <param name="jMod">The value we add to the jugglemeter upon a hit</param>
        /// <param name="dam">The damage the move does.</param>
        /// <param name="sMod">The modifier to scaling that the move applies</param>
        /// <param name="rHealth"></param>
        /// <param name="stunOnHit"> Amount of Hitstun on Hit. Also frame count for grabs. Also stagger on hit</param>
        /// <param name="stunOnBlock">Amount of Hitstun on block. Also Stagger on block</param>
        public AttackInfo(AttackType type, ActionProperties prop, int jMod, float dam, float sMod, float rHealth, int stunOnHit, int stunOnBlock = 0)
        {
            MyType = type;
            MyProperties = prop;
            JuggleMod = jMod;
            Damage = dam;
            ScalingMod = sMod;
            RedHealth = rHealth;
            StunOnHit = stunOnHit;
            StunOnBlock = stunOnBlock;
        }
    }
}
