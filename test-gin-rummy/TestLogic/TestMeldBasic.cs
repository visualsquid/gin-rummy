using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using gin_rummy.Cards;
using gin_rummy.Actors;

namespace test_gin_rummy.Cards
{
    /// <summary>
    /// Tests basic methods and properties of the Meld class.
    /// </summary>
    [TestClass]
    public class TestMeldBasic
    {
        private const int InitialMeldSize = 0;

        private Meld _currentMeld;

        [TestInitialize]
        public void Initialize()
        {
            _currentMeld = new Meld();
        }

        [TestMethod]
        public void TestCreate()
        {
            Assert.AreEqual(InitialMeldSize, _currentMeld.Size);
        }

        [TestMethod]
        public void TestAddOneAndClear()
        {
            _currentMeld.AddCard(new Card());
            Assert.AreEqual(InitialMeldSize + 1, _currentMeld.Size);

            _currentMeld.Clear();
            Assert.AreEqual(InitialMeldSize, _currentMeld.Size);
        }

        [TestMethod]
        public void TestAddManyAndClear()
        {
            _currentMeld.AddCard(new Card());
            _currentMeld.AddCard(new Card());
            _currentMeld.AddCard(new Card());
            Assert.AreEqual(InitialMeldSize + 3, _currentMeld.Size);

            _currentMeld.Clear();
            Assert.AreEqual(InitialMeldSize, _currentMeld.Size);
        }

        [TestMethod]
        public void TestAddNoneAndClear()
        {
            _currentMeld.Clear();
            Assert.AreEqual(InitialMeldSize, _currentMeld.Size);
        }

        [TestMethod]
        public void TestAddNoneAndRemove()
        {
            Assert.IsFalse(_currentMeld.RemoveCard(new Card()));
            Assert.AreEqual(InitialMeldSize, _currentMeld.Size);
        }

        [TestMethod]
        public void TestAddOneAndRemove()
        {
            Card c = new Card();
            _currentMeld.AddCard(c);
            Assert.IsTrue(_currentMeld.RemoveCard(c));
            Assert.AreEqual(InitialMeldSize, _currentMeld.Size);
        }

        public void TestAddManyAndRemoveOne()
        {
            Card x = new Card();
            Card y = new Card();
            Card z = new Card();

            _currentMeld.AddCard(x);
            _currentMeld.AddCard(y);
            _currentMeld.AddCard(z);

            Assert.IsTrue(_currentMeld.RemoveCard(y));
            Assert.AreEqual(InitialMeldSize + 3 - 1, _currentMeld.Size);
            Assert.IsFalse(_currentMeld.GetListOfCardsInMeld().Contains(y));
        }

        [TestMethod]
        public void TestDoMeldsOverlapNegativeEmpty()
        {
            Meld meldA = new Meld();
            Meld meldB = new Meld();

            Assert.IsFalse(meldA.DoesOverlap(meldB));
        }

        [TestMethod]
        public void TestDoMeldsOverlapNegativeSeparate()
        {
            Meld meldA = new Meld();
            Meld meldB = new Meld();

            meldA.AddCard(new Card("Ah"));
            meldA.AddCard(new Card("As"));
            meldA.AddCard(new Card("Ac"));

            meldB.AddCard(new Card("Kh"));
            meldB.AddCard(new Card("Ks"));
            meldB.AddCard(new Card("Kc"));

            Assert.IsFalse(meldA.DoesOverlap(meldB));
        }

        [TestMethod]
        public void TestDoMeldsOverlapPositive()
        {
            Meld meldA = new Meld();
            Meld meldB = new Meld();

            meldA.AddCard(new Card("Ah"));
            meldA.AddCard(new Card("As"));
            meldA.AddCard(new Card("Ac"));

            meldB.AddCard(new Card("Ah"));
            meldB.AddCard(new Card("As"));
            meldB.AddCard(new Card("Ad"));

            Assert.IsTrue(meldA.DoesOverlap(meldB));
        }
    }
}
