using gin_rummy.Actors;
using gin_rummy.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.GameStructures
{
    class Showdown
    {
        private readonly List<Meld> _melds;
        private readonly List<Card> _deadWood;

        public int MeldCount { get { return _melds.Count; } }
        public int DeadWoodCardCount { get { return _deadWood.Count; } }

        public Showdown(List<Meld> melds, List<Card> deadWood)
        {
            _melds = new List<Meld>(melds);
            _deadWood = new List<Card>(deadWood);
        }

        public List<Meld> GetListOfMelds()
        {
            return new List<Meld>(_melds);
        }

        public List<Card> GetListOfDeadWoodCards()
        {
            return new List<Card>(_deadWood);
        }

    }
}
