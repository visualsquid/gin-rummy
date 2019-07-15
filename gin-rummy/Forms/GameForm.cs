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
using gin_rummy.Messaging;

namespace gin_rummy.Forms
{
    public partial class GameForm : Form, IGameMessageListener
    {
        private Game _game;
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

        private void InitialiseStacks(int stockCount, Card visibleDiscard)
        {
            pStacks.StockCount = stockCount;
            pStacks.DiscardCount = 1;
            pStacks.VisibleDiscard = visibleDiscard;
            pStacks.StockDrawn = StacksEventStockDrawn;
            pStacks.DiscardTaken = StacksEventDiscardTaken;
        }

        private void InitialisePlayerActions()
        {
            pActions.OnTake += StacksEventDiscardTaken;
            pActions.OnDraw += StacksEventStockDrawn;
            pActions.OnDiscard += StacksEventDiscardPlaced;
        }

        private void StacksEventStockDrawn()
        {
            Card drawnCard;
            string error;

            if (!_gameMaster.RequestDrawStock(_gameMaster.CurrentPlayer, out drawnCard, out error))
            {
                MessageBox.Show($"Denied: {error}"); // TODO: what should we actually do here?
            }
        }

        private void StacksEventDiscardTaken()
        {
            Card drawnCard;
            string error;

            if (!_gameMaster.RequestDrawDiscard(_gameMaster.CurrentPlayer, out drawnCard, out error))
            {
                MessageBox.Show($"Denied: {error}"); // TODO: what should we actually do here?
            }
        }

        private void StacksEventDiscardPlaced()
        {
            string error;

            Card selectedCard = pYourHand.GetSelectedCard();
            if (!_gameMaster.RequestPlaceDiscard(_gameMaster.CurrentPlayer, selectedCard, out error))
            {
                MessageBox.Show($"Denied: {error}"); // TODO: what should we actually do here?
            }
        }

        private void CardPanelCardSelected(Card card, out bool removeCard)
        {
            removeCard = false; // TODO: Get rid of this parameter?
            StacksEventDiscardPlaced(); // TODO: Might not always want this action?
        }

        private void randomplayCPUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _gameMaster = new GameMaster(new HumanPlayerGUIBased("Ya boi"), new RandomCPUPlayer("Dave"));
            _game = _gameMaster.CurrentGame;
            _gameMaster.RegisterGameMessageListener(this);
            _gameMaster.StartGame();
        }


        private void GameForm_Shown(object sender, EventArgs e)
        {
            // TODO: remove auto-game start (for testing only)
            randomplayCPUToolStripMenuItem_Click(sender, e);
            //_gameMaster = new GameMaster(new RandomCPUPlayer("Teddy"), new RandomCPUPlayer("Dave"));
            //_gameMaster.GameFinished = new EventHandler(TestGameFinished);
            //_gameMaster.StartGame();
        }

        private void TestGameFinished(object sender, EventArgs e)
        {
            MessageBox.Show(string.Join("\n", _gameMaster.Log));
        }

        public void ReceiveMessage(GameMessage message)
        {
            HandleMessage(message);
        }

        private void HandleMessage(GameMessage message)
        {
            if (message is PlayerActionMessage)
            {
                PlayerActionMessage actionMessage = (PlayerActionMessage)message;
                CardPanel relevantCardPanel = actionMessage.Player == _game.PlayerOne ? pYourHand : pOpponentsHand;
                switch (actionMessage.PlayerActionValue)
                {
                    case PlayerActionMessage.PlayerAction.DrawDiscard:
                        relevantCardPanel.AddCard(actionMessage.Card);
                        pStacks.DiscardCount--;
                        pStacks.VisibleDiscard = _game.GetVisibleDiscard();
                        pActions.AllowDraw = false;
                        pActions.AllowTake = false;
                        pActions.AllowDiscard = true;
                        break;
                    case PlayerActionMessage.PlayerAction.DrawStock:
                        pStacks.StockCount--;
                        relevantCardPanel.AddCard(actionMessage.Card);
                        pActions.AllowDraw = false;
                        pActions.AllowTake = false;
                        pActions.AllowDiscard = true;
                        break;
                    case PlayerActionMessage.PlayerAction.SetDiscard:
                        pStacks.DiscardCount++;
                        pStacks.VisibleDiscard = actionMessage.Card;
                        relevantCardPanel.RemoveCard(actionMessage.Card);
                        pActions.AllowDraw = false;
                        pActions.AllowTake = false;
                        pActions.AllowDiscard = false;
                        break;
                    case PlayerActionMessage.PlayerAction.Knock:
                        // TODO: handle Knock message
                        break;
                    default:
                        break;
                }
            }
            else if (message is GameStatusMessage)
            {
                GameStatusMessage statusMessage = (GameStatusMessage)message;
                switch (statusMessage.GameStatusChangeValue)
                {
                    case GameStatusMessage.GameStatusChange.GameInitialised:
                        // TODO: assumes PlayerOne is always the human player
                        pYourHand.Clear();
                        InitialisePlayerCardPanel(pYourHand);
                        foreach (Card c in _game.PlayerOne.GetCards())
                        {
                            pYourHand.AddCard(c);
                        }

                        pOpponentsHand.Clear();
                        InitialiseOpponentCardPanel(pOpponentsHand);
                        foreach (Card c in _game.PlayerTwo.GetCards())
                        {
                            pOpponentsHand.AddCard(c);
                        }

                        InitialiseStacks(_game.GetStockCount(), _game.GetVisibleDiscard());
                        InitialisePlayerActions();
                        break;
                    case GameStatusMessage.GameStatusChange.StartTurn:
                        if (statusMessage.Player == _game.PlayerOne)
                        {
                            pActions.AllowTake = true;
                            pActions.AllowDraw = true;
                        }
                        else
                        {
                            pActions.AllowTake = false;
                            pActions.AllowDraw = false;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
