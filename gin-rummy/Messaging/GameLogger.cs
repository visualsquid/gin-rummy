using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Messaging
{
    public abstract class GameLogger : IGameStatusListener, IPlayerRequestListener, IPlayerResponseListener
    {
        public abstract void ReceiveMessage(GameStatusMessage message);
        public abstract void ReceiveMessage(PlayerRequestMessage message);
        public abstract void ReceiveMessage(PlayerResponseMessage message);
        public abstract void WriteLog(string message);
    }
}
