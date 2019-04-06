using System;
using System.Collections.Generic;
using gin_rummy.Actors;
using gin_rummy.Cards;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test_gin_rummy.TestLogic
{
    [TestClass]
    public class TestMeldChecker
    {

        private MeldChecker _checker;

        [TestInitialize]
        public void Initialize()
        {
            _checker = new MeldChecker();
        }

        [TestMethod]
        public void TestGetAllMeldsZero()
        {
            List<Card> cards = new List<Card>();

            Assert.AreEqual(0, _checker.GetAllPossibleMelds(cards).Count);
        }

        [TestMethod]
        public void TestGetAllMeldsRuns()
        {
            List<Card> cards = new List<Card>();
            cards.Add(new Card("Ah"));
            cards.Add(new Card("2h"));
            cards.Add(new Card("3h"));
            cards.Add(new Card("4h"));
            cards.Add(new Card("Ts")); // Deliberate spade
            cards.Add(new Card("Jh"));
            cards.Add(new Card("Qh"));
            cards.Add(new Card("Kh"));

            Assert.AreEqual(4, _checker.GetAllPossibleMelds(cards).Count); // A-3,A-4,2-4,J-K
        }

        [TestMethod]
        public void TestGetAllMeldsSets()
        {
            List<Card> cards = new List<Card>();
            cards.Add(new Card("Ah"));
            cards.Add(new Card("Ac"));
            cards.Add(new Card("As"));
            cards.Add(new Card("Ad"));

            Assert.AreEqual(5, _checker.GetAllPossibleMelds(cards).Count); // 4 x 3 Aces, 1 x 4 Aces
        }

        [TestMethod]
        public void TestGetAllMeldsSampleHand()
        {
            List<Card> cards = new List<Card>();
            cards.Add(new Card("Kh"));
            cards.Add(new Card("Kc"));
            cards.Add(new Card("Ks"));
            cards.Add(new Card("Jh"));
            cards.Add(new Card("Jc"));
            cards.Add(new Card("Js"));
            cards.Add(new Card("Qh"));
            cards.Add(new Card("Qc"));
            cards.Add(new Card("Qs"));
            cards.Add(new Card("Ad"));

            Assert.AreEqual(6, _checker.GetAllPossibleMelds(cards).Count); // 3 x 3 Jacks/Queens/Kings, 3 x JQK
        }

        [TestMethod]
        public void TestGetAllMeldSetsZero()
        {
            List<Card> cards = new List<Card>();

            Assert.AreEqual(0, _checker.GetAllPossibleMeldSets(cards).Count);
        }

        [TestMethod]
        public void TestGetAllMeldSetsRuns()
        {
            List<Card> cards = new List<Card>();
            cards.Add(new Card("Ah"));
            cards.Add(new Card("2h"));
            cards.Add(new Card("3h"));
            cards.Add(new Card("4h"));
            cards.Add(new Card("Ts")); // Deliberate spade
            cards.Add(new Card("Jh"));
            cards.Add(new Card("Qh"));
            cards.Add(new Card("Kh"));

            Assert.AreEqual(3, _checker.GetAllPossibleMeldSets(cards).Count); // [A-3,J-K],[A-4,J-K],[2-4,J-K]
        }

        [TestMethod]
        public void TestGetAllMeldSetsSets()
        {
            List<Card> cards = new List<Card>();
            cards.Add(new Card("Ah"));
            cards.Add(new Card("Ac"));
            cards.Add(new Card("As"));
            cards.Add(new Card("Ad"));
            cards.Add(new Card("Kh"));
            cards.Add(new Card("Kc"));
            cards.Add(new Card("Ks"));

            Assert.AreEqual(5, _checker.GetAllPossibleMeldSets(cards).Count); // 4 x 3 Aces + 3 Kings, 1 x 4 Aces + 3 Kings
        }

        [TestMethod]
        public void TestGetDeadWoodNoMelds()
        {
            var cards = new List<Card>();
            cards.Add(new Card("As"));
            cards.Add(new Card("Qh"));
            cards.Add(new Card("5d"));

            var meldSet = new List<Meld>();

            Assert.AreEqual(3, _checker.GetDeadWood(meldSet, cards).Count);
        }

        [TestMethod]
        public void TestGetDeadWoodOneMeld()
        {
            var cards = new List<Card>();
            cards.Add(new Card("As"));
            cards.Add(new Card("Qh"));
            cards.Add(new Card("5d"));
            cards.Add(new Card("6d"));
            cards.Add(new Card("7d"));

            var meldSet = new List<Meld>();

            Meld meld = new Meld();
            meld.AddCard(new Card("5d"));
            meld.AddCard(new Card("6d"));
            meld.AddCard(new Card("7d"));
            meldSet.Add(meld);

            Assert.AreEqual(2, _checker.GetDeadWood(meldSet, cards).Count);
        }

        [TestMethod]
        public void TestGetDeadWoodTwoMelds()
        {
            var cards = new List<Card>();
            cards.Add(new Card("As"));
            cards.Add(new Card("Qh"));
            cards.Add(new Card("5d"));
            cards.Add(new Card("6d"));
            cards.Add(new Card("7d"));
            cards.Add(new Card("7c"));
            cards.Add(new Card("Tc"));
            cards.Add(new Card("Jc"));
            cards.Add(new Card("Qc"));

            var meldSet = new List<Meld>();

            Meld meldA = new Meld();
            meldA.AddCard(new Card("5d"));
            meldA.AddCard(new Card("6d"));
            meldA.AddCard(new Card("7d"));
            meldSet.Add(meldA);

            Meld meldB = new Meld();
            meldB.AddCard(new Card("Tc"));
            meldB.AddCard(new Card("Jc"));
            meldB.AddCard(new Card("Qc"));
            meldSet.Add(meldB);

            Assert.AreEqual(3, _checker.GetDeadWood(meldSet, cards).Count);
        }

    }
}
