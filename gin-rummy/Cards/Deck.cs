using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using static gin_rummy.Cards.Card;

namespace gin_rummy.Cards
{
    public class Deck
    {
        public enum DeckType { Empty, Standard }

        private List<Card> _cards;

        public int Size { get { return _cards.Count; } }

        public Deck() : this(DeckType.Standard)
        {
        }

        public Deck(DeckType deckType)
        {
            _cards = new List<Card>();
            if (deckType == DeckType.Empty)
            {
                return;
            }

            AddStandard52(_cards);
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public Card PeekTop()
        {
            return PeekAt(_cards.Count - 1);
        }

        public Card RemoveTop()
        {
            return RemoveAt(_cards.Count - 1);
        }

        public List<Card> RemoveTop(int count)
        {
            var cards = new List<Card>();
            while (count-- > 0)
            {
                cards.Add(RemoveTop());
            }
            return cards;
        }

        public List<Card> RemoveAll()
        {
            return RemoveTop(Size);
        }

        public Card PeekBottom()
        {
            return PeekAt(0);
        }

        public Card RemoveBottom()
        {
            return RemoveAt(0);
        }

        public List<Card> RemoveBottom(int count)
        {
            var cards = new List<Card>();
            while (count-- > 0)
            {
                cards.Add(RemoveBottom());
            }
            return cards;
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

        public void AddStandard52(List<Card> cards)
        {
            _cards.Clear();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank)))
                {
                    cards.Add(new Card(suit, rank));
                }
            }
        }

        public override string ToString()
        {
            return string.Join(" ", _cards.Select(i => i.ToString()));
        }
    }
}
