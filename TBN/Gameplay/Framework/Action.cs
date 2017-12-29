using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TBN
{
    public delegate void SimpleBehavior();


    public class Action
    {
        /// <summary>
        /// The classification for a given attack
        /// </summary>
        public AttackType MyType { get; set; }
        /// <summary>
        /// The properties of a given attack
        /// </summary>
        public AttackProperties MyProperties { get; set; }
        /// <summary>
        /// The ID for this action used to look up drawing information.
        /// </summary>
        public int ActionId { get; set; }
        /// <summary>
        /// The length in frames of the Action
        /// </summary>
        public int EndFrame { get; set; }
        /// <summary>
        /// The value the opponents jugglemeter must be below in order to avoid whiffing
        /// </summary>
        public int JuggleNumber { get; set; }
        /// <summary>
        /// The value we add to the jugglemeter upon a hit
        /// </summary>
        public int JuggleMod { get; set; }
        /// <summary>
        /// A list of hitboxes and their start frames in order
        /// </summary>
        public List<Tuple<int, Rectangle[]>> Hitboxes { get; set; }
        /// <summary>
        /// A list of multipliers on the hitbox damage
        /// </summary>
        public List<float> HitBoxMultipliers { get; set; }
        /// <summary>
        /// A list of hurtboxes and their start frames in order
        /// </summary>
        public List<Tuple<int, Rectangle[]>> Hurtboxes { get; set; }
        /// <summary>
        /// A list of movement vectors and their start frames in order
        /// </summary>
        public List<Tuple<int, Vector2>> FrameDisplacement { get; set; }
        /// <summary>
        /// The max number of times this action can hit
        /// </summary>
        public int MaxHits { get; set; }
        /// <summary>
        /// Ways you can combo forward from your current action
        /// </summary>
        public List<Tuple<ActionCondition, Action>> ComboList { get; set; }
        /// <summary>
        /// Misc effects of the action on a given frame
        /// </summary>
        public List<Tuple<int, SimpleBehavior>> MiscBehaviors { get; set; }
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
        /// A basic constructor that initializes the combolist.
        /// </summary>
        /// <param name="actionID">The id that represents the action.</param>
        /// <param name="frameLength">Length in frames of the action.</param>
        /// <param name="juggleNum"></param>
        /// <param name="juggleMod"></param>
        /// <param name="hitboxes">The damage taking colliders on the action</param>
        /// <param name="hurtboxes">The damage dealing colliders on the action</param>
        /// <param name="disp">The displacements by frame of the action using a keyframe system</param>
        /// <param name="maxHits">The max number of times a move can hit</param>
        /// <param name="miscBehaviors">Any behaivior that you want that is not covered elsewhere</param>
        /// <param name="damage">The amount of damage the move does.</param>
        public Action(int actionID, int frameLength, int juggleNum, int juggleMod,
            List<Tuple<int, Rectangle[]>> hitboxes, List<Tuple<int, Rectangle[]>> hurtboxes, List<Tuple<int, Vector2>> disp,
            int maxHits,
            List<Tuple<int, SimpleBehavior>> miscBehaviors,
            float damage = 0, float scalMod = 0, float redHealth = 0,
            AttackProperties prop = AttackProperties.None,
            AttackType type = AttackType.Strike,
            List<float> hitboxMultipliers = null,
            int stunOnHit = 0,
            int stunOnBlock = 0)
        {
            ActionId = actionID;
            EndFrame = frameLength;
            JuggleNumber = juggleNum;
            JuggleMod = juggleMod;
            Hitboxes = hitboxes;
            Hurtboxes = hurtboxes;
            FrameDisplacement = disp;
            MaxHits = maxHits;
            ComboList = new List<Tuple<ActionCondition, Action>>();
            MiscBehaviors = miscBehaviors;
            Damage = damage;
            ScalingMod = scalMod;
            RedHealth = redHealth;
            MyProperties = prop;
            MyType = type;
            //Multipliers
            HitBoxMultipliers = hitboxMultipliers;
            if (HitBoxMultipliers == null)
                HitBoxMultipliers = new List<float>();
            while(HitBoxMultipliers.Count < hitboxes.Count)
            {
                HitBoxMultipliers.Add(1);
            }
            StunOnHit = stunOnHit;
            StunOnBlock = stunOnBlock;
        }


    }
}
