using gin_rummy.Cards;
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
using static gin_rummy.Cards.SuitColourScheme;

namespace gin_rummy.Forms
{
    public partial class GameForm : Form
    {
        private Game _game;
        private SuitColourScheme _suitColourScheme;
        private Dictionary<SuitColour, Color> _colourMap;

        public GameForm()
        {
            InitializeComponent();

            if (Properties.Settings.Default.UseFourColourScheme)
            {
                _suitColourScheme = new FourColourScheme();
            }
            else
            {
                _suitColourScheme = new TwoColourScheme();
            }

            _colourMap = new Dictionary<SuitColour, Color>();
            _colourMap.Add(SuitColour.Black, Color.LightSlateGray);
            _colourMap.Add(SuitColour.Blue, Color.LightSkyBlue);
            _colourMap.Add(SuitColour.Green, Color.LightGreen);
            _colourMap.Add(SuitColour.Red, Color.OrangeRed);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void randomplayCPUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Implement random play CPU game
            pYourCards.Controls.Clear();

            Deck deck = new Deck();
            deck.Shuffle();

            int i = 5;
            while (i-- > 0)
            {
                AddCardToPlayerHand(deck.RemoveTop());
            }
        }

        private void AddCardToPlayerHand(Card card)
        {
            Panel p = new Panel();
            p.Parent = pYourCards;
            p.Height = p.Parent.Height;
            p.Width = p.Height;
            p.BackColor = _colourMap[_suitColourScheme.GetColour(card.SuitValue)];
            p.MouseDown += CardPanelMouseDown;

            Label l = new Label();
            l.Parent = p;
            l.Font = new Font("Arial", 16, FontStyle.Bold);
            l.Text = card.ToString();
            l.Dock = DockStyle.Fill;
            l.TextAlign = ContentAlignment.MiddleCenter;

        }

        private void CardPanelMouseDown(object sender, MouseEventArgs e)
        {
            Panel p = (sender as Panel);
            //p.DoDragDrop(Pa)
                // TODO: implement drag and drop
        }
    }
}
