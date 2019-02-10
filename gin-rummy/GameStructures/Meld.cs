using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    public class Meld
    {

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

        public bool RemoveCard(Card c)
        {
            return _cards.Remove(c);
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public List<Card> GetListOfCardsInMeld()
        {
            return new List<Card>(_cards);
        }
    }

}
