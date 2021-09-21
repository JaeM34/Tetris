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
        public Form1()
        {
            InitializeComponent();
            Main.init();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }

    /* the main class for running Tetris*/
    public static class Main
    {
        /* dude whaaaat, that's so weird, wtf */
        /* creating the grid for the blocks to be inside */
        private static int[,] _map;

        public static void init()
        {
            _map = new int[5, 10];
            System.Console.WriteLine("Hello world");
        }
    }
}
