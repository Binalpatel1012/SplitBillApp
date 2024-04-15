using SplitBillLibrary;

namespace SplitBillTests
{
    [TestClass]
    public class BillCalculatorTests {
        private readonly BillCalculator _calculator = new BillCalculator();

        [TestMethod]
        public void SplitAmount_DividesTotalAmountEvenly() {
            decimal totalAmount = 200m;
            int numberOfPeople = 4;
            decimal result = _calculator.SplitAmount(totalAmount, numberOfPeople);
            Assert.AreEqual(50m, result);
        }

        [TestMethod]
        public void SplitAmount_DividesTotalAmountWithRounding() {
            decimal totalAmount = 100m;
            int numberOfPeople = 3;
            decimal result = _calculator.SplitAmount(totalAmount, numberOfPeople);
            Assert.AreEqual(33.33m, result);
        }

        [TestMethod]
        public void SplitAmount_WithOnePersonReturnsSameAmount() {
            decimal totalAmount = 50m;
            int numberOfPeople = 1;
            decimal result = _calculator.SplitAmount(totalAmount, numberOfPeople);
            Assert.AreEqual(totalAmount, result);
        }
    }
}
