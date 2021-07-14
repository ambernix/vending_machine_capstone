using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone
{
    [TestClass]
    public class HelperMethodsTests
    {
        [TestMethod]
        public void GetChangeTest()
        {
            string resultOne = HelperMethods.GetChange(5);
            string resultTwo = HelperMethods.GetChange(10);
            string resultThree = HelperMethods.GetChange(7.50M);
            string resultFour = HelperMethods.GetChange(4.05M);
            string resultFive = HelperMethods.GetChange(6.10M);
            string resultSix = HelperMethods.GetChange(5.15M);

            string expectedOne = $"Your change is 20 quarters, 0 dimes, and 0 nickels";
            string expectedTwo = $"Your change is 40 quarters, 0 dimes, and 0 nickels";
            string expectedThree = $"Your change is 30 quarters, 0 dimes, and 0 nickels";
            string expectedFour = $"Your change is 16 quarters, 0 dimes, and 1 nickels";
            string expectedFive = $"Your change is 24 quarters, 1 dimes, and 0 nickels";
            string expectedSix = $"Your change is 20 quarters, 1 dimes, and 1 nickels";

            Assert.AreEqual(expectedOne, resultOne);
            Assert.AreEqual(expectedTwo, resultTwo);
            Assert.AreEqual(expectedThree, resultThree);
            Assert.AreEqual(expectedFour, resultFour);
            Assert.AreEqual(expectedFive, resultFive);
            Assert.AreEqual(expectedSix, resultSix);
        }

    }
}
