using gin_rummy.Cards;
using gin_rummy.GameStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using gin_rummy.Messaging;

namespace gin_rummy.Actors
{
    /// <summary>
    /// Controller class for handling the game. Other actors should make requests of this class in order to progress the game.
    /// </summary>
    public class GameMaster
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

        private HashSet<IGameMessageListener> _messageListeners;
        private DeadWoodScorer _deadWoodScorer;
        private MeldChecker _meldChecker;

        private List<PlayerResults> _playerResults;

        public List<string> Log { get; }
        public EventHandler GameFinished { get; set; }
        public Player CurrentPlayer { get; set; }
        public Game CurrentGame { get; private set; }

        public GameMaster(Player playerOne, Player playerTwo)
        {
            Log = new List<string>();
            _messageListeners = new HashSet<IGameMessageListener>();
            _playerResults = new List<PlayerResults>();
            _playerResults.Add(new PlayerResults() { Player = playerOne });
            _playerResults.Add(new PlayerResults() { Player = playerTwo });
            CurrentGame = new Game(playerOne, playerTwo);
            _deadWoodScorer = new DeadWoodScorer();
            _meldChecker = new MeldChecker();
        }

        private void InitialiseGame()
        {
            CurrentGame.ShuffleDeck();
            CurrentGame.DealHand(CurrentGame.PlayerOne, Game.InitialHandSize);
            CurrentGame.DealHand(CurrentGame.PlayerTwo, Game.InitialHandSize);
            CurrentGame.CreateStacks();
            NotifyGameMessageListeners(new GameStatusMessage(GameStatusMessage.GameStatusChange.GameInitialised, null));
        }

        public void StartGame()
        {
            InitialiseGame();
            StartTurn();
        }

        public void RegisterGameMessageListener(IGameMessageListener listener)
        {
            _messageListeners.Add(listener);
        }

        private void NotifyGameMessageListeners(GameMessage message)
        {
            foreach(IGameMessageListener listener in _messageListeners)
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
            CurrentPlayer.YourTurn(this);
            NotifyGameMessageListeners(new GameStatusMessage(GameStatusMessage.GameStatusChange.StartTurn, CurrentPlayer));
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

        public bool RequestKnock(Player player, out string error)
        {
            if (!ValidateCurrentPlayer(player, out error))
            {
                return false;
            }
            else
            {
                NotifyGameMessageListeners(new PlayerActionMessage(PlayerActionMessage.PlayerAction.Knock, player));
                StartEndGame();
                return true;
            }
        }

        public bool RequestDrawDiscard(Player player, out Card drawnCard, out string error)
        {
            drawnCard = null;
            if (!ValidateCurrentPlayer(player, out error))
            {
                return false;
            }
            else if (!ValidateDiscardsExist(out error))
            {
                return false;
            }
            else
            {
                drawnCard = CurrentGame.DrawDiscard();
                player.DrawCard(drawnCard);
                NotifyGameMessageListeners(new PlayerActionMessage(PlayerActionMessage.PlayerAction.DrawDiscard, player, drawnCard));
                return true;
            }
        }

        public bool RequestDrawStock(Player player, out Card drawnCard, out string error)
        {
            if (!ValidateCurrentPlayer(player, out error))
            {
                drawnCard = null;
                return false;
            }
            else
            {
                drawnCard = CurrentGame.DrawStock();
                player.DrawCard(drawnCard);
                if (CurrentGame.GetStockCount() == 0)
                {
                    CurrentGame.RestockFromDiscard();
                }
                NotifyGameMessageListeners(new PlayerActionMessage(PlayerActionMessage.PlayerAction.DrawStock, player, drawnCard));
                return true;
            }
        }

        public bool RequestPlaceDiscard(Player player, Card discard, out string error)
        {
            if (!ValidateCurrentPlayer(player, out error))
            {
                return false;
            }
            else if (discard == null)
            {
                error = "Discard reference is null.";
                return false;
            }
            else
            {
                CurrentGame.PlaceDiscard(discard);
                NotifyGameMessageListeners(new PlayerActionMessage(PlayerActionMessage.PlayerAction.SetDiscard, player, discard));
                StartTurn();
                return true;
            }
        }

        public bool RequestSetMelds(Player player, List<Meld> melds, List<Card> deadWood, out string error, out Meld invalidMeld)
        {
            invalidMeld = null;

            if (!ValidateCurrentPlayer(player, out error))
            {
                return false;
            }
            else if (melds == null)
            {
                error = "Meld list reference is null.";
                return false;
            }
            else if (deadWood == null)
            {
                error = "Dead wood list reference is null.";
                return false;
            }

            foreach (Meld meld in melds)
            {
                if (!_meldChecker.IsValid(meld))
                {
                    invalidMeld = meld;
                    error = "Meld is invalid.";
                    return false;
                }
            }

            var relevantResults = _playerResults.First(i => i.Player == player);
            relevantResults.Melds = melds;
            relevantResults.DeadWood = deadWood;
            // TODO: add log
            StartLayOffs();
            return true;
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

    }
}
