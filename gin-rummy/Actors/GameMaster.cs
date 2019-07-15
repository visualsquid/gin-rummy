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

        private Game _game;
        private DeadWoodScorer _deadWoodScorer;
        private MeldChecker _meldChecker;

        private List<PlayerResults> _playerResults;

        public List<string> Log { get; }
        public EventHandler GameFinished { get; set; }
        public Player CurrentPlayer { get; set; }

        public GameMaster(Player playerOne, Player playerTwo)
        {
            Log = new List<string>();
            _playerResults = new List<PlayerResults>();
            _playerResults.Add(new PlayerResults() { Player = playerOne });
            _playerResults.Add(new PlayerResults() { Player = playerTwo });
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

        private void StartLayOffs()
        {
            _layOffsHandler = new Thread(new ThreadStart(HandleLayOffs));
            _layOffsHandler.Start();
        }

        private void HandleTurns()
        {
            if (CurrentPlayer == null || CurrentPlayer == _game.PlayerTwo)
            {
                CurrentPlayer = _game.PlayerOne;
            }
            else if (CurrentPlayer == _game.PlayerOne)
            {
                CurrentPlayer = _game.PlayerTwo;
            }
            CurrentPlayer.YourTurn(this);
        }

        private void HandleEndGame()
        {
            CurrentPlayer.RequestMelds(this);
        }

        private void HandleLayOffs()
        {
            Player finalPlayer = CurrentPlayer == _game.PlayerOne ? _game.PlayerTwo : _game.PlayerOne;
            PlayerResults currentPlayerResults = _playerResults.First(i => i.Player == CurrentPlayer);
            finalPlayer.RequestLayOffs(this, currentPlayerResults.Melds);
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
            else
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
