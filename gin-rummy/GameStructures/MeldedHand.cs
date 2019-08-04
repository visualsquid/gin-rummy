using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gin_rummy.Cards;

namespace gin_rummy.GameStructures
{
    /// <summary>
    /// Represents a hand that has been separated into a set of melds and the remaining deadwood, if any.
    /// </summary>
    public class MeldedHand
    {
        public List<Meld> Melds { get; set; }
        public List<Card> Deadwood{ get; set; }

        public MeldedHand()
        {
            Melds = new List<Meld>();
            Deadwood = new List<Card>();
        }

        public MeldedHand(List<Meld> melds, List<Card> deadwood) : this()
        {
            Melds.AddRange(melds);
            Deadwood.AddRange(deadwood);
        }
    }
}
