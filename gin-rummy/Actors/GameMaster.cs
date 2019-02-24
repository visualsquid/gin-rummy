using gin_rummy.Cards;
using gin_rummy.GameStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace gin_rummy.Actors
{
    public class GameMaster
    {

        public enum ActionErrorType
        {
            TooMuchDeadWood,
            NotYourTurn,
            MeldInvalid,
            LayOffInvalid
        }

        private Thread _turnHandler;
        private Thread _endGameHandler;
        private Game _game;
        private DeadWoodScorer _deadWoodScorer;
        private MeldChecker _meldChecker;

        private Player _currentPlayer;

        public List<string> Log { get; }
        public EventHandler GameFinished { get; set; }

        public GameMaster(Player playerOne, Player playerTwo)
        {
            Log = new List<string>();
            _game = new Game(playerOne, playerTwo);
            _deadWoodScorer = new DeadWoodScorer();
            _meldChecker = new MeldChecker();
            InitialiseGame();
        }

        private void InitialiseGame()
        {
            _game.ShuffleDeck();
            _game.DealHand(_game.PlayerOne, Game.InitialHandSize);
            _game.DealHand(_game.PlayerTwo, Game.InitialHandSize);
            _game.CreateStacks();
        }

        public void StartGame()
        {
            StartTurn();
        }

        private void StartTurn()
        {
            _turnHandler = new Thread(new ThreadStart(HandleTurns));
            _turnHandler.Start();
        }

        private void StartEndGame()
        {
            _endGameHandler = new Thread(new ThreadStart(HandleEndGame));
            _endGameHandler.Start();
        }

        private void HandleTurns()
        {
            if (_currentPlayer == null || _currentPlayer == _game.PlayerTwo)
            {
                _currentPlayer = _game.PlayerOne;
            }
            else if (_currentPlayer == _game.PlayerOne)
            {
                _currentPlayer = _game.PlayerTwo;
            }
            _currentPlayer.YourTurn(this);
        }

        private void HandleEndGame()
        {
            // TODO: implement end game
            Log.Add("Game ends.");
            if (GameFinished != null)
            {
                GameFinished(this, new EventArgs());
            }
        }

        private void LogKnock(string playerName)
        {
            Log.Add($"Player {playerName} knocks.");
        }

        private void LogDrawDiscard(string playerName)
        {
            Log.Add($"Player {playerName} draws discard.");
        }

        private void LogDrawStock(string playerName)
        {
            Log.Add($"Player {playerName} draws stock.");
        }

        private void LogPlaceDiscard(string playerName, string cardName)
        {
            Log.Add($"Player {playerName} discards {cardName}.");
        }

        public bool RequestKnock(Player player, out string error)
        {
            if (!ValidateCurrentPlayer(player, out error))
            {
                return false;
            }
            else
            {
                LogKnock(player.Name);
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
            {
                drawnCard = _game.DrawDiscard();
                player.DrawCard(drawnCard);
                LogDrawDiscard(player.Name);
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
                drawnCard = _game.DrawStock();
                player.DrawCard(drawnCard);
                if (_game.GetStockCount() == 0)
                {
                    _game.RestockFromDiscard();
                }
                LogDrawStock(player.Name);
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
                _game.PlaceDiscard(discard);
                LogPlaceDiscard(player.Name, discard.ToString());
                StartTurn();
                return true;
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
            if (player != _currentPlayer)
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
            if (_game.GetDiscardCount() == 0)
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
