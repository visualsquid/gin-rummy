using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using gin_rummy.Cards;

namespace test_gin_rummy.Cards
{
    [TestClass]
    public class TestTable
    {
        private const int InitialStockCount = 52;
        private const int InitialDiscardCount = 0;

        [TestMethod]
        public void TestInit()
        {
            

            var table = new Table();
            Assert.AreEqual(InitialStockCount, table.StockCount);
            Assert.AreEqual(InitialDiscardCount, table.DiscardCount);
        }

        [TestMethod]
        public void TestDrawAndPlace()
        {
            var table = new Table();
            var card = table.DrawStock();
            table.PlaceDiscard(card);

            Assert.AreEqual(InitialStockCount - 1, table.StockCount);
            Assert.AreEqual(InitialDiscardCount + 1, table.DiscardCount);
        }

        [TestMethod]
        public void TestDepleteAndRestock()
        {
            var table = new Table();

            while (table.StockCount > 0)
            {
                var card = table.DrawStock();
                table.PlaceDiscard(card);
            }

            table.RestockFromDiscard();

            Assert.AreEqual(InitialStockCount, table.StockCount);
            Assert.AreEqual(InitialDiscardCount, table.DiscardCount);
        }

        [TestMethod]
        public void TestDrawDiscard()
        {
            var table = new Table();
            var card = table.DrawStock();
            table.PlaceDiscard(card);
            var secondCard = table.DrawDiscard();

            Assert.AreEqual(card, secondCard);
            Assert.AreEqual(InitialDiscardCount, table.DiscardCount);
            Assert.AreEqual(InitialStockCount - 1, table.StockCount);
        }

    }
}
