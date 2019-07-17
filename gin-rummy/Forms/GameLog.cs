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
        private GameMemoBoxLogger _gameLogger;

        public GameLog(GameMaster gameMaster)
        {
            InitializeComponent();
            _gameLogger = new GameMemoBoxLogger();
            _gameLogger.MemoBox = eLog;
            gameMaster.RegisterGameMessageListener(_gameLogger);
        }

    }
}
