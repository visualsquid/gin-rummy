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

        public override string ToString()
        {
            string suit, rank;

            switch (Suit.SuitTypeValue)
            {
                case Suit.SuitType.Hearts:
                    suit = "h";
                    break;
                case Suit.SuitType.Spades:
                    suit = "s";
                    break;
                case Suit.SuitType.Diamonds:
                    suit = "d";
                    break;
                case Suit.SuitType.Clubs:
                    suit = "c";
                    break;
                default:
                    throw new Exception($"Invalid suit{Suit.SuitTypeValue.ToString()}");
            }

            switch (RankValue)
            {
                case Rank.Ace:
                    rank = "A";
                    break;
                case Rank.Two:
                    rank = "2";
                    break;
                case Rank.Three:
                    rank = "3";
                    break;
                case Rank.Four:
                    rank = "4";
                    break;
                case Rank.Five:
                    rank = "5";
                    break;
                case Rank.Six:
                    rank = "6";
                    break;
                case Rank.Seven:
                    rank = "7";
                    break;
                case Rank.Eight:
                    rank = "8";
                    break;
                case Rank.Nine:
                    rank = "9";
                    break;
                case Rank.Ten:
                    rank = "10";
                    break;
                case Rank.Jack:
                    rank = "J";
                    break;
                case Rank.Queen:
                    rank = "Q";
                    break;
                case Rank.King:
                    rank = "K";
                    break;
                default:
                    throw new Exception($"Invalid rank{RankValue.ToString()}");

            }

            return $"{rank}{suit}";
        }
    }
}
