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

        private readonly List<GameMessage> _messageBuffer;
        private readonly List<GameMessage> _wip;
        private readonly List<string> _logBuffer;

        public TextBox MemoBox { get; set; }

        public GameMemoBoxLogger()
        {
            _messageBuffer = new List<GameMessage>();
            _logBuffer = new List<string>();
            _wip = new List<GameMessage>();
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
            lock (_messageBuffer)
            {
                _messageBuffer.Add(message);
            }
            SpawnBackgroundWorkerToWriteLog();
        }

        private void SpawnBackgroundWorkerToWriteLog()
        {
            var worker = new BackgroundWorker() { WorkerReportsProgress = false, WorkerSupportsCancellation = false };
            worker.DoWork += BackgroundWorker_DoWork;
            worker.RunWorkerCompleted += BackgroundWorker_WorkCompleted;
            worker.RunWorkerAsync();
        }

        public bool HasPendingWork()
        {
            bool result;

            lock (_messageBuffer)
            {
                lock (_wip)
                {
                    lock (_logBuffer)
                    {
                        result = _wip.Count > 0 || _messageBuffer.Count > 0 || _logBuffer.Count > 0;
                    }
                }
            }

            return result;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            GameMessage nextMessage = null;
            lock (_messageBuffer)
            {
                lock (_wip)
                {
                    if (_messageBuffer.Count > 0)
                    {
                        nextMessage = _messageBuffer[0];
                        _messageBuffer.RemoveAt(0);
                    }

                    _wip.Add(nextMessage);
                }
            }

            if (nextMessage == null)
            {
                return;
            }

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

            lock (_wip)
            {
                lock (_logBuffer)
                {
                    _logBuffer.Add(nextLog);
                }
                _wip.Remove(nextMessage);
            }
        }

        private void BackgroundWorker_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string nextLog = null;
            lock (_logBuffer)
            {
                if (_logBuffer.Count > 0)
                {
                    nextLog = _logBuffer[0];
                    _logBuffer.RemoveAt(0);
                }
            }

            if (nextLog == null)
            {
                return;
            }

            WriteLog(nextLog);
        }

        public override void WriteLog(string message)
        {
            lock (MemoBox)
            {
                MemoBox.AppendText(message);
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
