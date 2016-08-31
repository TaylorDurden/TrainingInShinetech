
namespace StrategyPattern
{
    public class InterestOperation
    {
        private ITaxStrategy _strategy;
        public InterestOperation(ITaxStrategy strategy) {
            _strategy = strategy;
        }

        public double GetTax(double income) {
            return _strategy.CalculateTax(income);
        }
    }
}
