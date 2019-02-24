using gin_rummy.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Actors
{
    public interface IPlayer
    {
        int HandSize { get; }
        string Name { get; set; }

        void YourTurn(GameMaster gm);
        void DrawCard(Card c);
        bool DiscardCard(Card c);
        void ClearHand();
    }
}
