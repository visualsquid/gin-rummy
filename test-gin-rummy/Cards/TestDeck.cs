using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using gin_rummy.Cards;
using System.Collections.Generic;
using System.Linq;

namespace test_gin_rummy
{
    [TestClass]
    public class TestDeck
    {
        private const int StandardDeckSize = 52;

        [TestMethod]
        public void TestInit()
        {
            Deck deck = new Deck();
            Assert.AreEqual(StandardDeckSize, deck.Size);
        }

        [TestMethod]
        public void TestShuffle()
        {
            const int ShuffleLimit = 100;

            Deck deck = new Deck();

            int originalSize = deck.Size;
            for (int i = 0; i < ShuffleLimit; i++)
            {
                deck.Shuffle();
                Assert.AreEqual(originalSize, deck.Size);
            }
            
        }

        [TestMethod]
        public void TestClear()
        {
            Deck deck = new Deck();
            deck.Clear();
            Assert.AreEqual(0, deck.Size);
        }

        [TestMethod]
        public void TestAdd()
        {
            Deck deck = new Deck();
            int originalSize = deck.Size;
            deck.Add(new Card(Suit.Hearts(), Card.Rank.Ace));

            Assert.AreEqual(originalSize + 1, deck.Size);
        }

        [TestMethod]
        public void TestToString()
        {
            Deck deck = new Deck(Deck.DeckType.Standard);
            string s = deck.ToString();
            List<string> values = new List<string>(s.Split(' '));

            Assert.AreEqual(StandardDeckSize, values.Distinct().Count());
        }
    }
}
