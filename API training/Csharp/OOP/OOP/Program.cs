using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    // Encapsulation and Inheritance    
    class Customer
    {
        #region Private Member

        string _firstName, _lastName;

        #endregion

        #region Constructor
        public Customer()
        {
            Console.WriteLine("No name is assigned");
        }
        public Customer(string firstName, string lastName)
        {
            this._firstName = firstName;
            this._lastName = lastName;
        }
        #endregion

        #region Public Methods
        public string FirstName
        {
            get { return _firstName; }
            set { this._firstName = value; }
        }

        /// <summary>
        ///     Print the full name of customer
        /// </summary>
        public void PrintFullName()
        {
            Console.WriteLine("Full name is " + this._firstName + " " + this._lastName);
        }
        #endregion
    }


    // inheritance
    class VIPCoustomer : Customer
    {
        #region Public Member

        public bool isVIP;

        #endregion

        #region Constructor
        public VIPCoustomer(string firstName, string lastName) : base(firstName, lastName)
        {
            this.isVIP = true;
        }
        #endregion

        #region Public Methods

        /// <summary>
        ///     Print the full name 
        ///     if customer is VIP - print the message 
        /// </summary>
        public void PrintFullName()
        {
            base.PrintFullName();
            if (isVIP)
            {
                Console.WriteLine("Thank you for using our VIP service !!");
            }
        }
        #endregion
    }

    // Polymorphism and override
    class Animal
    {
        #region Public Method 

        /// <summary>
        ///     make virtual method
        /// </summary>
        public virtual void AnimalSound()
        {
            Console.WriteLine("The animal makes sound");
        }
        #endregion
    }

    class Pig : Animal
    {
        #region Public Method

        /// <summary>
        ///     make override method 
        /// </summary>
        public override void AnimalSound()
        {
            Console.WriteLine("The pig says: wee wee");
        }
        #endregion
    }

    class Dog : Animal
    {
        #region Public Method

        /// <summary>
        ///     make override method 
        /// </summary>
        public override void AnimalSound()
        {
            Console.WriteLine("The dog says: bow bow");
        }
        #endregion
    }

    // Abstraction
    abstract class Shape
    {
        #region Public Method
        /// <summary>
        ///     abstract method draw
        /// </summary>
        public abstract void Draw();
        #endregion
    }

    class Circle : Shape
    {
        #region Public Method 

        /// <summary>
        ///     override abstract method
        /// </summary>
        public override void Draw()
        {
            Console.WriteLine("Circle is draw");
        }
        #endregion  
    }

    //interface
    interface IShape
    {
        #region Private Method
        void Draw();
        int Area { get; }
        #endregion
    }

    class Square : IShape
    {
        #region Private Member
        int _length;
        #endregion

        #region Constructor
        public Square(int length)
        {
            this._length = length;
        }
        #endregion

        #region Public Method

        /// <summary>
        ///     Override the method from interface
        /// </summary>
        public void Draw()
        {
            Console.WriteLine("Drawing a Square");
        }

        /// <summary>
        ///     returns the area of sqare
        /// </summary>
        public int Area
        {
            get { return _length * _length; }
        }
        #endregion
    }


    class Program
    {
        public static void Main(string[] args)
        {
            Customer objCustomer = new Customer("Dev", "Nakum");
            objCustomer.PrintFullName();
            Console.WriteLine(objCustomer.FirstName);
            objCustomer.FirstName = "Kishan";
            objCustomer.PrintFullName();



            VIPCoustomer objVIPCoustomer = new VIPCoustomer("Kishan", "Nakum");
            objVIPCoustomer.PrintFullName();


            Animal objAnimal = new Animal();
            Animal objAnimalPig = new Pig();
            Animal objAnimalDog = new Dog();

            objAnimal.AnimalSound();
            objAnimalPig.AnimalSound();
            objAnimalDog.AnimalSound();

            Circle objCircle = new Circle();
            objCircle.Draw();


            IShape objIShapeSquare = new Square(5);
            objIShapeSquare.Draw();
            Console.WriteLine($"Area of Square is : {objIShapeSquare.Area}");

            Console.ReadLine();
        }
    }
}
