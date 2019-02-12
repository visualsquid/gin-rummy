using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static gin_rummy.Cards.Card;

namespace gin_rummy.Cards
{
    public class TwoColourScheme : SuitColourScheme
    {
        public TwoColourScheme() : base()
        {
            _colourMap.Add(Suit.Hearts, SuitColour.Red);
            _colourMap.Add(Suit.Diamonds, SuitColour.Red);
            _colourMap.Add(Suit.Spades, SuitColour.Black);
            _colourMap.Add(Suit.Clubs, SuitColour.Black);
        }
    }
}
