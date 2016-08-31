
namespace Builder
{
    public interface Builder
    {
        //装CPU
        void BuildPartCPU();

        //装主板
        void BuildPartMainBoard();

        //获取组装好的电脑
        Computer GetComputer();
    }
}
