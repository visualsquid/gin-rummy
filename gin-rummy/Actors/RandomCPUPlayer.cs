using gin_rummy.Cards;
using gin_rummy.GameStructures;
using gin_rummy.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;

namespace gin_rummy.Actors
{
    public class RandomCPUPlayer : Player
    {
        private enum TicketType { Knock, DrawDiscard, DrawStock }

        private const int KnockTicketCount = 5;
        private const double DrawDiscardTicketCount = 45;
        private const double DrawStockTicketCount = 50;

        private readonly Queue<GameMessage> _pendingMessages;
        private readonly Random _random;
        private readonly List<TicketType> _tickets;
        private BackgroundWorker _worker;

        public RandomCPUPlayer(string name) : base (name)
        {
            _pendingMessages = new Queue<GameMessage>();
            _random = new Random();
            _tickets = new List<TicketType>();
            InitialiseTickets();
            _worker = new BackgroundWorker() { WorkerReportsProgress = false, WorkerSupportsCancellation = false };
            _worker.DoWork += BackgroundWorker_DoWork;
            _worker.RunWorkerAsync();
        }

        private void InitialiseTickets()
        {
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
            switch (GetNextTicket())
            {
                case TicketType.Knock:
                    NotifyRequestListeners(new PlayerRequestMessage(PlayerRequestMessage.PlayerRequestType.Knock, this));
                    //success = _gameMaster.RequestKnock(this, out error);
                    break;
                case TicketType.DrawDiscard:
                    NotifyRequestListeners(new PlayerRequestMessage(PlayerRequestMessage.PlayerRequestType.DrawDiscard, this));
                    //success = (_gameMaster.RequestDrawDiscard(this, out drawnCard, out error) && _gameMaster.RequestPlaceDiscard(this, GetRandomCardFromHand(), out error));
                    break;
                case TicketType.DrawStock:
                    NotifyRequestListeners(new PlayerRequestMessage(PlayerRequestMessage.PlayerRequestType.DrawStock, this));
                    //success = (_gameMaster.RequestDrawStock(this, out drawnCard, out error) && _gameMaster.RequestPlaceDiscard(this, GetRandomCardFromHand(), out error));
                    break;
                default:
                    throw new Exception("Ticket type not supported.");
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
            
            NotifyRequestListeners(new PlayerRequestMessage(PlayerRequestMessage.PlayerRequestType.MeldHand, this, new MeldedHand(meldSet, deadWood)));
            //_gameMaster.RequestSetMelds(this, new MeldedHand(meldSet, deadWood), out error, out invalidMeld);
        }

        protected override void ThreadedRequestLayOffs()
        {
            // TODO: implement random CPU get lay offs
            //_gameMaster.?
            throw new NotImplementedException();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            const int SleepTimeMs = 250;
            const int MaxBufferSize = 50;
            var buffer = new Queue<GameMessage>();


            while (true)
            {
                lock (_pendingMessages)
                {
                    for (int i = MaxBufferSize; i > 0 && _pendingMessages.Count > 0; i--)
                    {
                        buffer.Enqueue(_pendingMessages.Dequeue());
                    }
                }

                while (buffer.Count > 0)
                {
                    GameMessage nextMessage = buffer.Dequeue();
                    if (nextMessage is GameStatusMessage)
                    {
                        HandleMessage(nextMessage as GameStatusMessage);
                    }
                    else if (nextMessage is PlayerResponseMessage)
                    {
                        HandleMessage(nextMessage as PlayerResponseMessage);
                    }
                    else
                    {
                        throw new Exception("Unexpected error - unknown message type.");
                    }
                }

                Thread.Sleep(SleepTimeMs);
            }
        }

        private void HandleMessage(GameStatusMessage message)
        {
            switch (message.GameStatusChangeValue)
            {
                case GameStatusMessage.GameStatusChange.GameInitialised:
                    // No work
                    break;
                case GameStatusMessage.GameStatusChange.StartTurn:
                    if (message.Player == this)
                    {
                        ThreadedYourTurn();
                    }
                    break;
                case GameStatusMessage.GameStatusChange.StartMeld:
                    if (message.Player == this)
                    {
                        ThreadedRequestMelds();
                    }
                    break;
                case GameStatusMessage.GameStatusChange.StartLayoff:
                    if (message.Player == this)
                    {
                        ThreadedRequestLayOffs();
                    }
                    break;
                default:
                    break;
            }
        }

        private void HandleMessage(PlayerResponseMessage message)
        {
            if (message.Player != this)
            {
                return;
            }

            if (message.Response == PlayerResponseMessage.PlayerResponseType.Denied)
            {
                // Do nothing - GameMaster will ask us to perform again.
                return;
            }

            switch (message.Request.PlayerRequestTypeValue)
            {
                case PlayerRequestMessage.PlayerRequestType.DrawDiscard:
                    NotifyRequestListeners(new PlayerRequestMessage(PlayerRequestMessage.PlayerRequestType.SetDiscard, this, GetRandomCardFromHand()));
                    break;
                case PlayerRequestMessage.PlayerRequestType.DrawStock:
                    NotifyRequestListeners(new PlayerRequestMessage(PlayerRequestMessage.PlayerRequestType.SetDiscard, this, GetRandomCardFromHand()));
                    break;
                case PlayerRequestMessage.PlayerRequestType.SetDiscard:
                    // Do nothing
                    break;
                case PlayerRequestMessage.PlayerRequestType.Knock:
                    // Do nothing
                    break;
                case PlayerRequestMessage.PlayerRequestType.MeldHand:
                    // Do nothing
                    break;
                default:
                    break;
            }
        }

        public override void ReceiveMessage(GameStatusMessage message)
        {
            lock (_pendingMessages)
            {
                _pendingMessages.Enqueue(message);
            }
        }

        public override void ReceiveMessage(PlayerResponseMessage message)
        {
            lock (_pendingMessages)
            {
                _pendingMessages.Enqueue(message);
            }
        }
    }
}
