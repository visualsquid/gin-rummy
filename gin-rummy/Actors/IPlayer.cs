using gin_rummy.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Actors
{
    interface IPlayer
    {
        int HandSize { get; }

        void ClearHand();
    }
}
