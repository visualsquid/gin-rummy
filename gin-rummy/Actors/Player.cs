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
        protected Thread _getMelds;
        protected Thread _getLayOffs;
        protected GameMaster _gameMaster;
        protected MeldChecker _meldChecker;

        public int HandSize { get { return _hand.Size; } }
        public string Name { get; set; }

        public Player(string name)
        {
            Name = name;
            _hand = new GinRummyHand();
            _meldChecker = new MeldChecker();
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

        public void RequestMelds(GameMaster gm)
        {
            _gameMaster = gm;
            _getMelds = new Thread(new ThreadStart(ThreadedRequestMelds));
            _getMelds.Start();
        }

        public void RequestLayOffs(GameMaster gm, List<Meld> otherPlayerMelds)
        {
            _gameMaster = gm;
            _getLayOffs = new Thread(new ThreadStart(ThreadedRequestMelds));
            _getLayOffs.Start();
        }

        protected abstract void ThreadedYourTurn();
        protected abstract void ThreadedRequestMelds();
        protected abstract void ThreadedRequestLayOffs();

        
    }
}
