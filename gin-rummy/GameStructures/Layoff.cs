using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gin_rummy.Cards;

namespace gin_rummy.GameStructures
{
    /// <summary>
    /// Represents a single instance of a card being laid-off onto an opponent's meld.
    /// </summary>
    public class Layoff
    {
        public Meld Meld { get; set; }
        public Card Card { get; set; }
    }
}
