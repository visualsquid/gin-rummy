using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gin_rummy.Actors;
using gin_rummy.Cards;

namespace gin_rummy.Messaging
{
    /// <summary>
    /// Class for messages pertaining to an action a player has taken.
    /// </summary>
    class PlayerActionMessage : GameMessage
    {
        public enum PlayerAction { DrawDiscard, DrawStock, SetDiscard, Knock }

        public PlayerAction PlayerActionValue { get; set; }
        public Player Player { get; set; }
        public Card Card { get; set; }

        public PlayerActionMessage(PlayerAction playerAction, Player player) : this(playerAction, player, null) { }

        public PlayerActionMessage(PlayerAction playerAction, Player player, Card card)
        {
            this.PlayerActionValue = playerAction;
            this.Player = player;
            this.Card = card;
        }
    }
}
