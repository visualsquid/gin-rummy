using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    class Hand
    {
        private List<Card> _cards;

        public int Size { get { return _cards.Count; } }

        public Hand()
        {
            _cards = new List<Card>();
        }

        public List<Card> ViewHand()
        {
            var cards = new List<Card>();
            cards.AddRange(_cards);

            return cards;
        }

        public bool RemoveCard(Card card)
        {
            return _cards.Remove(card);
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        public void AddAtPosition(Card card, int i)
        {
            _cards.Insert(i, card);
        }

        public override string ToString()
        {
            return string.Join(" ", _cards.Select(i => i.ToString()));
        }
    }
}
