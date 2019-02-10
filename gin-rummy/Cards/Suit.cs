using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    public class Suit
    {
        public enum SuitType { Hearts, Spades, Diamonds, Clubs }
        public enum SuitColour { Red, Black }

        public SuitType SuitTypeValue { get; set; }
        public SuitColour SuitColourValue { get; set; }

        private Suit (SuitType suitType, SuitColour suitColour)
        {
            this.SuitTypeValue = suitType;
            this.SuitColourValue = suitColour;
        }

        public static Suit Hearts()
        {
            return new Suit(SuitType.Hearts, SuitColour.Red);
        }

        public static Suit Diamonds()
        {
            return new Suit(SuitType.Diamonds, SuitColour.Red);
        }

        public static Suit Spades()
        {
            return new Suit(SuitType.Spades, SuitColour.Black);
        }

        public static Suit Clubs()
        {
            return new Suit(SuitType.Clubs, SuitColour.Black);
        }

        public static Suit GetSuit(SuitType suitType)
        {
            switch (suitType)
            {
                case SuitType.Hearts:
                    return Suit.Hearts();
                case SuitType.Spades:
                    return Suit.Spades();
                case SuitType.Diamonds:
                    return Suit.Diamonds();
                case SuitType.Clubs:
                    return Suit.Clubs();
                default:
                    return null;
            }
        }

    }
}
