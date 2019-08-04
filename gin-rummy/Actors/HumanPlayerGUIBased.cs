using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gin_rummy.Cards;
using gin_rummy.GameStructures;
using gin_rummy.Messaging;

namespace gin_rummy.Actors
{
    public class HumanPlayerGUIBased : Player
    {

        public HumanPlayerGUIBased(string name) : base(name)
        {
            // No work
        }

        public void RequestKnock()
        {
            NotifyRequestListeners(new PlayerRequestMessage(PlayerRequestMessage.PlayerRequestType.Knock, this));
        }

        public void RequestDrawDiscard()
        {
            NotifyRequestListeners(new PlayerRequestMessage(PlayerRequestMessage.PlayerRequestType.DrawDiscard, this));
        }

        public void RequestDrawStock()
        {
            NotifyRequestListeners(new PlayerRequestMessage(PlayerRequestMessage.PlayerRequestType.DrawStock, this));
        }

        public void RequestDiscard(Card discard)
        {
            NotifyRequestListeners(new PlayerRequestMessage(PlayerRequestMessage.PlayerRequestType.SetDiscard, this, discard));
        }

        public void RequestSetMelds(MeldedHand hand)
        {
            NotifyRequestListeners(new PlayerRequestMessage(PlayerRequestMessage.PlayerRequestType.MeldHand, this, hand));
        }

        //private bool DrawCard(out Card drawnCard, out string error)
        //{
        //if (_gameMaster.RequestDrawDiscard(this, out drawnCard, out error))
        //{
        //    this._hand.AddCard(drawnCard);
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
        //}

        //public bool DrawStock(out Card drawnCard, out string error)
        //{
        //    return DrawCard(out drawnCard, out error);
        //}

        //public bool DrawDiscard(out Card drawnCard, out string error)
        //{
        //    return DrawCard(out drawnCard, out error);
        //}

        protected override void ThreadedRequestLayOffs()
        {
            //TODO: notify/enable the GUI
        }

        protected override void ThreadedRequestMelds()
        {
            //TODO: notify/enable the GUI
        }

        protected override void ThreadedYourTurn()
        {
            //TODO: notify/enable the GUI
        }

        public override void ReceiveMessage(GameStatusMessage message)
        {
            // TODO: do we need to do anything here?
        }

        public override void ReceiveMessage(PlayerResponseMessage message)
        {
            // TODO: do we need to do anything here?
        }
    }
}
