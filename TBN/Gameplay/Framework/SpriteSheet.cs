using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TBN
{
    public class SpriteSheet
    {
        /// <summary>
        /// The texture to us to draw this spritesheet
        /// </summary>
        public Texture2D Sheet { get; set; }
        /// <summary>
        /// All of the draw info for all of the frames
        /// </summary>
        public FrameDrawInfo[][] FrameInfo { get; set; }


        public SpriteSheet(Texture2D sheet, FrameDrawInfo[][] info)
        {
            Sheet = sheet;
            FrameInfo = info;
        }



    }
}
