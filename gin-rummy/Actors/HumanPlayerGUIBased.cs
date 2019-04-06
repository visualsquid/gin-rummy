using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Actors
{
    class HumanPlayerGUIBased : Player
    {
        public HumanPlayerGUIBased(string name) : base(name)
        {
        }

        protected override void ThreadedRequestLayOffs()
        {
            throw new NotImplementedException();
        }

        protected override void ThreadedRequestMelds()
        {
            throw new NotImplementedException();
        }

        protected override void ThreadedYourTurn()
        {
            throw new NotImplementedException();
        }
    }
}
