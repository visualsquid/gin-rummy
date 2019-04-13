using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gin_rummy.Cards;

namespace gin_rummy.Actors
{
    class HumanPlayerGUIBased : Player
    {
        public HumanPlayerGUIBased(string name) : base(name)
        {
        }

        private bool DrawCard(out Card drawnCard, out string error)
        {
            if (_gameMaster.RequestDrawDiscard(this, out drawnCard, out error))
            {
                this._hand.AddCard(drawnCard);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DrawStock(out Card drawnCard, out string error)
        {
            return DrawCard(out drawnCard, out error);
        }

        public bool DrawDiscard(out Card drawnCard, out string error)
        {
            return DrawCard(out drawnCard, out error);
        }

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
    }
}
