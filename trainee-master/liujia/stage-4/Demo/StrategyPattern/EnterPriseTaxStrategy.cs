
namespace StrategyPattern
{
    /// <summary>
    /// 企业所得税
    /// </summary>
    public class EnterPriseTaxStrategy:ITaxStrategy
    {
        public double CalculateTax(double income) {
            return (income - 3500) > 0 ? (income - 3500) * 0.045 : 0.0;
        }
    }
}
