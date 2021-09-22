using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    class GameSprite
    {
        public Bitmap SpriteImage;
        public int x;
        public int y;
        public int width;
        public int height;
        public int velocity;

        public GameSprite()
        {

        }

        public void Draw(Graphics gfx)
        {
            gfx.DrawImage(SpriteImage, x, y, width, height);
        }

    }
}
