using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static gin_rummy.Cards.Card;

namespace gin_rummy.Cards
{
    public abstract class SuitColourScheme
    {
        protected Dictionary<Suit, SuitColour> _colourMap;

        public enum SuitColour { Red, Black, Green, Blue }

        public SuitColourScheme()
        {
            _colourMap = new Dictionary<Suit, SuitColour>();
        }

        public SuitColour GetColour(Suit suit)
        {
            return _colourMap[suit];
        }
    }
}
