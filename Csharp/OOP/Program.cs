// Encapsulation and Inheritance    
using System.ComponentModel.DataAnnotations;

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
        
    public void PrintFullName()
    {
        Console.WriteLine("Full name is "+this._firstName +" "+this._lastName);
    }
#endregion
}

class VIPCoustomer : Customer
{
    #region Public Member

    public bool isVIP;

    #endregion

    #region Public Methods
    public VIPCoustomer(string firstName, string lastName):base(firstName,lastName)
    {
        this.isVIP = true;
    }
    public void PrintFullName()
    {
        base.PrintFullName();
        Console.WriteLine("Thank you for using our VIP service !!");
    }
    #endregion
}

// Polymorphism
class Animal
{
    public virtual void AnimalSound()
    {
        Console.WriteLine("The animal makes sound");
    }
}

class Pig : Animal
{
    public override void AnimalSound()
    {
        Console.WriteLine("The pig says: wee wee");
    }
}

class Dog : Animal
{
    public override void AnimalSound()
    {
        Console.WriteLine("The dog says: bow bow");
    }
}

// Abstraction
abstract class Shape
{
    public abstract void Draw();
}

class Circle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Circle is draw");
    }
}

//interface
interface IShape
{
    void Draw();    
    int Area { get; }
}

class Square : IShape
{
    int _length;
    public Square(int length)
    {
        this._length = length;
    }

    public void Draw()
    {
        Console.WriteLine("Dawing a Square");
    }

    public int Area
    {
        get { return _length * _length; }
    }
}


class Program
{
    public static void Main(string[] args)
    {
        Customer c1 = new Customer("Dev", "Nakum");
        c1.PrintFullName();
        Console.WriteLine(c1.FirstName);
        c1.FirstName = "Kishan";
        c1.PrintFullName();

        Customer c2 = new Customer();

        VIPCoustomer vIPCoustomer = new VIPCoustomer("Kishan","Nakum");
        vIPCoustomer.PrintFullName();


        Animal myAnimal = new Animal();
        Animal pig = new Pig();
        Animal dog = new Dog();

        myAnimal.AnimalSound();
        pig.AnimalSound();
        dog.AnimalSound();  

        Circle circle = new Circle(); 
        circle.Draw();  


        IShape square = new Square(5);
        square.Draw();
        Console.WriteLine($"Area of Square is : {square.Area}");
    }
}