using System;
using System.Collections.Generic;
using gin_rummy.Actors;
using gin_rummy.Cards;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test_gin_rummy.TestLogic
{
    /// <summary>
    /// Tests the penalties calculated for dead wood.
    /// </summary>
    [TestClass]
    public class TestDeadWoodPenalties
    {

        private DeadWoodScorer _scorer;
        private List<Card> _deadWood;

        [TestInitialize]
        public void Initialize()
        {
            _scorer = new DeadWoodScorer();
            _deadWood = new List<Card>();
        }

        [TestMethod]
        public void TestEmptyDeadWood()
        {
            Assert.AreEqual(0, _scorer.GetDeadWoodPenalty(_deadWood));
        }

        [TestMethod]
        public void TestLowAce()
        {
            _deadWood.Add(new Card(Suit.Clubs(), Card.Rank.Ace));
            Assert.AreEqual(1, _scorer.GetDeadWoodPenalty(_deadWood));
        }

        [TestMethod]
        public void TestFaceCards()
        {
            _deadWood.Add(new Card(Suit.Clubs(), Card.Rank.Jack));
            _deadWood.Add(new Card(Suit.Clubs(), Card.Rank.Queen));
            _deadWood.Add(new Card(Suit.Clubs(), Card.Rank.King));

            Assert.AreEqual(10 + 10 + 10, _scorer.GetDeadWoodPenalty(_deadWood));
        }

        [TestMethod]
        public void TestSuitEquality()
        {
            _deadWood.Add(new Card(Suit.Clubs(), Card.Rank.Five));
            _deadWood.Add(new Card(Suit.Hearts(), Card.Rank.Five));
            _deadWood.Add(new Card(Suit.Diamonds(), Card.Rank.Five));
            _deadWood.Add(new Card(Suit.Spades(), Card.Rank.Five));

            Assert.AreEqual(5 + 5 + 5 + 5, _scorer.GetDeadWoodPenalty(_deadWood));
        }

        [TestMethod]
        public void TestMisc()
        {
            _deadWood.Add(new Card(Suit.Clubs(), Card.Rank.Three));
            _deadWood.Add(new Card(Suit.Hearts(), Card.Rank.Six));
            _deadWood.Add(new Card(Suit.Diamonds(), Card.Rank.Ten));

            Assert.AreEqual(3 + 6 + 10, _scorer.GetDeadWoodPenalty(_deadWood));
        }
    }
}
