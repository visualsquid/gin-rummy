using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace gin_rummy.Messaging
{
    /// <summary>
    /// Implementation of a logger for logging to a Windows Forms TextBox. Logger's perspective is that of a non-participating observer i.e. it is not all-seeing.
    /// This class handles logging using BackgroundWorkers. The work is so trivial it's not really necessary, but this is practice to threading the other classes that 
    /// handle messages.
    /// </summary>
    public class GameMemoBoxLogger : GameLogger
    {

        private readonly Queue<GameMessage> _pendingMessages;
        private readonly BackgroundWorker _messageHandler;

        public TextBox MemoBox { get; set; }

        public GameMemoBoxLogger()
        {
            _pendingMessages = new Queue<GameMessage>();
            _messageHandler = new BackgroundWorker() { WorkerReportsProgress = false, WorkerSupportsCancellation = false };
            _messageHandler.DoWork += MessageHandler_DoWork;
            _messageHandler.RunWorkerAsync();
        }

        public List<string> GetLog()
        {
            lock (MemoBox)
            {
                return new List<string>(MemoBox.Lines);
            }
        }

        public override void ReceiveMessage(GameStatusMessage message)
        {
            ReceiveMessage(message);
        }

        public override void ReceiveMessage(PlayerRequestMessage message)
        {
            ReceiveMessage(message);
        }

        public override void ReceiveMessage(PlayerResponseMessage message)
        {
            ReceiveMessage(message);
        }

        private void ReceiveMessage(GameMessage message)
        {
            lock (_pendingMessages)
            {
                _pendingMessages.Enqueue(message);
            }
        }

        private void MessageHandler_DoWork(object sender, DoWorkEventArgs e)
        {
            const int SleepTimeMs = 500;
            const int MaxBatchSize = 50;
            Queue<GameMessage> _buffer = new Queue<GameMessage>();
            
            while (true)
            {
                lock (_pendingMessages)
                {
                    for (int i = MaxBatchSize; i > 0 && _pendingMessages.Count > 0; i--)
                    {
                        _buffer.Enqueue(_pendingMessages.Dequeue());
                    }
                }

                while (_buffer.Count > 0)
                {
                    GameMessage nextMessage = _buffer.Dequeue();
                    string nextLog = "";
                    if (nextMessage is GameStatusMessage)
                    {
                        nextLog = ParseGameStatusMessage(nextMessage as GameStatusMessage);
                    }
                    else if (nextMessage is PlayerRequestMessage)
                    {
                        nextLog = ParsePlayerRequestMessage(nextMessage as PlayerRequestMessage);
                    }
                    else if (nextMessage is PlayerResponseMessage)
                    {
                        nextLog = ParsePlayerResponseMessage(nextMessage as PlayerResponseMessage);
                    }
                    else
                    {
                        nextLog = "Unexpected error - unknown message type.";
                    }

                    WriteLog(nextLog);
                }

                Thread.Sleep(SleepTimeMs);
            }
            
        }

        public override void WriteLog(string message)
        {
            lock (MemoBox)
            {
                MemoBox.Invoke((MethodInvoker)delegate
                {
                    MemoBox.AppendText(message);
                });
            }
        }

        private string ParsePlayerResponseMessage(PlayerResponseMessage response)
        {
            bool requestAccepted = response.Response == PlayerResponseMessage.PlayerResponseType.Accepted;

            if (!requestAccepted)
            {
                return $"Denied: {response.ErrorMessage}";
            }
            else {
                switch (response.Request.PlayerRequestTypeValue)
                {
                    case PlayerRequestMessage.PlayerRequestType.DrawDiscard:
                        return $"{response.Player.Name} draws discard {response.Card.ToString()}.";
                    case PlayerRequestMessage.PlayerRequestType.DrawStock:
                        return $"{response.Player.Name} draws stock.";
                    case PlayerRequestMessage.PlayerRequestType.SetDiscard:
                        return $"{response.Player.Name} sets discard {response.Card.ToString()}.";
                    case PlayerRequestMessage.PlayerRequestType.Knock:
                        return $"{response.Player.Name} knocks.";
                    default:
                        return "Unknown message";
                }
            }
        }

        private string ParseGameStatusMessage(GameStatusMessage message)
        {
            switch (message.GameStatusChangeValue)
            {
                case GameStatusMessage.GameStatusChange.GameInitialised:
                    return "New game was initialised.";
                case GameStatusMessage.GameStatusChange.StartTurn:
                    return $"{message.Player.Name}'s turn.";
                case GameStatusMessage.GameStatusChange.StartMeld:
                    return $"{message.Player.Name}: meld your cards!";
                case GameStatusMessage.GameStatusChange.StartLayoff:
                    return $"{message.Player.Name}: layoff now!";
                default:
                    return "Unknown message";
            }
        }

        private string ParsePlayerRequestMessage(PlayerRequestMessage message)
        {
            switch (message.PlayerRequestTypeValue)
            {
                case PlayerRequestMessage.PlayerRequestType.DrawDiscard:
                    return $"{message.Player.Name} requests discard...";
                case PlayerRequestMessage.PlayerRequestType.DrawStock:
                    return $"{message.Player.Name} requests stock...";
                case PlayerRequestMessage.PlayerRequestType.SetDiscard:
                    return $"{message.Player.Name} requests to discard {message.Card.ToString()}.";
                case PlayerRequestMessage.PlayerRequestType.Knock:
                    return $"{message.Player.Name} requests knock...";
                default:
                    return "Unknown message";
            }
        }

    }
}
