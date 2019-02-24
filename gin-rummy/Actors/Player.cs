using gin_rummy.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gin_rummy.Actors
{
    public abstract class Player : IPlayer
    {
        protected readonly GinRummyHand _hand;
        protected Thread _yourTurn;
        protected GameMaster _gameMaster;

        public int HandSize { get { return _hand.Size; } }
        public string Name { get; set; }

        public Player(string name)
        {
            Name = name;
            _hand = new GinRummyHand();
        }

        public void ClearHand()
        {
            _hand.Clear();
        }

        public void DrawCard(Card c)
        {
            _hand.AddCard(c);
        }

        public bool DiscardCard(Card c)
        {
            return _hand.RemoveCard(c);
        }

        public void YourTurn(GameMaster gm)
        {
            _gameMaster = gm;
            _yourTurn = new Thread(new ThreadStart(ThreadedYourTurn));
            _yourTurn.Start();
        }

        protected abstract void ThreadedYourTurn();
        
    }
}
