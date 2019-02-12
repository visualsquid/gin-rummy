using gin_rummy.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static gin_rummy.Cards.Card;

namespace gin_rummy.Actors
{
    /// <summary>
    /// Controller class for checking the validity of melds.
    /// </summary>
    public class MeldChecker
    {

        public const int MinimumRunSize = 3;
        public const int MinimumSetSize = 3;

        public MeldChecker() { }

        public bool IsRun(Meld m)
        {
            List<Card> cardsInMeld = m.GetListOfCardsInMeld();

            if (cardsInMeld.Count < MinimumRunSize)
            {
                return false;
            }

            Suit firstItemSuit = cardsInMeld[0].SuitValue;

            if (!cardsInMeld.All(c => c.SuitValue == firstItemSuit))
            {
                return false;
            }

            GinRummyRankComparer rankComparer = new GinRummyRankComparer();
            cardsInMeld.Sort(rankComparer);

            int i = 0;
            int j = i + 1;
            while (j < cardsInMeld.Count)
            {
                if (rankComparer.Compare(cardsInMeld[i], cardsInMeld[j]) != -1)
                {
                    return false;
                }

                i++;
                j++;
            }

            return true;
        }

        public bool IsSet(Meld m)
        {
            List<Card> cardsInMeld = m.GetListOfCardsInMeld();

            if (cardsInMeld.Count < MinimumSetSize)
            {
                return false;
            }

            Card.Rank firstItemRank = cardsInMeld[0].RankValue;

            if (cardsInMeld.Distinct(new GinRummySuitEqualityComparer()).Count() != cardsInMeld.Count)
            {
                return false;
            }

            return cardsInMeld.All(i => i.RankValue == firstItemRank);
        }

        public bool IsValid(Meld m)
        {
            return IsRun(m) || IsSet(m);
        }

    }
}
