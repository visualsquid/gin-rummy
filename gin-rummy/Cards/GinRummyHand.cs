using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Cards
{
    public class GinRummyHand : Hand
    {

        public const int MaxSize = 11;

        public GinRummyHand() { }

        public override bool AddCard(Card card)
        {
            if (Size == MaxSize)
            {
                return false;
            }
            else
            {
                return base.AddCard(card);
            }
        }

        public override bool AddAtPosition(Card card, int i)
        {
            if (Size == MaxSize)
            {
                return false;
            }
            else
            {
                return base.AddAtPosition(card, i);
            }
        }


    }
}
