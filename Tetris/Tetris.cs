using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Input;

namespace Tetris
{
    class Tetris
    {
        public Size Resolution;
        private List<TetrisSprite> LoadedSprites;
        private TetrisSprite PlayerSprite;
        private Rectangle FloorHitbox;
        bool[,] map = new bool[5, 10];

        //Used for loading in objects at the start of the game
        public void Load()
        {
            FloorHitbox = new Rectangle(0, 323, 168, 32);
            LoadedSprites = new List<TetrisSprite>();
            PlayerSprite = new TetrisSprite
            {
                SpriteImage = Properties.Resources.BlueTetris_1_png,
                width = 32,
                height = 32,
                x = 68,
                y = -28,
                velocity = 100,
                gravity = true
            };
            PlayerSprite.UpdateHitbox();
        }

        //Unloads the content when the GameLoop is stopped
        public void Unload()
        {
            LoadedSprites.Clear();
        }


        int tick;
        //Frequently executed method to update controller input and sprite animation
        public void Update(TimeSpan gameTime)
        {
            double gameTimeElapsed = gameTime.TotalMilliseconds / 1000;
            tick++;
            if (Keyboard.IsKeyDown(Key.D) && tick % 5 == 0 && PlayerSprite.x + 64 < Resolution.Width)
            {
                Rectangle rectangle = PlayerSprite.hitbox;
                rectangle.X += 32;
                if(!IsHitboxCollide(rectangle))
                    PlayerSprite.x += 32;
            }
            else if (Keyboard.IsKeyDown(Key.A) && tick % 5 == 0 && PlayerSprite.x - 16 > 0)
            {
                Rectangle rectangle = PlayerSprite.hitbox;
                rectangle.X -= 32;
                if(!IsHitboxCollide(rectangle))
                    PlayerSprite.x -= 32;
            }
            PlayerSprite.UpdateHitbox();
            if (tick == 60 || Keyboard.IsKeyDown(Key.S) && tick % 5 == 0)
            {
                foreach (TetrisSprite loadedSprite in LoadedSprites)
                {
                    if (PlayerSprite.hitbox.IntersectsWith(loadedSprite.hitbox))
                    {
                        PlayerSprite.gravity = false;
                    }
                }
                //If the tetris has been placed any where, it will run this
                if(!PlayerSprite.gravity)
                {
                    TetrisSprite loadedSprite = new TetrisSprite
                    {
                        SpriteImage = Properties.Resources.BlueTetris_1_png,
                        width = PlayerSprite.width,
                        height = PlayerSprite.height,
                        x = PlayerSprite.x,
                        y = PlayerSprite.y,
                        velocity = PlayerSprite.velocity,
                        gravity = false
                    };
                    map[PlayerSprite.x / 32, PlayerSprite.y / 32] = true;
                    loadedSprite.UpdateHitbox();
                    LoadedSprites.Add(loadedSprite);
                    TetrisPlaced(loadedSprite);
                    PlayerSprite = new TetrisSprite
                    {
                        SpriteImage = Properties.Resources.BlueTetris_1_png,
                        width = 32,
                        height = 32,
                        x = 68,
                        y = 4,
                        velocity = 100,
                        gravity = true
                    };
                }
                //If the tetris has not been placed, the tetris will continue falling
                else if (!PlayerSprite.hitbox.IntersectsWith(FloorHitbox))
                {
                    PlayerSprite.y += PlayerSprite.height;
                    PlayerSprite.UpdateHitbox();
                }
                //If the tetris has touched the floor and not another tetris, it will run this.
                else
                {
                    PlayerSprite.gravity = false;
                }
                tick = 0;
            }
            if(Keyboard.IsKeyDown(Key.Escape))
            {
                Unload();
            }
        }

        //Frequently executed method to draw sprites onto the screen
        public void Draw(Graphics gfx)
        {
            //Draws in all the background imagery
            gfx.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, Resolution.Width, Resolution.Height));
            gfx.DrawImage(Properties.Resources.Border, new Point(0, 0));
            gfx.FillRectangle(new SolidBrush(Color.DarkGray), FloorHitbox);

            //Draws in all sprites that are currently loaded
            foreach (TetrisSprite loadedSprite in LoadedSprites)
            {
                loadedSprite.Draw(gfx);
                gfx.DrawRectangle(new Pen(Color.Red), loadedSprite.hitbox);
            }
            PlayerSprite.Draw(gfx);

            bool IsRowFilled = false;
            for (int x = 0; x < Resolution.Width / 32; x++)
            {
                for (int y = 0; y < Resolution.Height / 32; y++)
                {
                    Rectangle r = new Rectangle((x * 32) + 4, (y * 32) + 4, 32, 32);
                    map[x, y] = IsHitboxCollide(r);
                    if (map[x, y])
                    {
                        gfx.DrawRectangle(new Pen(Color.Blue), r);
                    }
                    else
                    {
                        gfx.DrawRectangle(new Pen(Color.Green), r);
                    }
                }
            }
        }

        /*
         * @Param Rectangle - Hitbox to be checked
         * 
         * Compares given hitbox to all currently loaded and returns
         * whether or not given hitbox is intersecting any hitboxes
         * 
         * Returns true - Given hitbox is colliding
         */
        private bool IsHitboxCollide(Rectangle hitbox)
        {
            foreach(TetrisSprite loadedSprite in LoadedSprites)
            {
                if(hitbox.IntersectsWith(loadedSprite.hitbox))
                {
                    return true;
                }
            }
            return false;
        }

        private TetrisSprite GetTetrisAtRectangularLocation(Rectangle location)
        {
            foreach (TetrisSprite loadedSprite in LoadedSprites)
            {
                if (location.IntersectsWith(loadedSprite.hitbox))
                {
                    return loadedSprite;
                }
            }
            return null;
        }


        private void TetrisPlaced(TetrisSprite tetrisSprite)
        {
            int y = tetrisSprite.hitbox.Y/32;
            bool isRowFilled = true;
            List<Rectangle> toBeDeletedTetris = new List<Rectangle>();
            for(int x = 0; x < Resolution.Width/32; x++)
            {
                if(!map[x, y])
                {
                    isRowFilled = map[x, y];
                    break;
                }
                toBeDeletedTetris.Add(new Rectangle((x * 32) + 4, (y * 32) + 4, 32, 32));
            }
            if(isRowFilled)
            {
                foreach(Rectangle ToBeDeleted in toBeDeletedTetris)
                {
                    LoadedSprites.Remove(GetTetrisAtRectangularLocation(ToBeDeleted));
                    map[ToBeDeleted.X / 32, ToBeDeleted.Y / 32] = false;
                }
            }
        }
    }
}
