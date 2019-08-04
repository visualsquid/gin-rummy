using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gin_rummy.Cards;
using gin_rummy.Actors;
using static gin_rummy.Messaging.PlayerRequestMessage;

namespace gin_rummy.Messaging
{
    public class PlayerResponseMessage : GameMessage
    {
        public enum PlayerResponseType { Accepted, Denied };

        public PlayerRequestMessage Request { get; set; }
        public PlayerResponseType Response { get; set; }
        public Player Player { get; set; }
        public string ErrorMessage { get; set; }
        public Card Card { get; set; }

        public PlayerResponseMessage(Player player, PlayerRequestMessage request, PlayerResponseType response)
        {
            Player = player;
            Request = request;
            Response = response;
        }

        public PlayerResponseMessage(Player player, PlayerRequestMessage request, PlayerResponseType response, Card card) : this(player, request, response)
        {
            Card = card;
        }

        public PlayerResponseMessage(Player player, PlayerRequestMessage request, PlayerResponseType response, string errorMessage) : this(player, request, response)
        {
            ErrorMessage = errorMessage;
        }
    }
}
