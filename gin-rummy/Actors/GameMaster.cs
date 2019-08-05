using gin_rummy.Cards;
using gin_rummy.GameStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using gin_rummy.Messaging;
using System.ComponentModel;

namespace gin_rummy.Actors
{
    /// <summary>
    /// Controller class for handling the game. Other actors should make requests of this class in order to progress the game.
    /// </summary>
    public class GameMaster : IPlayerRequestListener
    {

        public enum ActionErrorType
        {
            TooMuchDeadWood,
            NotYourTurn,
            MeldInvalid,
            LayOffInvalid
        }

        private class PlayerResults
        {
            public Player Player { get; set; }
            public List<Meld> Melds { get; set; }
            public List<Card> DeadWood { get; set; }
        }

        private Thread _turnHandler;
        private Thread _endGameHandler;
        private Thread _layOffsHandler;

        private readonly Queue<GameMessage> _pendingMessages;
        private BackgroundWorker _worker;
        private HashSet<IGameStatusListener> _statusListeners;
        private HashSet<IPlayerResponseListener> _responseListeners;
        private DeadWoodScorer _deadWoodScorer;
        private MeldChecker _meldChecker;

        private List<PlayerResults> _playerResults;
        private Hand _playerOneStartingHand; // TODO: remove/refactor debug functionality

        public List<string> Log { get; } // TODO: remove old log functionality
        public EventHandler GameFinished { get; set; }
        public Player CurrentPlayer { get; set; }
        public Game CurrentGame { get; private set; }

        public GameMaster(Player playerOne, Player playerTwo)
        {
            _pendingMessages = new Queue<GameMessage>();
            Log = new List<string>();
            _statusListeners = new HashSet<IGameStatusListener>();
            _responseListeners = new HashSet<IPlayerResponseListener>();
            _playerResults = new List<PlayerResults>();
            CurrentGame = new Game(playerOne, playerTwo);
            RegisterGameStatusListener(playerOne);
            RegisterPlayerResponseListener(playerOne);
            playerOne.RegisterRequestListener(this);
            RegisterGameStatusListener(playerTwo);
            RegisterPlayerResponseListener(playerTwo);
            playerTwo.RegisterRequestListener(this);
            _deadWoodScorer = new DeadWoodScorer();
            _meldChecker = new MeldChecker();
            _worker = new BackgroundWorker() { WorkerReportsProgress = false, WorkerSupportsCancellation = false };
            _worker.DoWork += BackgroundWorker_DoWork;
            _worker.RunWorkerAsync();
        }


        public GameMaster(Player playerOne, Player playerTwo, Hand playerOneStartingHand) : this(playerOne, playerTwo)  
        {
            // TODO: remove/refactor debug functionality
            _playerOneStartingHand = playerOneStartingHand;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
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

                    if (nextMessage is PlayerRequestMessage)
                    {
                        HandleMessage(nextMessage as PlayerRequestMessage);
                    }
                    else
                    {
                        throw new Exception("Unexpected error - unknown message type.");
                    }
                }

