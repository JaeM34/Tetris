using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    class GameLoop
    {
        private Tetris _tetris;

        public Boolean Running;

        public void Load(Tetris tetris)
        {
            _tetris = tetris;
        }

        public async void Start()
        {
            if(_tetris == null)
            {
                throw new ArgumentException("Game not loaded");
            }

            _tetris.Load();

            Running = true;

            DateTime _previousGameTime = DateTime.Now;

            while(Running)
            {
                TimeSpan GameTime = DateTime.Now - _previousGameTime;
                _previousGameTime = _previousGameTime + GameTime;
                _tetris.Update(GameTime);
                await Task.Delay(8);
            }
        }

        public void Stop()
        {
            Running = false;
            _tetris.Unload();
        }

        public void Draw(Graphics gfx)
        {
            _tetris.Draw(gfx);
        }
    }
}
