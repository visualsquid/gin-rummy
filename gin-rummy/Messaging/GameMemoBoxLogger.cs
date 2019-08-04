using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gin_rummy.Messaging
{
    /// <summary>
    /// Implementation of a logger for logging to a Windows Forms TextBox. Logger's perspective is that of a non-participating observer i.e. it is not all-seeing.
    /// </summary>
    public class GameMemoBoxLogger : GameLogger
    {
        
        public TextBox MemoBox { get; set; }

        public override void ReceiveMessage(GameStatusMessage message)
        {
            WriteLog(ParseGameStatusMessage(message));
        }

        public override void ReceiveMessage(PlayerRequestMessage message)
        {
            WriteLog(ParsePlayerRequestMessage(message));
        }

        public override void ReceiveMessage(PlayerResponseMessage message)
        {
            WriteLog(ParsePlayerResponseMessage(message));
        }

        public override void WriteLog(string message)
        {
            MemoBox.AppendText(message);
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