                Thread.Sleep(SleepTimeMs);
            }
        }

        private void HandleMessage(PlayerRequestMessage message)
        {
            switch (message.PlayerRequestTypeValue)
            {
                case PlayerRequestMessage.PlayerRequestType.DrawDiscard:
                    RequestDrawDiscard(message);
                    break;
                case PlayerRequestMessage.PlayerRequestType.DrawStock:
                    RequestDrawStock(message);
                    break;
                case PlayerRequestMessage.PlayerRequestType.SetDiscard:
                    RequestPlaceDiscard(message);
                    break;
                case PlayerRequestMessage.PlayerRequestType.Knock:
                    RequestKnock(message);
                    break;
                case PlayerRequestMessage.PlayerRequestType.MeldHand:
                    RequestSetMelds(message);
                    break;
                default:
                    break;
            }
        }

        private void InitialiseGame()
        {
            CurrentGame.ShuffleDeck();
            if (_playerOneStartingHand != null)
            {
                foreach (Card c in _playerOneStartingHand.ViewHand())
                {
                    CurrentGame.PlayerOne.DrawCard(c);
                }
            }
            else
            {
                CurrentGame.DealHand(CurrentGame.PlayerOne, Game.InitialHandSize);
            }
            CurrentGame.DealHand(CurrentGame.PlayerTwo, Game.InitialHandSize);
            CurrentGame.CreateStacks();
            NotifyGameStatusListeners(new GameStatusMessage(GameStatusMessage.GameStatusChange.GameInitialised, null));
        }

        public void StartGame()
        {
            InitialiseGame();
            StartTurn();
        }

        public void RegisterGameStatusListener(IGameStatusListener listener)
        {
            _statusListeners.Add(listener);
        }

        private void NotifyGameStatusListeners(GameStatusMessage message)
        {
            foreach(var listener in _statusListeners)
            {
                listener.ReceiveMessage(message);
            }
        }

        public void RegisterPlayerResponseListener(IPlayerResponseListener listener)
        {
            _responseListeners.Add(listener);
        }

        private void NotifyPlayerResponseListeners(PlayerResponseMessage message)
        {
            foreach (var listener in _responseListeners)
            {
                listener.ReceiveMessage(message);
            }
        }

        private void StartTurn()
        {
            //_turnHandler = new Thread(new ThreadStart(HandleTurns));
            //_turnHandler.Start();
            HandleTurns();
        }

        private void StartEndGame()
        {
            _endGameHandler = new Thread(new ThreadStart(HandleEndGame));
            _endGameHandler.Start();
        }

        private void StartLayOffs()
        {
            _layOffsHandler = new Thread(new ThreadStart(HandleLayOffs));
            _layOffsHandler.Start();
        }

        private void HandleTurns()
        {
            if (CurrentPlayer == null || CurrentPlayer == CurrentGame.PlayerTwo)
            {
                CurrentPlayer = CurrentGame.PlayerOne;
            }
            else if (CurrentPlayer == CurrentGame.PlayerOne)
            {
                CurrentPlayer = CurrentGame.PlayerTwo;
            }
            //CurrentPlayer.YourTurn(this); // TODO: Sort this out
            NotifyGameStatusListeners(new GameStatusMessage(GameStatusMessage.GameStatusChange.StartTurn, CurrentPlayer));
        }

        private void HandleEndGame()
        {
            CurrentPlayer.RequestMelds(this);
        }

        private void HandleLayOffs()
        {
            Player finalPlayer = CurrentPlayer == CurrentGame.PlayerOne ? CurrentGame.PlayerTwo : CurrentGame.PlayerOne;
            PlayerResults currentPlayerResults = _playerResults.First(i => i.Player == CurrentPlayer);
            finalPlayer.RequestLayOffs(this, currentPlayerResults.Melds);
        }

        public void RequestKnock(PlayerRequestMessage request)
        {
            string errorMessage = string.Empty;

            if (!ValidateCurrentPlayer(request.Player, out errorMessage))
            {
                NotifyPlayerResponseListeners(new PlayerResponseMessage(request.Player, request, PlayerResponseMessage.PlayerResponseType.Denied, errorMessage));
                NotifyGameStatusListeners(new GameStatusMessage(GameStatusMessage.GameStatusChange.StartTurn, request.Player)); // TODO: Not really fair to ask them to start their whole turn again, is it? Also won't work if they've drawn a card...
                return;
            }

            NotifyPlayerResponseListeners(new PlayerResponseMessage(request.Player, request, PlayerResponseMessage.PlayerResponseType.Accepted));

            NotifyGameStatusListeners(new GameStatusMessage(GameStatusMessage.GameStatusChange.StartMeld, CurrentGame.PlayerOne));
            NotifyGameStatusListeners(new GameStatusMessage(GameStatusMessage.GameStatusChange.StartMeld, CurrentGame.PlayerTwo));
        }

        public void RequestDrawDiscard(PlayerRequestMessage request)
        {
            Card drawnCard = null;
            string errorMessage = string.Empty;

            if (!ValidateCurrentPlayer(request.Player, out errorMessage) || !ValidateDiscardsExist(out errorMessage))
            {
                NotifyPlayerResponseListeners(new PlayerResponseMessage(request.Player, request, PlayerResponseMessage.PlayerResponseType.Denied, errorMessage));
                NotifyGameStatusListeners(new GameStatusMessage(GameStatusMessage.GameStatusChange.StartTurn, request.Player)); // TODO: Not really fair to ask them to start their whole turn again, is it?
                return;
            }

            drawnCard = CurrentGame.DrawDiscard();
            request.Player.DrawCard(drawnCard);

            NotifyPlayerResponseListeners(new PlayerResponseMessage(request.Player, request, PlayerResponseMessage.PlayerResponseType.Accepted, drawnCard));
        }

        public void RequestDrawStock(PlayerRequestMessage request)
        {
            Card drawnCard = null;
            string errorMessage = null;

            if (!ValidateCurrentPlayer(request.Player, out errorMessage))
            {
                NotifyPlayerResponseListeners(new PlayerResponseMessage(request.Player, request, PlayerResponseMessage.PlayerResponseType.Denied, errorMessage));
                NotifyGameStatusListeners(new GameStatusMessage(GameStatusMessage.GameStatusChange.StartTurn, request.Player)); // TODO: Not really fair to ask them to start their whole turn again, is it?
                return;
            }
            
            drawnCard = CurrentGame.DrawStock();
            request.Player.DrawCard(drawnCard);

            if (CurrentGame.GetStockCount() == 0)
            {
                CurrentGame.RestockFromDiscard();
            }

            NotifyPlayerResponseListeners(new PlayerResponseMessage(request.Player, request, PlayerResponseMessage.PlayerResponseType.Accepted, drawnCard));
        }

        public void RequestPlaceDiscard(PlayerRequestMessage request)
        {
            Card discard = request.Card;
            string errorMessage = string.Empty;

            if (!ValidateCurrentPlayer(request.Player, out errorMessage))
            {
                // Do nothing
            }
            else if (discard == null)
            {
                errorMessage = "Discard reference is null.";
            }

            if (errorMessage != string.Empty)
            {
                NotifyPlayerResponseListeners(new PlayerResponseMessage(request.Player, request, PlayerResponseMessage.PlayerResponseType.Denied, errorMessage));
                NotifyGameStatusListeners(new GameStatusMessage(GameStatusMessage.GameStatusChange.StartTurn, request.Player)); // TODO: Not really fair to ask them to start their whole turn again, is it?
                return;
            }
            
            CurrentGame.PlaceDiscard(discard);
            NotifyPlayerResponseListeners(new PlayerResponseMessage(request.Player, request, PlayerResponseMessage.PlayerResponseType.Accepted, discard));

            StartTurn();
        }

        public void RequestSetMelds(PlayerRequestMessage request)
        {
            MeldedHand hand = request.MeldedHand;
            Meld invalidMeld = null;
            string errorMessage = null;

            if (!ValidateCurrentPlayer(request.Player, out errorMessage))
            {
                // Do nothing
            }
            else if (hand == null)
            {
                errorMessage = "Melded hand reference is null.";
            }

            if (errorMessage != string.Empty)
            {
                NotifyPlayerResponseListeners(new PlayerResponseMessage(request.Player, request, PlayerResponseMessage.PlayerResponseType.Denied, errorMessage));
                NotifyGameStatusListeners(new GameStatusMessage(GameStatusMessage.GameStatusChange.StartMeld, request.Player)); // TODO: Will this work?
                return;
            }

            foreach (Meld meld in hand.Melds)
            {
                if (!_meldChecker.IsValid(meld))
                {
                    invalidMeld = meld;
                    errorMessage = "Meld is invalid.";
                    NotifyPlayerResponseListeners(new PlayerResponseMessage(request.Player, request, PlayerResponseMessage.PlayerResponseType.Denied, errorMessage));
                    return;
                }
            }

            request.Player.MeldHand(hand);
            NotifyPlayerResponseListeners(new PlayerResponseMessage(request.Player, request, PlayerResponseMessage.PlayerResponseType.Accepted));

            if (CurrentGame.PlayerOne.MeldedHand != null && CurrentGame.PlayerTwo.MeldedHand != null)
            {
                NotifyGameStatusListeners(new GameStatusMessage(GameStatusMessage.GameStatusChange.StartLayoff, CurrentGame.PlayerOne, CurrentGame.PlayerTwo.MeldedHand));
                NotifyGameStatusListeners(new GameStatusMessage(GameStatusMessage.GameStatusChange.StartLayoff, CurrentGame.PlayerTwo, CurrentGame.PlayerOne.MeldedHand));
            }
        }

        private bool CanKnock(Hand hand)
        {
            return _deadWoodScorer.GetDeadWoodPenalty(hand.ViewHand()) <= Game.MinimumDeadWoodForKnock;
        }

        private bool ValidateCurrentPlayer(Player player, out string error)
        {
            if (player == null)
            {
                error = "Player reference is null.";
                return false;
            }
            if (player != CurrentPlayer)
            {
                error = "It is not your turn.";
                return false;
            }
            else
            {
                error = "";
                return true;
            }
        }

        private bool ValidateDiscardsExist(out string error)
        {
            if (CurrentGame.GetDiscardCount() == 0)
            {
                error = "Discard pile is empty.";
                return false;
            }
            else
            {
                error = "";
                return true;
            }
        }

        public void ReceiveMessage(PlayerRequestMessage request)
        {
            lock (_pendingMessages)
            {
                _pendingMessages.Enqueue(request);
            }
        }

    }
}
