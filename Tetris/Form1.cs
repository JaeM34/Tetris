using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {

        Timer graphicsTimer;
        GameLoop gameLoop;

        public Form1()
        {
            InitializeComponent();
            Paint += Form1_Paint;

            graphicsTimer = new Timer();
            graphicsTimer.Interval = 1000 / 20;
            graphicsTimer.Tick += GraphicsTimer_Tick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Tetris myTetris = new Tetris();
            myTetris.Resolution = new Size(168, 328);

            gameLoop = new GameLoop();
            gameLoop.Load(myTetris);
            gameLoop.Start();

            graphicsTimer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            gameLoop.Draw(e.Graphics);
        }

        private void GraphicsTimer_Tick(Object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
