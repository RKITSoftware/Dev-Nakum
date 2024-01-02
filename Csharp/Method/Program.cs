class Program
{
    static int Addition(int x,int y)
    {
        return x + y;
    }
    static int Multiplication(int x, int y)
    {
        return x * y;
    }

    static int Division(int x, int y) { 
        return x / y;
    }

    static int Subtraction(int x, int y) { 
        return x > y ? x-y : y-x;
    }

    public static void Main(string[] args)
    {
        int one = 10, two = 5;

        Console.WriteLine("Addition is : " + Addition(one, two));
        Console.WriteLine("Multiplication is : " + Multiplication(one, two));
        Console.WriteLine("Subtraction is : " + Subtraction(one, two));
        Console.WriteLine("Division is : " + Division(one, two));

    }
    
}