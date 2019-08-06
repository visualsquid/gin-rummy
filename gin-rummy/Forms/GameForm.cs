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
using System.Threading;

namespace gin_rummy.Forms
{
    public partial class GameForm : Form, IGameStatusListener, IPlayerResponseListener
    {
        private readonly Queue<GameMessage> _pendingMessages;
        private readonly BackgroundWorker _messageHandler;

        private Game _game;
        private GameMaster _gameMaster;
        private GameLog _gameLog;
        private HumanPlayerGUIBased _player;

        public GameForm()
        {
            InitializeComponent();
            _pendingMessages = new Queue<GameMessage>();
            _messageHandler = new BackgroundWorker() { WorkerReportsProgress = false, WorkerSupportsCancellation = false };
            _messageHandler.DoWork += MessageHandler_DoWork;
            _messageHandler.RunWorkerAsync();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private SuitColourScheme GetSelectedSuitColourScheme()
        {
            if (Properties.Settings.Default.UseFourColourScheme)
            {
                return new FourColourScheme();
            }
            else
            {
                return new TwoColourScheme();
            }
        }

        private void InitialisePlayerCardPanel(CardPanel p)
        {
            p.ColourScheme = GetSelectedSuitColourScheme();
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
            pActions.OnKnock += PlayerEventKnock;
        }

        private void PlayerEventKnock()
        {
            // TODO: Disable controls
            _player.RequestKnock();
        }

        private void StacksEventStockDrawn()
        {
            // TODO: Disable controls
            _player.RequestDrawStock();
        }

        private void StacksEventDiscardTaken()
        {
            // TODO: Disable controls
            _player.RequestDrawDiscard();
        }

        private void StacksEventDiscardPlaced()
        {
            Card discard = pYourHand.GetSelectedCard();
            // TODO: Disable controls
            _player.RequestDiscard(discard);
        }

        private void CardPanelCardSelected(Card card, out bool removeCard)
        {
            removeCard = false; // TODO: Get rid of this parameter?
            StacksEventDiscardPlaced(); // TODO: Might not always want this action?
        }

        private void randomplayCPUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Remove/factor debug functionality (starting game with a specific hand)
            _player = new HumanPlayerGUIBased("Ya boi");
            _gameMaster = new GameMaster(_player, new RandomCPUPlayer("Dave"), new Hand(new Card[] { new Card("Ks"), new Card("Kd"), new Card("Kc"), new Card("Kh"), new Card("Qh"), new Card("Qs"), new Card("Qd"), new Card("Jh"), new Card("Js") }.ToList()), new Hand(new Card[] { new Card("Ts"), new Card("9s"), new Card("8s"), new Card("7s"), new Card("6s"), new Card("5s"), new Card("4s"), new Card("3s"), new Card("2s") }.ToList()));
            _game = _gameMaster.CurrentGame;
            _gameMaster.RegisterGameStatusListener(this);
            _gameMaster.RegisterPlayerResponseListener(this);
            InitialiseAndShowLogScreen();
            _gameMaster.StartGame();
        }

        private void InitialiseAndShowLogScreen()
        {
            _gameLog = new GameLog(_gameMaster, _game.PlayerOne, _game.PlayerTwo);
            _gameLog.StartPosition = FormStartPosition.Manual;
            _gameLog.Location = new Point(this.Right + 1, this.Top);
            _gameLog.Show();
        }

        private void GameForm_Shown(object sender, EventArgs e)
        {
            // TODO: remove auto-game start (for testing only)
            randomplayCPUToolStripMenuItem_Click(sender, e);
            //_gameMaster = new GameMaster(new RandomCPUPlayer("Teddy"), new RandomCPUPlayer("Dave"));
            //_gameMaster.GameFinished = new EventHandler(TestGameFinished);
            //_gameMaster.StartGame();
        }

        public void ReceiveMessage(GameStatusMessage message)
        {
            lock (_pendingMessages)
            {
                _pendingMessages.Enqueue(message);
            }
        }

        public void ReceiveMessage(PlayerResponseMessage response)
        {
            lock (_pendingMessages)
            {
                _pendingMessages.Enqueue(response);
            }
        }

        private MeldCreator ShowMeldCreator(Player player)
        {
            Form f = new Form();
            MeldCreator mc = new MeldCreator(new Hand(player.GetCards()), GetSelectedSuitColourScheme());
            f.Width = mc.Width;
            f.Height = mc.Height;
            mc.Dock = DockStyle.Fill;
            mc.OnUserAcceptsSelection = delegate () { f.Hide(); };
            f.Controls.Add(mc);
            f.ShowDialog();

            return mc;
        }

