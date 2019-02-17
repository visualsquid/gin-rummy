using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gin_rummy.Controls
{
    public partial class PlayerActions : UserControl
    {

        public delegate void OnPlayerAction();

        public OnPlayerAction OnKnock { get; set; }
        public OnPlayerAction OnTake { get; set; }
        public OnPlayerAction OnDraw { get; set; }

        public bool AllowKnock { get { return bKnock.Enabled; } set { bKnock.Enabled = value; } }
        public bool AllowTake { get { return bTake.Enabled; } set { bTake.Enabled = value; } }
        public bool AllowDraw { get { return bDraw.Enabled; } set { bDraw.Enabled = value; } }

        public PlayerActions()
        {
            InitializeComponent();
            InitialiseDefaultPropertyValues();
        }

        public void InitialiseDefaultPropertyValues()
        {
            AllowKnock = false;
            AllowTake = false;
            AllowDraw = false;
        }

        private void bTake_Click(object sender, EventArgs e)
        {
            if (AllowTake && OnTake != null)
            {
                OnTake();
            }
        }

        private void bDraw_Click(object sender, EventArgs e)
        {
            if (AllowDraw && OnDraw != null)
            {
                OnDraw();
            }
        }

        private void bKnock_Click(object sender, EventArgs e)
        {
            if (AllowKnock && OnKnock != null)
            {
                OnKnock();
            }
        }
    }
}
