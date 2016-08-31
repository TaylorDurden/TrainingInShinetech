
namespace Builder
{
    //指挥创建过程类
    public class Director
    {
        //组装电脑
        public void Construct(Builder builder) {
            builder.BuildPartCPU();
            builder.BuildPartMainBoard();
        }
    }
}
