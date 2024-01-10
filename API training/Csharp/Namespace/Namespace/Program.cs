using System;
using PrTA = ProjectA.TeamA;
using PrTB = ProjectA.TeamB;

namespace Namespace
{
    class Program
    {
        public static void Main(string[] args)
        {
            PrTA.ClassA.Print();
            PrTB.ClassA.Print();
        }
    }
}
