using gin_rummy.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Actors
{
    class Player : IPlayer
    {
        protected readonly GinRummyHand _hand;

        public int HandSize { get { return _hand.Size; } }

        public Player()
        {
            _hand = new GinRummyHand();
        }

        public void ClearHand()
        {
            _hand.Clear();
        }

        public Card DiscardCard()
        {
            throw new NotImplementedException();
        }

        public void TakeCard(Card c)
        {
            throw new NotImplementedException();
        }
       
    }
}
