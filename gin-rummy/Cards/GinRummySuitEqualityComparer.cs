using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    class GinRummySuitEqualityComparer : IEqualityComparer<Card>
    {
        public bool Equals(Card x, Card y)
        {
            return x.Suit.SuitTypeValue.Equals(y.Suit.SuitTypeValue);
        }

        public int GetHashCode(Card obj)
        {
            return obj.Suit.SuitTypeValue.GetHashCode();
        }
    }
}
