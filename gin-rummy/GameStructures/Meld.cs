using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    public class Meld
    {

        public const int MinimumRunSize = 3;
        public const int MinimumSetSize = 3;

        private readonly List<Card> _cards;

        public int Size { get { return _cards.Count; } }

        public Meld()
        {
            _cards = new List<Card>();
        }


        public void AddCard(Card c)
        {
            _cards.Add(c);
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public bool IsRun()
        {
            if (_cards.Count < MinimumRunSize)
            {
                return false;
            }

            Suit.SuitType firstItemSuit = _cards[0].Suit.SuitTypeValue;

            if (!_cards.All(c => c.Suit.SuitTypeValue == firstItemSuit))
            {
                return false;
            }

            GinRummyRankComparer rankComparer = new GinRummyRankComparer();
            _cards.Sort(rankComparer);

            int i = 0;
            int j = i + 1;
            while (j < _cards.Count)
            {
                if (rankComparer.Compare(_cards[j], _cards[i]) != 1)
                {
                    return false;
                }

                i++;
                j++;
            }

            return true;
        }

        public bool IsSet()
        {
            if (_cards.Count < MinimumSetSize)
            {
                return false;
            }

            Card.Rank firstItemRank = _cards[0].RankValue;

            if (_cards.Distinct(new GinRummySuitEqualityComparer()).Count() != _cards.Count)
            {
                return false;
            }

            return _cards.All(i => i.RankValue == firstItemRank);
        }

        public bool IsValid()
        {
            return IsRun() || IsSet();
        }
    }
}
