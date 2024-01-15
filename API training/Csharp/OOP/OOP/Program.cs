using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class Program
    {   
        /// <summary>
        ///     Call the all 4 pillors of OOP
        ///     perform operations
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // encapsulation
            Customer objCustomer = new Customer("Dev", "Nakum");
            objCustomer.PrintFullName();
            Console.WriteLine(objCustomer.FirstName);
            objCustomer.FirstName = "Kishan";
            objCustomer.PrintFullName();
            Console.WriteLine();

            // inheritance
            VIPCoustomer objVIPCoustomer = new VIPCoustomer("Kishan", "Nakum");
            objVIPCoustomer.PrintFullName();
            Console.WriteLine();
            VIPCoustomer objExtraVIPCustomer = new ExtraVIPCustomer("John", "Doe");
            objExtraVIPCustomer.PrintFullName() ;
            Console.WriteLine();
            D objD = new D();
            objD.MethodD();
            B objB = new B();
            objB.MethodB();
            C objC = new C();
            objC.MethodC();
            Console.WriteLine();

            // polymorphism
            Animal objAnimal = new Animal();
            Animal objAnimalPig = new Pig();
            Animal objAnimalDog = new Dog();

            objAnimal.AnimalSound();
            objAnimalPig.AnimalSound();
            objAnimalDog.AnimalSound();
            Console.WriteLine();
            Dog objDog = new Dog();
            objDog.OverloadMethod(1);
            objDog.OverloadMethod(2,3);
            Console.WriteLine();
            //string str = objDog.OverloadMethodWithOptional();  //it gives ambiguous error

            // Abstraction
            Circle objCircle = new Circle();
            objCircle.Draw();
            objCircle.Welcome();
            Console.WriteLine();

            // interface
            IShape objIShapeSquare = new Square(5);
            objIShapeSquare.Draw();
            Console.WriteLine($"Area of Square is : {objIShapeSquare.Area}");
            Console.WriteLine();
            Circle2 objCircle2 = new Circle2(5);   
            objCircle2.Draw();
            objCircle2.Draw2();
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
