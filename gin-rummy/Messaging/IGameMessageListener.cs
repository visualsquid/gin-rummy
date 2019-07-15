using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Messaging
{
    /// <summary>
    /// Interface to be implemented if a class wishes to listen for messages from the GameMaster.
    /// </summary>
    public interface IGameMessageListener
    {
        void ReceiveMessage(GameMessage message);
    }
}
