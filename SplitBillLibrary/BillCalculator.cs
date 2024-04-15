namespace SplitBillLibrary
{
    public class BillCalculator {
        public decimal SplitAmount(decimal totalAmount, int numberOfPeople) {
            if (numberOfPeople <= 0)
                throw new ArgumentException("Number of people must be greater than zero.");
            return totalAmount / numberOfPeople;
        }

        // Method to calculate tip per person based on a total price, number of patrons, and tip percentage
        public decimal TipPerPerson(decimal totalPrice, int numberOfPatrons, float tipPercentage) {
            if (numberOfPatrons <= 0)
                throw new ArgumentException("Number of patrons must be greater than zero.");
            decimal totalTip = totalPrice * (decimal)(tipPercentage / 100);
            return Math.Round(totalTip / numberOfPatrons, 2);
        }

        // Method to calculate individual tips based on meal costs and tip percentage
        public Dictionary<string, decimal> CalculateIndividualTips(Dictionary<string, decimal> mealCosts, float tipPercentage) {
            var tipAmounts = new Dictionary<string, decimal>();
            foreach (var entry in mealCosts) {
                decimal tipAmount = entry.Value * (decimal)(tipPercentage / 100);
                tipAmounts.Add(entry.Key, Math.Round(tipAmount, 2));
            }
            return tipAmounts;
        }
    }
}
