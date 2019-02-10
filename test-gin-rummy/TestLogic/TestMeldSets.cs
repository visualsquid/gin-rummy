using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using gin_rummy.Cards;
using gin_rummy.Actors;

namespace test_gin_rummy.Cards
{
    /// <summary>
    /// Tests validity of set-based melds within the Gin-Rummy context.
    /// </summary>
    [TestClass]
    public class TestMeldSets
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
        public void TestEmptySet()
        {
            Assert.IsFalse(_checker.IsSet(_currentMeld));
        }

        [TestMethod]
        public void TestSetTooSmall()
        {
            _currentMeld.AddCard(new Card(Suit.Hearts(), Card.Rank.Ace));
            _currentMeld.AddCard(new Card(Suit.Clubs(), Card.Rank.Ace));

            Assert.IsFalse(_checker.IsSet(_currentMeld));
        }

        [TestMethod]
        public void TestSetWithDuplicates()
        {
            _currentMeld.AddCard(new Card(Suit.Hearts(), Card.Rank.Ace));
            _currentMeld.AddCard(new Card(Suit.Clubs(), Card.Rank.Ace));
            _currentMeld.AddCard(new Card(Suit.Spades(), Card.Rank.Ace));
            _currentMeld.AddCard(new Card(Suit.Spades(), Card.Rank.Ace));

            Assert.IsFalse(_checker.IsSet(_currentMeld));
        }

        [TestMethod]
        public void TestValidThreeCardSet()
        {
            _currentMeld.AddCard(new Card(Suit.Hearts(), Card.Rank.Ace));
            _currentMeld.AddCard(new Card(Suit.Clubs(), Card.Rank.Ace));
            _currentMeld.AddCard(new Card(Suit.Spades(), Card.Rank.Ace));

            Assert.IsTrue(_checker.IsSet(_currentMeld));
        }

        [TestMethod]
        public void TestValidFourCardSet()
        {
            _currentMeld.AddCard(new Card(Suit.Hearts(), Card.Rank.Five));
            _currentMeld.AddCard(new Card(Suit.Clubs(), Card.Rank.Five));
            _currentMeld.AddCard(new Card(Suit.Spades(), Card.Rank.Five));
            _currentMeld.AddCard(new Card(Suit.Diamonds(), Card.Rank.Five));

            Assert.IsTrue(_checker.IsSet(_currentMeld));
        }
    }
}
