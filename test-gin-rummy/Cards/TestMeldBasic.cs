using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using gin_rummy.Cards;

namespace test_gin_rummy.Cards
{
    /// <summary>
    /// Tests basic methods and properties of the Meld class.
    /// </summary>
    [TestClass]
    public class TestMeldBasic
    {
        private const int MeldInitialSize = 0;

        [TestMethod]
        public void TestCreate()
        {
            Meld m = new Meld();
            Assert.AreEqual(MeldInitialSize, m.Size);
        }

        [TestMethod]
        public void TestAddOneAndClear()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Clubs(), Card.Rank.Ace));
            Assert.AreEqual(MeldInitialSize + 1, m.Size);

            m.Clear();
            Assert.AreEqual(MeldInitialSize, m.Size);
        }

        [TestMethod]
        public void TestAddManyAndClear()
        {
            Meld m = new Meld();

            m.AddCard(new Card(Suit.Clubs(), Card.Rank.Ace));
            m.AddCard(new Card(Suit.Hearts(), Card.Rank.Queen));
            m.AddCard(new Card(Suit.Spades(), Card.Rank.Ten));
            Assert.AreEqual(MeldInitialSize + 3, m.Size);

            m.Clear();
            Assert.AreEqual(MeldInitialSize, m.Size);
        }

        [TestMethod]
        public void TestAddNoneAndClear()
        {
            Meld m = new Meld();

            m.Clear();
            Assert.AreEqual(MeldInitialSize, m.Size);
        }
    }
}
