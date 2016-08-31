
namespace StrategyPattern
{
    /// <summary>
    /// 个人所得税
    /// </summary>
    public class PersonalTaxStrategy:ITaxStrategy
    {
        public double CalculateTax(double income) {
            return income * 0.12;
        }
    }
}
