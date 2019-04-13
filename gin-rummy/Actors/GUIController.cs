using gin_rummy.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Actors
{
    public abstract class GUIController
    {
        public abstract void NotifyCardDrawn(Player player);
        public abstract void NotifyCardDiscarded(Player player);
        public abstract void NotifyStartPlayerTurn(Player player);
    }
}
