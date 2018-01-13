using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TBN
{
    public delegate void SimpleBehavior();

    [Flags] public enum ActionProperties
    {
        None = 0,
        AllowBlock = 1,
        UnGrabbable = 2,
        Unblockable = 4,
        Loops = 8,
        AirStagger = 16
    }

    public class Action
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
        public List<float[]> HitBoxMultipliers { get; set; }
        /// <summary>
        /// A list of hurtboxes and their start frames in order
        /// </summary>
        public List<Tuple<int, Rectangle[]>> Hurtboxes { get; set; }
        /// <summary>
        /// A list of multipliers on the hitbox damage
        /// </summary>
        public List<float[]> HurtBoxMultipliers { get; set; }
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
        ///  Amount of RedHealth this move will add to its opponents healthbar
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
        /// DEPRECIATED. A basic constructor that initializes the combolist. DEPRECIATED.
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
            ActionProperties prop = ActionProperties.None,
            AttackType type = AttackType.Strike,
            List<float[]> hitboxMultipliers = null,
            List<float[]> hurtboxMultipliers = null, 
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
            if(MiscBehaviors == null)
                MiscBehaviors = new List<Tuple<int, SimpleBehavior>>();
            Damage = damage;
            ScalingMod = scalMod;
            RedHealth = redHealth;
            MyProperties = prop;
            MyType = type;
            //Multipliers
            HitBoxMultipliers = hitboxMultipliers;
            if (HitBoxMultipliers == null)
                HitBoxMultipliers = new List<float[]>();
            while (HitBoxMultipliers.Count < hitboxes.Count)
            {
                HitBoxMultipliers.Add(new float[hitboxes[HitBoxMultipliers.Count].Item2.Length]);
                for (int i = 0; i < HitBoxMultipliers[HitBoxMultipliers.Count-1].Length; i++)
                {
                    HitBoxMultipliers[HitBoxMultipliers.Count - 1][i] = 1;
                }
            }
            HurtBoxMultipliers = hurtboxMultipliers;
            if (HurtBoxMultipliers == null)
                HurtBoxMultipliers = new List<float[]>();
            while (HurtBoxMultipliers.Count < hurtboxes.Count)
            {
                HurtBoxMultipliers.Add(new float[hurtboxes[HurtBoxMultipliers.Count].Item2.Length]);
                for (int i = 0; i < HurtBoxMultipliers[HurtBoxMultipliers.Count - 1].Length; i++)
                {
                    HurtBoxMultipliers[HurtBoxMultipliers.Count - 1][i] = 1;
                }
            }
            StunOnHit = stunOnHit;
            StunOnBlock = stunOnBlock;
        }

        /// <summary>
        /// Creates a blank action that does nothing
        /// </summary>
        /// <param name="actionID">The id for the action</param>
        public Action(int actionID, int length = 1)
        {
            ActionId = actionID;
            EndFrame = length;
            JuggleNumber = 0;
            JuggleMod = 0;
            Hitboxes = new List<Tuple<int, Rectangle[]>>();
            Hurtboxes = new List<Tuple<int, Rectangle[]>>();
            FrameDisplacement = new List<Tuple<int, Vector2>>();
            MaxHits = 0;
            ComboList = new List<Tuple<ActionCondition, Action>>();
            MiscBehaviors = new List<Tuple<int, SimpleBehavior>>();
            Damage = 0;
            ScalingMod = 0;
            RedHealth = 0;
            MyProperties = ActionProperties.None;
            MyType = AttackType.Strike;
            //Multipliers
            HitBoxMultipliers = new List<float[]>();
            HurtBoxMultipliers = new List<float[]>();
            StunOnHit = 0;
            StunOnBlock = 0;
        }

        /// <summary>
        /// Adds a hitbox keyframe to the action
        /// </summary>
        /// <param name="startFrame">Keyframe start frame</param>
        /// <param name="boxes">The hitboxes</param>
        /// <param name="multipliers">The multipliers for damage to this hitbox</param>
        public void AddHitboxKeyFrame(int startFrame, Rectangle[] boxes, float[] multipliers)
        {
            int index = Hitboxes.Count;
            while (index > 0 && startFrame < Hitboxes[index - 1].Item1)
            {
                index--;
            }
            Hitboxes.Insert(index, new Tuple<int, Rectangle[]>(startFrame, boxes));
            HitBoxMultipliers.Insert(index, multipliers);
        }

        /// <summary>
        /// Adds a hurtbox keyframe to the action
        /// </summary>
        /// <param name="startFrame">Keyframe start frame</param>
        /// <param name="boxes">The hurtboxes</param>
        /// <param name="multipliers">The multipliers for damage to this hurtbox</param>
        public void AddHurtboxKeyFrame(int startFrame, Rectangle[] boxes, float[] multipliers)
        {
            int index = Hurtboxes.Count;
            while (index > 0 && startFrame < Hurtboxes[index - 1].Item1)
            {
                index--;
            }
            Hurtboxes.Insert(index, new Tuple<int, Rectangle[]>(startFrame, boxes));
            HurtBoxMultipliers.Insert(index, multipliers);
        }

        /// <summary>
        /// Adds a keyframe for displacement
        /// </summary>
        /// <param name="startFrame">Keyframe start frame</param>
        /// <param name="displacement">Displacement for this period</param>
        public void AddDisplacementKeyFrame(int startFrame, Vector2 displacement)
        {
            int index = FrameDisplacement.Count;
            while (index > 0 && startFrame < FrameDisplacement[index - 1].Item1)
            {
                index--;
            }
            FrameDisplacement.Insert(index, new Tuple<int, Vector2>(startFrame, displacement));
        }

        /// <summary>
        /// Adds a Misc behavior at a particular frame
        /// </summary>
        /// <param name="frame">The frame the behavior occurs on. -1 for every frame.</param>
        /// <param name="behavior">The behavior to occur</param>
        public void AddMiscBehavior(int frame, SimpleBehavior behavior)
        {
            int index = MiscBehaviors.Count;
            while (index > 0 && frame < MiscBehaviors[index - 1].Item1)
            {
                index--;
            }
            MiscBehaviors.Insert(index, new Tuple<int, SimpleBehavior>(frame, behavior));
        }

    }
}
