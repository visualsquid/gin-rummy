using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    /// <summary>
    /// Model class for storing the state of the table, that is, the stacks.
    /// </summary>
    public class Table
    {

        private Deck _stock;
        private Deck _discards;

        public int StockCount { get { return _stock.Size; } }
        public int DiscardCount { get { return _discards.Size; } }

        public Table()
        {
            _stock = new Deck(Deck.DeckType.Standard);
            _stock.Shuffle();
            _discards = new Deck(Deck.DeckType.Empty);
        }

        public void ClearTable()
        {
            _stock.Clear();
            _discards.Clear();
        }

        public Card DrawStock()
        {
            return _stock.RemoveTop();
        }

        public Card DrawDiscard()
        {
            return _discards.RemoveTop();
        }

        public void PlaceDiscard(Card card)
        {
            _discards.Add(card);
        }

        public void RestockFromDiscard()
        {
            _discards.Shuffle();
            _stock = _discards;
            _discards = new Deck(Deck.DeckType.Empty);
        }

        public Card PeekDiscard()
        {
            return _discards.PeekTop();
        }
    }
}
