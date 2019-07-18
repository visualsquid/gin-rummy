using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    public class CardEqualityComparerSuitAndRank : IEqualityComparer<Card>
    {

        public bool Equals(Card x, Card y)
        {
            return x.SuitValue == y.SuitValue && x.RankValue == y.RankValue;
        }

        public int GetHashCode(Card obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
