using System;

namespace Types_of_Classes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // abstract class 
            DerivedClass objDerivedClass = new DerivedClass();
            objDerivedClass.DisplayName();

            // sealed class - can not inheritant 
            SealedClass objSealedClass = new SealedClass();
            objSealedClass.Name = "Dev";            // set Name to Dev
            Console.WriteLine($"Sealed class : name is {objSealedClass.Name}");

            // static class -- can not create instance of static class 
            Console.WriteLine($"Static class method : Addition of two number is {StaticClass.Add(10,5)}");

            // partial class
            PartialClass objPartialClass = new PartialClass();
            objPartialClass.partialMethod();
            objPartialClass.partialAnotherMethod();

        }
    }
}
