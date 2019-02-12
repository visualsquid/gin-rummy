using System;
using gin_rummy.Actors;
using gin_rummy.Cards;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static gin_rummy.Cards.Card;

namespace test_gin_rummy.Cards
{
    /// <summary>
    /// Tests validity of run-based melds.
    /// </summary>
    [TestClass]
    public class TestMeldRuns
    {
        private MeldChecker _checker;
        private Meld _currentMeld;

        [TestInitialize]
        public void Initialize()
        {
            _currentMeld = new Meld();
            _checker = new MeldChecker();
        }

        [TestMethod]
        public void TestEmptyRun()
        {
            Assert.IsFalse(_checker.IsRun(_currentMeld));
        }

        [TestMethod]
        public void TestRunTooSmall()
        {
            _currentMeld.AddCard(new Card(Suit.Hearts, Card.Rank.Ace));
            _currentMeld.AddCard(new Card(Suit.Hearts, Card.Rank.Two));

            Assert.IsFalse(_checker.IsRun(_currentMeld));
        }

        [TestMethod]
        public void TestRunWithDuplicates()
        {
            _currentMeld.AddCard(new Card(Suit.Hearts, Card.Rank.Ace));
            _currentMeld.AddCard(new Card(Suit.Hearts, Card.Rank.Two));
            _currentMeld.AddCard(new Card(Suit.Hearts, Card.Rank.Two));

            Assert.IsFalse(_checker.IsRun(_currentMeld));
        }

        [TestMethod]
        public void TestRunWithGaps()
        {
            _currentMeld.AddCard(new Card(Suit.Hearts, Card.Rank.Ace));
            _currentMeld.AddCard(new Card(Suit.Hearts, Card.Rank.Two));
            _currentMeld.AddCard(new Card(Suit.Hearts, Card.Rank.Four));

            Assert.IsFalse(_checker.IsRun(_currentMeld));
        }

        [TestMethod]
        public void TestInvalidAceHighRun()
        {
            _currentMeld.AddCard(new Card(Suit.Spades, Card.Rank.Queen));
            _currentMeld.AddCard(new Card(Suit.Spades, Card.Rank.King));
            _currentMeld.AddCard(new Card(Suit.Spades, Card.Rank.Ace));

            Assert.IsFalse(_checker.IsRun(_currentMeld));
        }

        [TestMethod]
        public void TestInvalidWrappedMeld()
        {
            _currentMeld.AddCard(new Card(Suit.Spades, Card.Rank.King));
            _currentMeld.AddCard(new Card(Suit.Spades, Card.Rank.Ace));
            _currentMeld.AddCard(new Card(Suit.Spades, Card.Rank.Two));

            Assert.IsFalse(_checker.IsRun(_currentMeld));
        }

        [TestMethod]
        public void TestValidThreeCardRun()
        {
            _currentMeld.AddCard(new Card(Suit.Hearts, Card.Rank.Ace));
            _currentMeld.AddCard(new Card(Suit.Hearts, Card.Rank.Two));
            _currentMeld.AddCard(new Card(Suit.Hearts, Card.Rank.Three));

            Assert.IsTrue(_checker.IsRun(_currentMeld));
        }

        [TestMethod]
        public void TestValidFourCardRun()
        {
            _currentMeld.AddCard(new Card(Suit.Clubs, Card.Rank.Ten));
            _currentMeld.AddCard(new Card(Suit.Clubs, Card.Rank.Jack));
            _currentMeld.AddCard(new Card(Suit.Clubs, Card.Rank.Queen));
            _currentMeld.AddCard(new Card(Suit.Clubs, Card.Rank.King));

            Assert.IsTrue(_checker.IsRun(_currentMeld));
        }

        [TestMethod]
        public void TestValidRunInMixedOrder()
        {
            _currentMeld.AddCard(new Card(Suit.Diamonds, Card.Rank.Eight));
            _currentMeld.AddCard(new Card(Suit.Diamonds, Card.Rank.Seven));
            _currentMeld.AddCard(new Card(Suit.Diamonds, Card.Rank.Five));
            _currentMeld.AddCard(new Card(Suit.Diamonds, Card.Rank.Ten));
            _currentMeld.AddCard(new Card(Suit.Diamonds, Card.Rank.Six));
            _currentMeld.AddCard(new Card(Suit.Diamonds, Card.Rank.Jack));
            _currentMeld.AddCard(new Card(Suit.Diamonds, Card.Rank.Nine));

            Assert.IsTrue(_checker.IsRun(_currentMeld));
        }
    }
}
