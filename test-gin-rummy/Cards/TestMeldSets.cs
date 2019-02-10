using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using gin_rummy.Cards;

namespace test_gin_rummy.Cards
{
    /// <summary>
    /// Tests validity of set-based melds within the Gin-Rummy context.
    /// </summary>
    [TestClass]
    public class TestMeldSets
    {
        [TestMethod]
        public void TestEmptySet()
        {
            Meld m = new Meld();
            Assert.IsFalse(m.IsSet());
        }

        [TestMethod]
        public void TestSetTooSmall()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Ace));
            m.AddCard(new Card(Suit.Clubs(), Card.Rank.Ace));

            Assert.IsFalse(m.IsSet());
        }

        [TestMethod]
        public void TestSetWithDuplicates()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Ace));
            m.AddCard(new Card(Suit.Clubs(), Card.Rank.Ace));
            m.AddCard(new Card(Suit.Spades(), Card.Rank.Ace));
            m.AddCard(new Card(Suit.Spades(), Card.Rank.Ace));

            Assert.IsFalse(m.IsSet());
        }

        [TestMethod]
        public void TestValidThreeCardSet()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Ace));
            m.AddCard(new Card(Suit.Clubs(), Card.Rank.Ace));
            m.AddCard(new Card(Suit.Spades(), Card.Rank.Ace));

            Assert.IsTrue(m.IsSet());
        }

        [TestMethod]
        public void TestValidFourCardSet()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Five));
            m.AddCard(new Card(Suit.Clubs(), Card.Rank.Five));
            m.AddCard(new Card(Suit.Spades(), Card.Rank.Five));
            m.AddCard(new Card(Suit.Diamonds(), Card.Rank.Five));

            Assert.IsTrue(m.IsSet());
        }
    }
}
