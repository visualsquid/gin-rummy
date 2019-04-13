using gin_rummy.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Actors
{
    public class GUIControllerFormsA : GUIController
    {

        private class PlayerControls
        {
            public Player Player { get; set; }
            public CardPanel CardPanel { get; set; }
        }

        private readonly List<PlayerControls> _playerControls;
        private CardStacks cardStacks;

        public GUIControllerFormsA() : base()
        {
            _playerControls = new List<PlayerControls>();
        }

        public void AddPlayer(Player player, CardPanel cardPanel)
        {
            _playerControls.Add(new PlayerControls() { Player = player, CardPanel = cardPanel });
        }

        // TODO: finish implementing this - the idea is the player notifies the gui controller, I think, or someone does anyway, which then enables
        // the controls as appropriate. Then, when the user clicks or whatever, it notifies the player?
        public override void NotifyCardDrawn(Player player)
        {
            throw new NotImplementedException();
        }

        public override void NotifyCardDiscarded(Player player)
        {
            throw new NotImplementedException();
        }

        public override void NotifyStartPlayerTurn(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
