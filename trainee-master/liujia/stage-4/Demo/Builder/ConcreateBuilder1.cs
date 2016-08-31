
namespace Builder
{
    //集体创建者
    public class ConcreateBuilder1 : Builder
    {
        private Computer computer = new Computer();
        public void BuildPartCPU()
        {
            computer.Add("CPU1");
        }

        public void BuildPartMainBoard()
        {
            computer.Add("Main Board1");
        }

        public Computer GetComputer()
        {
            return computer;
        }
    }
}
