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

        public override void ReceiveMessage(GameMessage message)
        {
            WriteLog(ParseGameMessage(message));
        }

        public override void WriteLog(string message)
        {
            MemoBox.AppendText(message);
        }

        private string ParseGameMessage(GameMessage message)
        {
            if (message is GameStatusMessage)
            {
                return ParseGameStatusMessage(message as GameStatusMessage);
            }
            else if (message is PlayerActionMessage)
            {
                return ParsePlayerActionMessage(message as PlayerActionMessage);
            }
            else
            {
                throw new ArgumentException("Message type is not recognised.");
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
                default:
                    return "Unknown message";
            }
        }

        private string ParsePlayerActionMessage(PlayerActionMessage message)
        {
            switch (message.PlayerActionValue)
            {
                case PlayerActionMessage.PlayerAction.DrawDiscard:
                    return $"{message.Player.Name} took discard {message.Card.ToString()}.";
                case PlayerActionMessage.PlayerAction.DrawStock:
                    return $"{message.Player.Name} drew stock.";
                case PlayerActionMessage.PlayerAction.SetDiscard:
                    return $"{message.Player.Name} set discard {message.Card.ToString()}.";
                case PlayerActionMessage.PlayerAction.Knock:
                    return $"{message.Player.Name} knocked.";
                default:
                    return "Unknown message";
            }
        }


    }
}
