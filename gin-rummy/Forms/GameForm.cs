using gin_rummy.Actors;
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
        private Deck _deck;
        private GameMaster _gameMaster;

        public GameForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitialisePlayerCardPanel(CardPanel p)
        {
            if (Properties.Settings.Default.UseFourColourScheme)
            {
                p.ColourScheme = new FourColourScheme();
            }
            else
            {
                p.ColourScheme = new TwoColourScheme();
            }

            p.CardSelected += CardPanelCardSelected;
            p.ShowCards = true;
            p.AllowReordering = true;
            p.AllowSelection = true;
        }

        private void InitialiseOpponentCardPanel(CardPanel p)
        {
            p.ShowCards = false;
            p.AllowReordering = false;
            p.AllowSelection = false;
        }

        private void InitialiseStacks(int stockCount)
        {
            pStacks.StockCount = stockCount;
            pStacks.DiscardCount = 1;
            pStacks.VisibleDiscard = _deck.RemoveTop();
            pStacks.AllowDrawStock = true;
            pStacks.StockDrawn = StacksEventStockDrawn;
            pStacks.AllowTakeDiscard = true;
            pStacks.DiscardTaken = StacksEventDiscardTaken;
        }

        private void InitialisePlayerActions()
        {
            pActions.AllowTake = true;
            pActions.OnTake += StacksEventDiscardTaken;
            pActions.AllowDraw = true;
            pActions.OnDraw += StacksEventStockDrawn;
        }

        private void StacksEventStockDrawn()
        {
            if (pStacks.StockCount > 0)
            {
                pStacks.StockCount--;
                MessageBox.Show($"New stock count = {pStacks.StockCount}");
            }
        }

        private void StacksEventDiscardTaken()
        {
            if (pStacks.DiscardCount > 0)
            {
                pStacks.DiscardCount--;
                MessageBox.Show($"New discard count = {pStacks.DiscardCount}");
            }
        }

        private void CardPanelCardSelected(Card card, out bool removeCard)
        {
            MessageBox.Show(card.ToString());
            removeCard = true;
        }

        private void randomplayCPUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Implement random play CPU game
            pYourHand.Clear();
            InitialisePlayerCardPanel(pYourHand);
            
            pOpponentsHand.Clear();
            InitialiseOpponentCardPanel(pOpponentsHand);

            _deck = new Deck();
            _deck.Shuffle();

            int i = 10;
            while (i-- > 0)
            {
                pYourHand.AddCard(_deck.RemoveTop());
                pOpponentsHand.AddCard(_deck.RemoveTop());
            }

            InitialiseStacks(_deck.Size);
            InitialisePlayerActions();
        }


        private void GameForm_Shown(object sender, EventArgs e)
        {
            // TODO: remove auto-game start (for testing only)
            //randomplayCPUToolStripMenuItem_Click(sender, e);
            _gameMaster = new GameMaster(new RandomCPUPlayer("Teddy"), new RandomCPUPlayer("Dave"));
            _gameMaster.GameFinished = new EventHandler(TestGameFinished);
            _gameMaster.StartGame();
        }

        private void TestGameFinished(object sender, EventArgs e)
        {
            MessageBox.Show(string.Join("\n", _gameMaster.Log));
        }
        
    }
}
