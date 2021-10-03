using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    class TetrisSprite
    {
        public Bitmap SpriteImage;
        public int x;
        public int y;
        public int width;
        public int height;
        public int velocity;
        public Rectangle hitbox;
        public bool gravity;

        public TetrisSprite()
        {
        }

        public void UpdateHitbox()
        {
            hitbox = new Rectangle(x, y, width, height+1);
        }

        public void Draw(Graphics gfx)
        {
            gfx.DrawImage(SpriteImage, x, y, width, height);
        }

    }
}
