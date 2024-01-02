using PrTA = ProjectA.TeamA;
using PrTB = ProjectA.TeamB;
class Program
{
    public static void Main(string[] args)
    {
        PrTA.ClassA.Print();
        PrTB.ClassA.Print();
    }
}