using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gin_rummy.Actors;
using gin_rummy.Cards;
using gin_rummy.GameStructures;

namespace gin_rummy.Messaging
{
    /// <summary>
    /// Class for messages pertaining to changes in game status.
    /// </summary>
    public class GameStatusMessage : GameMessage
    {
        public enum GameStatusChange { GameInitialised, StartTurn, StartMeld, StartLayoff }

        public GameStatusChange GameStatusChangeValue { get; set; }
        public Player Player { get; set; } // TODO: too much info? Currently mostly all we need is the name...
        public MeldedHand OpponentsMeldedHand { get; set; }

        public GameStatusMessage(GameStatusChange gameStatusChange, Player player)
        {
            this.GameStatusChangeValue = gameStatusChange;
            this.Player = player;
        }
        
        public GameStatusMessage(GameStatusChange gameStatusChange, Player player, MeldedHand opponentsMeldedHand) : this(gameStatusChange, player)
        {
            this.OpponentsMeldedHand = opponentsMeldedHand;
        }
    }
}
