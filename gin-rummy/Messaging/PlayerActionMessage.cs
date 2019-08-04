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
    /// Class for messages pertaining to an action a player has taken.
    /// </summary>
    public class PlayerRequestMessage : GameMessage
    {
        public enum PlayerRequestType { DrawDiscard, DrawStock, SetDiscard, Knock, MeldHand }

        public PlayerRequestType PlayerRequestTypeValue { get; set; }
        public Player Player { get; set; }// TODO: too much info? Currently mostly all we need is the name...
        public Card Card { get; set; }
        public MeldedHand MeldedHand { get; set; }

        public PlayerRequestMessage(PlayerRequestType playerRequest, Player player)
        {
            this.PlayerRequestTypeValue = playerRequest;
            this.Player = player;
        }

        public PlayerRequestMessage(PlayerRequestType playerRequest, Player player, Card card) : this(playerRequest, player)
        {
            this.Card = card;
        }

        public PlayerRequestMessage(PlayerRequestType playerRequest, Player player, MeldedHand hand) : this(playerRequest, player)
        {
            this.MeldedHand = hand;
        }
    }
}
