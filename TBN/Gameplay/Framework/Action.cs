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
        public List<Tuple<int, Rectangle>> Hitboxes { get; set; }
        /// <summary>
        /// A list of hitboxes and their start frames in order
        /// </summary>
        public List<Tuple<int, Rectangle>> Hurtboxes { get; set; }
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


        public Action(int frameLength, int juggleNum, int juggleMod, 
            List<Tuple<int, Rectangle>> hitboxes, List<Tuple<int, Rectangle>> hurtboxes, List<Tuple<int, Vector2>> disp, 
            int maxHits, List<Tuple<ActionCondition, Action>> comboList,
            List<Tuple<int, SimpleBehavior>> miscBehaviors)
        {
            EndFrame = frameLength;
            JuggleNumber = juggleNum;
            JuggleMod = juggleMod;
            Hitboxes = hitboxes;
            Hurtboxes = hurtboxes;
            FrameDisplacement = disp;
            MaxHits = maxHits;
            ComboList = comboList;
            MiscBehaviors = miscBehaviors;
        }

    }
}
