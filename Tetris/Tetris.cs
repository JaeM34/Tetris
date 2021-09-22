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
        private GameSprite TestSprite;
        private GameSprite Border;

        //Used for loading in objects at the start of the game
        public void Load()
        {
            TestSprite = new GameSprite();
            TestSprite.SpriteImage = Properties.Resources.BlueTetris_1_png;
            TestSprite.width = 32;
            TestSprite.height = 32;
            TestSprite.x = 50;
            TestSprite.y = 50;
            TestSprite.velocity = 100;

            Border = new GameSprite();
            Border.SpriteImage = Properties.Resources.Border;
            Border.width = 160;
            Border.height = 320;
            Border.x = 46;
            Border.y = 46;
            Border.velocity = 100;
        }

        //Unloads the content when the GameLoop is stopped
        public void Unload()
        {

        }

        int tick;
        //Frequently executed method to update controller input and sprite animation
        public void Update(TimeSpan gameTime)
        {
            tick++;
            Console.WriteLine(gameTime.TotalMilliseconds);
            double gameTimeElapsed = gameTime.TotalMilliseconds / 1000;
            if(tick == 60)
            {
                TestSprite.y += TestSprite.height;
                tick = 0;
            }
            //Console.WriteLine(_lastSecond);
        }
        
        //Frequently executed method to draw sprites onto the screen
        public void Draw(Graphics gfx)
        {
            gfx.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, Resolution.Width, Resolution.Height));

            Border.Draw(gfx);
            TestSprite.Draw(gfx);
        }
    }
}
