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
        /// The ID for this action used to look up drawing information.
        /// Not to be used by 
        /// </summary>
        public int ActionId { get; set; }
        /// <summary>
        /// The length in frames of the Action
        /// </summary>
        public int Frames { get; set; }
        /// <summary>
        /// The length in frames that have passed since the action was primed
        /// </summary>
        public int CurrentFrame { get; set; }
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
        public List<Tuple<int, Rectangle>> Hitboxes { get; set; }
        /// <summary>
        /// A list of hitboxes and their start frames in order
        /// </summary>
        public List<Tuple<int, Rectangle>> Hurtboxes { get; set; }
        /// <summary>
        /// The max number of times this action can hit
        /// </summary>
        public int MaxHits { get; set; }
        /// <summary>
        /// Number of times the action has hit
        /// </summary>
        public int CurrentHits { get; set; }
        /// <summary>
        /// Ways you can combo forward from your current action
        /// </summary>
        public List<Tuple<ActionCondition, Action>> ComboList { get; set; }
        /// <summary>
        /// Misc effects of the action on a given frame
        /// </summary>
        public List<Tuple<int, SimpleBehavior>> MiscBehaviors { get; set; }

        public Action(int frameLength, int juggleNum, int juggleMod, 
            List<Tuple<int, Rectangle>> hitboxes, List<Tuple<int, Rectangle>> hurtboxes, 
            int maxHits, List<Tuple<ActionCondition, Action>> comboList,
            List<Tuple<int, SimpleBehavior>> miscBehaviors)
        {
            Frames = frameLength;
            CurrentFrame = 0;
            JuggleNumber = juggleNum;
            JuggleMod = juggleMod;
            Hitboxes = hitboxes;
            Hurtboxes = hurtboxes;
            MaxHits = maxHits;
            CurrentHits = 0;
            ComboList = comboList;
            MiscBehaviors = miscBehaviors;
        }

        public void Prime()
        {
            CurrentFrame = 0;
            CurrentHits = 0;
        }

    }
}
