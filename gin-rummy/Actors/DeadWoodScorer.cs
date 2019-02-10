using gin_rummy.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gin_rummy.Actors
{
    /// <summary>
    /// Controller class for calculating dead wood penalties.
    /// </summary>
    public class DeadWoodScorer
    {
        private const int RankOrdinalToPenaltyOffset = 1;

        public const int PictureCardDeadWoodPenalty = 10;
        

        public DeadWoodScorer() { }

        public int GetDeadWoodPenalty(List<Card> deadWood)
        {
            int totalPenalty = 0;

            foreach (Card c in deadWood)
            {
                switch (c.RankValue)
                {
                    case Card.Rank.Ace:
                    case Card.Rank.Two:
                    case Card.Rank.Three:
                    case Card.Rank.Four:
                    case Card.Rank.Five:
                    case Card.Rank.Six:
                    case Card.Rank.Seven:
                    case Card.Rank.Eight:
                    case Card.Rank.Nine:
                    case Card.Rank.Ten:
                        totalPenalty += (int)c.RankValue + RankOrdinalToPenaltyOffset;
                        break;
                    case Card.Rank.Jack:
                    case Card.Rank.Queen:
                    case Card.Rank.King:
                        totalPenalty += PictureCardDeadWoodPenalty;
                        break;
                    default:
                        throw new Exception($"Invalid rank {c.RankValue.ToString()}");
                }
            }

            return totalPenalty;
        }
    }
}
