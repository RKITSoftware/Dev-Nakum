using PrTA = ProjectA.TeamA;
using PrTB = ProjectA.TeamB;

namespace Namespace
{
    /// <summary>
    /// call the method of another namespace
    /// </summary>
    class Program
    {
        public static void Main(string[] args)
        {
            PrTA.ClassA.Print();        // call the ProjectA.TeamA namespace
            PrTB.ClassA.Print();        // call the ProjectA.TeamB namespace
        }
    }
}
