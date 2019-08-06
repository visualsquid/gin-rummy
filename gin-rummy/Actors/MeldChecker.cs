using gin_rummy.Cards;
using gin_rummy.GameStructures;
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

        private DeadWoodScorer _deadWoodScorer;

        public const int MinimumRunSize = 3;
        public const int MinimumSetSize = 3;

        public MeldChecker()
        {
            _deadWoodScorer = new DeadWoodScorer();
        }

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

        public List<Meld> GetAllPossibleMelds(List<Card> cards)
        {
            var melds = new List<Meld>();

            int count = (int)Math.Pow(2, cards.Count);
            for (int i = 1; i <= count - 1; i++)
            {
                string bitMask = Convert.ToString(i, 2).PadLeft(cards.Count, '0');

                Meld nextMeld = new Meld();
                for (int j = 0; j < bitMask.Length; j++)
                {
                    if (bitMask[j] == '1')
                    {
                        nextMeld.AddCard(cards[j]);
                    }
                }

                if (IsValid(nextMeld))
                {
                    melds.Add(nextMeld);
                }
            }

            return melds;
        }

        private bool DoAnyMeldsOverlap(List<Meld> melds)
        {
            for (int i = 0; i < melds.Count; i++)
            {
                for (int j = 0; j < melds.Count; j++)
                {
                    if (melds[i] != melds[j] && melds[i].DoesOverlap(melds[j]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool DoesMeldSetOmitPossibleMelds(List<Meld> meldSet, List<Card> originalCards)
        {
            var tempCards = new List<Card>(originalCards);
            foreach (Meld meld in meldSet)
            {
                foreach (Card card in meld.GetListOfCardsInMeld())
                {
                    tempCards.Remove(card);
                }
            }

            return GetAllPossibleMelds(tempCards).Count > 0;
        }

        public List<List<Meld>> GetAllPossibleMeldSets(List<Card> cards)
        {
            List<Meld> melds = GetAllPossibleMelds(cards);

            var meldsets = new List<List<Meld>>();

            int count = (int)Math.Pow(2, melds.Count); // TODO: this is too many combinations...
            for (int i = 1; i <= count - 1; i++)
            {
                string bitMask = Convert.ToString(i, 2).PadLeft(melds.Count, '0');

                var nextMeldSet = new List<Meld>();
                for (int j = 0; j < bitMask.Length; j++)
                {
                    if (bitMask[j] == '1')
                    {
                        nextMeldSet.Add(melds[j]);
                    }
                }

                if (!DoAnyMeldsOverlap(nextMeldSet) && !DoesMeldSetOmitPossibleMelds(nextMeldSet, cards))
                {
                    meldsets.Add(nextMeldSet);
                }

            }

            return meldsets;
        }

        public List<Card> GetDeadWood(List<Meld> meldSet, List<Card> originalCardsInHand)
        {
            var deadWood = new List<Card>(originalCardsInHand);
            foreach(Meld meld in meldSet)
            {
                var meldCards = meld.GetListOfCardsInMeld();
                deadWood.RemoveAll(i => meldCards.Contains(i));
            }
            return deadWood;
        }

        public List<Meld> GetBestMeldSet(Hand hand)
        {
            List<Card> cards = hand.ViewHand();
            List<List<Meld>> allPossibleMeldSets = GetAllPossibleMeldSets(cards);

            List<Meld> bestMeldSet = null;
            int lowestDeadWoodPenalty = int.MaxValue;
            foreach(List<Meld> meldSet in allPossibleMeldSets)
            {
                List<Card> deadWood = GetDeadWood(meldSet, cards);
                int nextDeadWoodPenalty = _deadWoodScorer.GetDeadWoodPenalty(deadWood);
                if (nextDeadWoodPenalty < lowestDeadWoodPenalty)
                {
                    lowestDeadWoodPenalty = nextDeadWoodPenalty;
                    bestMeldSet = meldSet;
                }
            }

            return bestMeldSet;
        }
    }
}
