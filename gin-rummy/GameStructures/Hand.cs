using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    public class Hand
    {
        private List<Card> _cards;

        public int Size { get { return _cards.Count; } }

        public Hand()
        {
            _cards = new List<Card>();
        }

        public Hand(List<Card> cards)
        {
            _cards = new List<Card>();
            _cards.AddRange(cards.Select(i => new Card(i.ToString())));
        }

        public List<Card> ViewHand()
        {
            var cards = new List<Card>();
            // TODO: clone cards
            cards.AddRange(_cards);

            return cards;
        }

        public bool RemoveCard(Card card)
        {
            return _cards.Remove(card);
        }

        public virtual bool AddCard(Card card)
        {
            _cards.Add(card);
            return true;
        }

        public virtual bool AddAtPosition(Card card, int i)
        {
            _cards.Insert(i, card);
            return true;
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public override string ToString()
        {
            return string.Join(" ", _cards.Select(i => i.ToString()));
        }
    }
}
