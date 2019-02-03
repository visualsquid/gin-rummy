using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    public class Card
    {
        public enum Rank { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }

        public Suit Suit { get; set; }
        public Rank RankValue { get; set; }

        public Card(Suit suit, Rank rank)
        {
            this.Suit = suit;
            this.RankValue = rank;
        }

        public bool IsEqual(Card other)
        {
            return this.Suit.SuitTypeValue == other.Suit.SuitTypeValue && this.RankValue == other.RankValue;
        }
    }
}
