using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    public class Card
    {
        private static readonly Random _random = new Random();

        private Dictionary<string, Suit> _stringsToSuits;
        private Dictionary<string, Rank> _stringsToRanks;

        public enum Rank { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King } // Gin rummy rank ordering is implicit in this enum
        public enum Suit { Hearts, Spades, Diamonds, Clubs }

        public Suit SuitValue { get; set; }
        public Rank RankValue { get; set; }

        public Card() : this(GetRandomSuit(), GetRandomRank()) { }

        public Card(Suit suit) : this(suit, GetRandomRank()) { }

        public Card(Rank rank) : this(GetRandomSuit(), rank) { }

        public Card(Suit suit, Rank rank)
        {
            CreateMaps();
            this.SuitValue = suit;
            this.RankValue = rank;
        }

        public Card(string stringValue)
        {
            CreateMaps();

            string suit = stringValue[1].ToString();
            string rank = stringValue[0].ToString();

            this.SuitValue = _stringsToSuits[suit];
            this.RankValue = _stringsToRanks[rank];
        }

        private void CreateMaps()
        {
            CreateSuitMap();
            CreateRankMap();
        }

        private void CreateSuitMap()
        {
            _stringsToSuits = new Dictionary<string, Suit>();
            _stringsToSuits.Add("h", Suit.Hearts);
            _stringsToSuits.Add("d", Suit.Diamonds);
            _stringsToSuits.Add("s", Suit.Spades);
            _stringsToSuits.Add("c", Suit.Clubs);
        }

        private void CreateRankMap()
        {
            _stringsToRanks = new Dictionary<string, Rank>();
            _stringsToRanks.Add("A", Rank.Ace);
            _stringsToRanks.Add("2", Rank.Two);
            _stringsToRanks.Add("3", Rank.Three);
            _stringsToRanks.Add("4", Rank.Four);
            _stringsToRanks.Add("5", Rank.Five);
            _stringsToRanks.Add("6", Rank.Six);
            _stringsToRanks.Add("7", Rank.Seven);
            _stringsToRanks.Add("8", Rank.Eight);
            _stringsToRanks.Add("9", Rank.Nine);
            _stringsToRanks.Add("T", Rank.Ten);
            _stringsToRanks.Add("J", Rank.Jack);
            _stringsToRanks.Add("Q", Rank.Queen);
            _stringsToRanks.Add("K", Rank.King);
        }

        public bool IsEqual(Card other)
        {
            return this.SuitValue == other.SuitValue && this.RankValue == other.RankValue;
        }

        public override bool Equals(Object obj)
        {
            if (obj is Card)
            {
                return this.IsEqual((obj as Card));
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override string ToString()
        {
            string rank = _stringsToRanks.First(i => i.Value == RankValue).Key;
            string suit = _stringsToSuits.First(i => i.Value == SuitValue).Key;

            return $"{rank}{suit}";
        }

        public static Rank GetRandomRank()
        {
            int next = _random.Next(Enum.GetValues(typeof(Rank)).Length);

            return (Rank)next;
        }

        public static Suit GetRandomSuit()
        {
            Array availableValues = Enum.GetValues(typeof(Suit));
            IEnumerable<Suit> convertedValues = availableValues.Cast<Suit>();
            int next = _random.Next(availableValues.Length);

            return convertedValues.ElementAt(next);
        }
    }
}
