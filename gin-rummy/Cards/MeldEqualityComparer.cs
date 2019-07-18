using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    public class MeldEqualityComparerDefault : IEqualityComparer<Meld>
    {
        public bool Equals(Meld x, Meld y)
        {
            return x.IsEqual(y);
        }

        public int GetHashCode(Meld obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
