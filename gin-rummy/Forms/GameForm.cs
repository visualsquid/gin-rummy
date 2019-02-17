using gin_rummy.Cards;
using gin_rummy.Controls;
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

        public GameForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private CardPanel InitialisePlayerCardPanel()
        {
            CardPanel p = new CardPanel();

            if (Properties.Settings.Default.UseFourColourScheme)
            {
                p.ColourScheme = new FourColourScheme();
            }
            else
            {
                p.ColourScheme = new TwoColourScheme();
            }

            p.ColourMap = new Dictionary<SuitColour, Color>();
            p.ColourMap.Add(SuitColour.Black, Color.LightSlateGray);
            p.ColourMap.Add(SuitColour.Blue, Color.LightSkyBlue);
            p.ColourMap.Add(SuitColour.Green, Color.LightGreen);
            p.ColourMap.Add(SuitColour.Red, Color.OrangeRed);

            p.CardSelected += CardPanelCardSelected;
            p.ShowCards = true;
            p.AllowReordering = true;
            p.AllowSelection = true;

            return p;
        }

        private CardPanel InitialiseOpponentCardPanel()
        {
            CardPanel p = new CardPanel();

            p.ShowCards = false;
            p.AllowReordering = false;
            p.AllowSelection = false;

            return p;
        }

        private void CardPanelCardSelected(Card card, out bool removeCard)
        {
            MessageBox.Show(card.ToString());
            removeCard = true;
        }

        private void randomplayCPUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Implement random play CPU game
            pYourHand.Controls.Clear();
            CardPanel yourCards = InitialisePlayerCardPanel();
            pYourHand.Controls.Add(yourCards);
            yourCards.Dock = DockStyle.Fill;

            pOpponentsHand.Controls.Clear();
            CardPanel opponentsCards = InitialiseOpponentCardPanel();
            pOpponentsHand.Controls.Add(opponentsCards);
            opponentsCards.Dock = DockStyle.Fill;

            Deck deck = new Deck();
            deck.Shuffle();

            int i = 10;
            while (i-- > 0)
            {
                yourCards.AddCard(deck.RemoveTop());
                opponentsCards.AddCard(deck.RemoveTop());
            }
        }


        private void GameForm_Shown(object sender, EventArgs e)
        {
            // TODO: remove auto-game start (for testing only)
            randomplayCPUToolStripMenuItem_Click(sender, e);
        }

        
    }
}
