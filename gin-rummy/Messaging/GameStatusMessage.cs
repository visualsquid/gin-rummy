using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gin_rummy.Actors;

namespace gin_rummy.Messaging
{
    /// <summary>
    /// Class for messages pertaining to changes in game status.
    /// </summary>
    class GameStatusMessage : GameMessage
    {
        public enum GameStatusChange { GameInitialised, StartTurn }

        public GameStatusChange GameStatusChangeValue { get; set; }
        public Player Player { get; set; }

        public GameStatusMessage(GameStatusChange gameStatusChange, Player player)
        {
            this.GameStatusChangeValue = gameStatusChange;
            this.Player = player;
        }
    }
}
