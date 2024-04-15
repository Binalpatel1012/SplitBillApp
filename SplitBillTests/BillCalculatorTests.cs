using SplitBillLibrary;

namespace SplitBillTests
{
    [TestClass]
    public class BillCalculatorTests {
        private readonly BillCalculator _calculator = new BillCalculator();

        // Tests for SplitAmount method
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

        // Tests for CalculateIndividualTips method
        [TestMethod]
        public void CalculateIndividualTips_SimpleCases_CalculatesCorrectly() {
            var mealCosts = new Dictionary<string, decimal> {
                { "Binal", 120m },
                { "Patel", 80m }
            };
            float tipPercentage = 15;

            var tips = _calculator.CalculateIndividualTips(mealCosts, tipPercentage);

            Assert.AreEqual(18m, tips["Binal"]);
            Assert.AreEqual(12m, tips["Patel"]);
        }

        [TestMethod]
        public void CalculateIndividualTips_WhenTipIsZero_ReturnsZeroTip() {
            var mealCosts = new Dictionary<string, decimal> {
                { "Binal", 150m }
            };
            float tipPercentage = 0;

            var tips = _calculator.CalculateIndividualTips(mealCosts, tipPercentage);

            Assert.AreEqual(0m, tips["Binal"]);
        }

        [TestMethod]
        public void CalculateIndividualTips_WithRoundedValues_ReturnsCorrectlyRoundedTips() {
            var mealCosts = new Dictionary<string, decimal> {
                { "Heer", 333m },
                { "Shah", 222m }
            };
            float tipPercentage = 18.5f;

            var tips = _calculator.CalculateIndividualTips(mealCosts, tipPercentage);

            Assert.AreEqual(61.60m, tips["Heer"]);
            Assert.AreEqual(41.07m, tips["Shah"]);
        }

        // Tests for TipPerPerson method
        [TestMethod]
        public void TipPerPerson_EqualSplit_ShouldReturnCorrectAmount() {
            decimal totalPrice = 100m;
            int numberOfPatrons = 4;
            float tipPercentage = 25;  // 25% tip
            decimal expectedTipPerPerson = 6.25m;
            
            decimal actualTipPerPerson = _calculator.TipPerPerson(totalPrice, numberOfPatrons, tipPercentage);

            Assert.AreEqual(expectedTipPerPerson, actualTipPerPerson);
        }

        [TestMethod]
        public void TipPerPerson_UnevenSplit_ShouldHandleRounding() {
            decimal totalPrice = 123.45m;
            int numberOfPatrons = 3;
            float tipPercentage = 15.5f;
            decimal expectedTipPerPerson = 6.37m;
            
            decimal actualTipPerPerson = _calculator.TipPerPerson(totalPrice, numberOfPatrons, tipPercentage);

            Assert.AreEqual(expectedTipPerPerson, actualTipPerPerson);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TipPerPerson_ZeroPatrons_ShouldThrowArgumentException() {
            decimal totalPrice = 200m;
            int numberOfPatrons = 0;  // Zero patrons, should throw exception
            float tipPercentage = 10;
            
            _calculator.TipPerPerson(totalPrice, numberOfPatrons, tipPercentage);  // This should throw an exception
        }
    }
}
