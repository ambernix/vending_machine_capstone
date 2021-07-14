using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Capstone
{
    [TestClass]
    public class VendingTests
    {
        [TestMethod]
        public void ReStockTest()
        {
            Vending vending = new Vending("txt");

            vending.ReStock(new string[] {"A1", "SlimJim", "1.50", "Chip" });

            Item chip = new Chips("SlimJim", 1.50M, "A1");

            vending.Stock.Add(chip);
            
            Assert.AreEqual(vending.Stock[1].SlotNumber, vending.Stock[0].SlotNumber);
            Assert.AreEqual(vending.Stock[1].Name, vending.Stock[0].Name);
            Assert.AreEqual(vending.Stock[1].Price, vending.Stock[0].Price);
            Assert.AreEqual(vending.Stock[1].MakeSound(), vending.Stock[0].MakeSound());
        }

        [TestMethod]
        public void FeedMoneyTest()
        {
            Vending vendingFirst = new Vending("txt");
            using (var reader = new StringReader("5"))
            {
                Console.SetIn(reader);
                vendingFirst.FeedMoney();
            }

            Vending vendingSecond = new Vending("txt");
            using (var reader = new StringReader("10"))
            {
                Console.SetIn(reader);
                vendingSecond.FeedMoney();
            }

            Vending vendingThird = new Vending("txt");
            using (var reader = new StringReader(@"4 
            k"))
            {
                Console.SetIn(reader);
                vendingThird.FeedMoney();
            }

            Vending vendingFourth = new Vending("txt");
            using (var reader = new StringReader("10"))
            {
                Console.SetIn(reader);
                vendingFourth.FeedMoney();
            }
            using (var reader = new StringReader(@"5 
            k"))
            {
                Console.SetIn(reader);
                vendingFourth.FeedMoney();
            }

            Vending vendingFifth = new Vending("txt");
            using (var reader = new StringReader(@"g 
            k"))
            {
                Console.SetIn(reader);
                vendingFifth.FeedMoney();
            }

            Assert.AreEqual(5, vendingFirst.CurrentMoneyIn);
            Assert.AreEqual(10, vendingSecond.CurrentMoneyIn);
            Assert.AreEqual(0, vendingThird.CurrentMoneyIn);
            Assert.AreEqual(10, vendingFourth.CurrentMoneyIn);
            Assert.AreEqual(0, vendingFifth.CurrentMoneyIn);
        }

    }
}