        private MeldCreator ShowLayoffControl(MeldedHand yourHand, MeldedHand opponentsHand)
        {
            Form f = new Form();
            MeldCreator mc = new MeldCreator(new MeldedHand(opponentsHand.Melds, yourHand.Deadwood), GetSelectedSuitColourScheme());
            f.Width = mc.Width;
            f.Height = mc.Height;
            mc.Dock = DockStyle.Fill;
            mc.OnUserAcceptsSelection = delegate () { f.Hide(); };
            f.Controls.Add(mc);
            f.ShowDialog();

            return mc;
        }

        private void MessageHandler_DoWork(object sender, DoWorkEventArgs e)
        {
            const int SleepTimeMs = 250;
            const int MaxBufferSize = 50;
            var buffer = new Queue<GameMessage>();

            while (true)
            {
                lock (_pendingMessages)
                {
                    for (int i = MaxBufferSize; i > 0 && _pendingMessages.Count > 0; i--)
                    {
                        buffer.Enqueue(_pendingMessages.Dequeue());
                    }
                }

                while (buffer.Count > 0)
                {
                    GameMessage nextMessage = buffer.Dequeue();

                    if (nextMessage is GameStatusMessage)
                    {
                        this.Invoke((MethodInvoker)delegate { HandleMessage(nextMessage as GameStatusMessage); });
                    }
                    else if (nextMessage is PlayerResponseMessage)
                    {
                        this.Invoke((MethodInvoker)delegate { HandleMessage(nextMessage as PlayerResponseMessage); }); // TODO: error here _sometimes_ when closing GameForm
                    }
                    else
                    {
                        throw new Exception("Unexpected error - unknown message type.");
                    }
                }

                Thread.Sleep(SleepTimeMs);
            }
        }

        private void HandleMessage(GameStatusMessage message)
        {
            switch (message.GameStatusChangeValue)
            {
                case GameStatusMessage.GameStatusChange.GameInitialised:
                    pYourHand.Clear();
                    InitialisePlayerCardPanel(pYourHand);
                    foreach (Card c in _player.GetCards())
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
                    if (message.Player == _player)
                    {
                        pActions.AllowTake = true;
                        pActions.AllowDraw = true;
                        pActions.AllowKnock = true;
                    }
                    else
                    {
                        pActions.AllowTake = false;
                        pActions.AllowDraw = false;
                        pActions.AllowKnock = false;
                    }
                    break;
                case GameStatusMessage.GameStatusChange.StartMeld:
                    if (message.Player == _player)
                    {
                        var meldCreator = ShowMeldCreator(_player);
                        // TODO: what if this fails? Stick it in a loop?
                        _player.RequestSetMelds(new MeldedHand(meldCreator.GetMeldedHand().Melds, meldCreator.GetMeldedHand().Deadwood));
                        //_gameMaster.RequestSetMelds(_game.PlayerOne, new MeldedHand(meldCreator.GetMeldedHand().Melds, meldCreator.GetMeldedHand().Deadwood), out error, out invalidMeld);
                    }
                    break;
                case GameStatusMessage.GameStatusChange.StartLayoff:
                    if (message.Player == _player)
                    {
                        var layoffControl = ShowLayoffControl(message.Player.MeldedHand, message.OpponentsMeldedHand); // Again, assuming player two is the opponent
                        // TODO: what if this fails? Stick it in a loop?
                        // TODO: Need a HumanPlayerGUIBased.RequestSetLayoffs or some such
                    }
                    break;
                default:
                    break;
            }
        }

        private void HandleMessage(PlayerResponseMessage response)
        {
            if (response.Response == PlayerResponseMessage.PlayerResponseType.Denied)
            {
                // Nothing to do - we'll get another message from the GameMaster to begin again
                return;
            }

            CardPanel relevantCardPanel = response.Player == _player ? pYourHand : pOpponentsHand;
            switch (response.Request.PlayerRequestTypeValue)
            {
                case PlayerRequestMessage.PlayerRequestType.DrawDiscard:
                    relevantCardPanel.AddCard(response.Card);
                    pStacks.DiscardCount--;
                    pStacks.VisibleDiscard = _game.GetVisibleDiscard();
                    pActions.AllowDraw = false;
                    pActions.AllowTake = false;
                    pActions.AllowDiscard = true;
                    break;
                case PlayerRequestMessage.PlayerRequestType.DrawStock:
                    pStacks.StockCount--;
                    relevantCardPanel.AddCard(response.Card);
                    pActions.AllowDraw = false;
                    pActions.AllowTake = false;
                    pActions.AllowDiscard = true;
                    break;
                case PlayerRequestMessage.PlayerRequestType.SetDiscard:
                    pStacks.DiscardCount++;
                    pStacks.VisibleDiscard = response.Card;
                    relevantCardPanel.RemoveCard(response.Card);
                    pActions.AllowDraw = false;
                    pActions.AllowTake = false;
                    pActions.AllowDiscard = false;
                    break;
                case PlayerRequestMessage.PlayerRequestType.Knock:
                    // Nothing required
                    break;
                default:
                    break;
            }
        }
    }
}
