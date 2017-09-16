using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TBN
{
    public class FrameDrawInfo
    {
        /// <summary>
        /// The source rectangle on the texture 2D to draw.
        /// </summary>
        public Rectangle SourceRectangle { get; set; }
        /// <summary>
        /// The origin point to be used in the drawing. The point that corresponds to the anchor point
        /// </summary>
        public Vector2 Origin { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">The source rectangle on the texture 2D to draw.</param>
        /// <param name="org">The origin point to be used in the drawing. The point that corresponds to the anchor point</param>
        public FrameDrawInfo(Rectangle source, Vector2 org)
        {
            SourceRectangle = source;
            Origin = org;
        }
    }
}
