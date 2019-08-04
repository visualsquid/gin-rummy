using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gin_rummy.Actors;
using gin_rummy.Messaging;

namespace gin_rummy.Forms
{
    public partial class GameLog : Form
    {
        private readonly GameMemoBoxLogger _gameLogger;

        public GameLog(GameMaster gameMaster, Player playerOne, Player playerTwo)
        {
            InitializeComponent();
            _gameLogger = new GameMemoBoxLogger();
            _gameLogger.MemoBox = eLog;
            gameMaster.RegisterGameStatusListener(_gameLogger);
            gameMaster.RegisterPlayerResponseListener(_gameLogger);
            playerOne.RegisterRequestListener(_gameLogger);
            playerTwo.RegisterRequestListener(_gameLogger);
        }

    }
}
