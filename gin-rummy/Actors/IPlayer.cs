using gin_rummy.Cards;
using gin_rummy.GameStructures;
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
        void RequestMelds(GameMaster gm);
        void RequestLayOffs(GameMaster gm, List<Meld> otherPlayerMelds);
        void DrawCard(Card c);
        bool DiscardCard(Card c);
        void ClearHand();
        void MeldHand(MeldedHand hand);
    }
}
