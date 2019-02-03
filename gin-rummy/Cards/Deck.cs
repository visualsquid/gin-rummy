using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    class Deck
    {
        private List<Card> _cards;

        public Deck()
        {
            _cards = new List<Card>();
            InitialiseStandardDeck(_cards);
        }

        
    }
}
