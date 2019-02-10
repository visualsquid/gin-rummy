using System;
using gin_rummy.Cards;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test_gin_rummy.Cards
{
    /// <summary>
    /// Tests validity of run-based melds.
    /// </summary>
    [TestClass]
    public class TestMeldRuns
    {
        [TestMethod]
        public void TestEmptyRun()
        {
            Meld m = new Meld();
            Assert.IsFalse(m.IsRun());
        }

        [TestMethod]
        public void TestRunTooSmall()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Ace));
            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Two));

            Assert.IsFalse(m.IsRun());
        }

        [TestMethod]
        public void TestRunWithDuplicates()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Ace));
            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Two));
            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Two));

            Assert.IsFalse(m.IsRun());
        }

        [TestMethod]
        public void TestRunWithGaps()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Ace));
            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Two));
            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Four));

            Assert.IsFalse(m.IsRun());
        }

        [TestMethod]
        public void TestInvalidAceHighRun()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Spades(), Card.Rank.Queen));
            m.AddCard(new Card(Suit.Spades(), Card.Rank.King));
            m.AddCard(new Card(Suit.Spades(), Card.Rank.Ace));

            Assert.IsFalse(m.IsRun());
        }

        [TestMethod]
        public void TestInvalidWrappedMeld()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Spades(), Card.Rank.King));
            m.AddCard(new Card(Suit.Spades(), Card.Rank.Ace));
            m.AddCard(new Card(Suit.Spades(), Card.Rank.Two));

            Assert.IsFalse(m.IsRun());
        }

        [TestMethod]
        public void TestValidThreeCardRun()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Ace));
            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Two));
            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Three));

            Assert.IsTrue(m.IsRun());
        }

        [TestMethod]
        public void TestValidFourCardRun()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Clubs(), Card.Rank.Ten));
            m.AddCard(new Card(Suit.Clubs(), Card.Rank.Jack));
            m.AddCard(new Card(Suit.Clubs(), Card.Rank.Queen));
            m.AddCard(new Card(Suit.Clubs(), Card.Rank.King));

            Assert.IsTrue(m.IsRun());
        }

        [TestMethod]
        public void TestValidRunInMixedOrder()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Diamonds(), Card.Rank.Eight));
            m.AddCard(new Card(Suit.Diamonds(), Card.Rank.Seven));
            m.AddCard(new Card(Suit.Diamonds(), Card.Rank.Five));
            m.AddCard(new Card(Suit.Diamonds(), Card.Rank.Ten));
            m.AddCard(new Card(Suit.Diamonds(), Card.Rank.Six));
            m.AddCard(new Card(Suit.Diamonds(), Card.Rank.Jack));
            m.AddCard(new Card(Suit.Diamonds(), Card.Rank.Nine));

            Assert.IsTrue(m.IsRun());
        }
    }
}
