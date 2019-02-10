using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    class GinRummyRankComparer : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            // TODO: Relies on ordinal of Rank enum values
            return x.RankValue - y.RankValue;
        }
    }
}
