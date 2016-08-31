
namespace Builder
{
    public class ConcreateBuilder2 : Builder
    {
        private Computer computer = new Computer();
        public void BuildPartCPU()
        {
            computer.Add("CPU2");
        }

        public void BuildPartMainBoard()
        {
            computer.Add("Main Board2");
        }

        public Computer GetComputer()
        {
            return computer;
        }
    }
}
