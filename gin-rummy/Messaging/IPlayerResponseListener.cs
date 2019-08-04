using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Messaging
{
    public interface IPlayerResponseListener
    {
        void ReceiveMessage(PlayerResponseMessage message);
    }
}
