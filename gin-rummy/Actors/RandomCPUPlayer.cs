using gin_rummy.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Actors
{
    public class RandomCPUPlayer : Player
    {

        private enum TicketType { Knock, DrawDiscard, DrawStock }

        private const int KnockTicketCount = 5;
        private const double DrawDiscardTicketCount = 45;
        private const double DrawStockTicketCount = 50;

        private Random _random;
        private List<TicketType> _tickets;

        public RandomCPUPlayer(string name) : base (name)
        {
            _random = new Random();
            InitiateTickets();
        }

        private void InitiateTickets()
        {
            _tickets = new List<TicketType>();
            AddKnockTickets();
            AddDrawDiscardTickets();
            AddDrawStockTickets();
        }

        private void AddKnockTickets()
        {
            for (int i = 0; i < KnockTicketCount; i++)
            {
                _tickets.Add(TicketType.Knock);
            }
        }

        private void AddDrawDiscardTickets()
        {
            for (int i = 0; i < DrawDiscardTicketCount; i++)
            {
                _tickets.Add(TicketType.DrawDiscard);
            }
        }

        private void AddDrawStockTickets()
        {
            for (int i = 0; i < DrawStockTicketCount; i++)
            {
                _tickets.Add(TicketType.DrawStock);
            }
        }

        private TicketType GetNextTicket()
        {
            return _tickets[_random.Next(_tickets.Count)];
        }

        private Card GetRandomCardFromHand()
        {
            Card card = _hand.ViewHand()[_random.Next(_hand.Size)];
            _hand.RemoveCard(card);
            return card;
        }

        protected override void ThreadedYourTurn()
        {
            string error = "";
            bool success = false;
            Card drawnCard = null;
            while (!success)
            {
                switch (GetNextTicket())
                {
                    case TicketType.Knock:
                        success = _gameMaster.RequestKnock(this, out error);
                        break;
                    case TicketType.DrawDiscard:
                        success = (_gameMaster.RequestDrawDiscard(this, out drawnCard, out error) && _gameMaster.RequestPlaceDiscard(this, GetRandomCardFromHand(), out error));
                        
                        break;
                    case TicketType.DrawStock:
                        success = (_gameMaster.RequestDrawStock(this, out drawnCard, out error) && _gameMaster.RequestPlaceDiscard(this, GetRandomCardFromHand(), out error));
                        break;
                    default:
                        throw new Exception("Ticket type not supported.");
                }
            }

        }

        protected override void ThreadedRequestMelds()
        {
            // TODO: implement random CPU get melds
            var cardsInHand = this._hand.ViewHand();
            var possibleMeldSets = _meldChecker.GetAllPossibleMeldSets(cardsInHand);
            List<Meld> meldSet;
            if (possibleMeldSets.Count == 0)
            {
                meldSet = new List<Meld>();
            }
            else
            {
                meldSet = possibleMeldSets[_random.Next(possibleMeldSets.Count)];
            }
            var deadWood = _meldChecker.GetDeadWood(meldSet, cardsInHand);
            string error;
            Meld invalidMeld;
            _gameMaster.RequestSetMelds(this, meldSet, deadWood, out error, out invalidMeld);
        }

        protected override void ThreadedRequestLayOffs()
        {
            // TODO: implement random CPU get lay offs
            //_gameMaster.?
            throw new NotImplementedException();
        }
    }
}
