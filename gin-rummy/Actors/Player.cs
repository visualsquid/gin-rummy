using gin_rummy.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using gin_rummy.GameStructures;
using gin_rummy.Messaging;

namespace gin_rummy.Actors
{
    public abstract class Player : IPlayer, IGameStatusListener, IPlayerResponseListener
    {
        protected readonly List<IPlayerRequestListener> _requestListeners;

        protected readonly GinRummyHand _hand;
        protected MeldedHand _meldedHand;

        protected Thread _yourTurn;
        protected Thread _getMelds;
        protected Thread _getLayOffs;
        protected MeldChecker _meldChecker;

        public int HandSize { get { return _hand.Size; } }
        public string Name { get; set; }
        public MeldedHand MeldedHand { get { return _meldedHand; } }

        public Player(string name)
        {
            _requestListeners = new List<IPlayerRequestListener>();
            Name = name;
            _hand = new GinRummyHand();
            _meldChecker = new MeldChecker();
            _meldedHand = null;
        }

        public void RegisterRequestListener(IPlayerRequestListener listener)
        {
            _requestListeners.Add(listener);
        }

        protected void NotifyRequestListeners(PlayerRequestMessage message)
        {
            foreach (var listener in _requestListeners)
            {
                listener.ReceiveMessage(message);
            }
        }

        public void MeldHand(MeldedHand hand)
        {
            _meldedHand = new MeldedHand(hand.Melds, hand.Deadwood);
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

        public List<Card> GetCards()
        {
            return _hand.ViewHand();
        }

        public void YourTurn(GameMaster gm)
        {
            //_gameMaster = gm;
            //_yourTurn = new Thread(new ThreadStart(ThreadedYourTurn));
            //_yourTurn.Start();
            ThreadedYourTurn();
        }

        public void RequestMelds(GameMaster gm)
        {
            //_gameMaster = gm;
            _getMelds = new Thread(new ThreadStart(ThreadedRequestMelds));
            _getMelds.Start();
        }

        public void RequestLayOffs(GameMaster gm, List<Meld> otherPlayerMelds)
        {
            //_gameMaster = gm;
            _getLayOffs = new Thread(new ThreadStart(ThreadedRequestMelds));
            _getLayOffs.Start();
        }

        protected abstract void ThreadedYourTurn();
        protected abstract void ThreadedRequestMelds();
        protected abstract void ThreadedRequestLayOffs();
        public abstract void ReceiveMessage(GameStatusMessage message);
        public abstract void ReceiveMessage(PlayerResponseMessage message);

    }
}
