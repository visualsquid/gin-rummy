using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    public class Deck
    {
        private List<Card> _cards;

        public int Size { get { return _cards.Count; } }

        public Deck()
        {
            _cards = new List<Card>();
            InitialiseStandardDeck(_cards);
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public Card PeekTop()
        {
            return PeekAt(0);
        }

        public Card RemoveTop()
        {
            return RemoveAt(0);
        }

        public Card PeekAt(int i)
        {
            // TODO: bounds checking?
            return _cards[i];
        }

        public Card RemoveAt(int i)
        {
            // TODO: bounds checking?
            var card = _cards[i];
            _cards.Remove(card);
            return card;
        }

        public void Add(Card card)
        {
            _cards.Add(card);
        }

        public bool ContainsDuplicates()
        {
            // TODO: implement this
            throw new NotImplementedException();
        }

        public void Shuffle()
        {
            var tempDeck = new List<Card>();

            Random r = new Random();
            while (_cards.Count > 0)
            {
                var card = _cards[r.Next(_cards.Count - 1)];
                _cards.Remove(card);
                tempDeck.Add(card);
            }

            _cards = tempDeck;
        }

        public void InitialiseStandardDeck(List<Card> cards)
        {
            _cards.Clear();
            foreach (Suit.SuitType suitType in Enum.GetValues(typeof(Suit.SuitType)))
            {
                foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank)))
                {
                    cards.Add(new Card(Suit.GetSuit(suitType), rank));
                }
            }
        }
    }
}
