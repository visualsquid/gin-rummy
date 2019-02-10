using gin_rummy.GameStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gin_rummy.Forms
{
    public partial class GameForm : Form
    {
        private Game _game;

        public GameForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void randomplayCPUToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
