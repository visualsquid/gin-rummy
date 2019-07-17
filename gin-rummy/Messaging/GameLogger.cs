using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Messaging
{
    public abstract class GameLogger : IGameMessageListener
    {
        public abstract void ReceiveMessage(GameMessage message);
        public abstract void WriteLog(string message);
    }
}
