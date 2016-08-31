
namespace StrategyPattern
{
    //计算所得税策略
    public interface ITaxStrategy
    {
        double CalculateTax(double income);
    }
}
