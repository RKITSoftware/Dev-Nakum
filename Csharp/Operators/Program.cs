using System;

class Program
{
    public static void Main(string[] args)
    {
        // Arithmetic Operators
        int x = 10, y = 5;
        Console.WriteLine("x+y : {0}", x + y);
        Console.WriteLine("x-y : {0}", x - y);
        Console.WriteLine("x*y : {0}", x * y);
        Console.WriteLine("x/y : {0}", x / y);
        Console.WriteLine("x%y : {0}", x % y);

        // Relational Operators
        Console.WriteLine("x==y : {0}", x == y);
        Console.WriteLine("x!=y : {0}", x != y);
        Console.WriteLine("x>y : {0}", x > y);
        Console.WriteLine("x>=y : {0}", x >= y);
        Console.WriteLine("x<y : {0}", x < y);
        Console.WriteLine("x<=y : {0}", x <= y);

        // Logical Operators
        bool one = true, two = false;
        Console.WriteLine("one && two : {0}", one && two);
        Console.WriteLine("one || two : {0}", one || two);
        Console.WriteLine("!one : {0}", !one);

        // Bitwise Operators
        Console.WriteLine("3&4 : {0}", 3 & 4);
        Console.WriteLine("3|4 : {0}", 3 | 4);
        Console.WriteLine("3^4 : {0}", 3 ^ 4);
        Console.WriteLine("~3 : {0}", ~3);
        Console.WriteLine("3<<1 : {0}", 3 << 1);
        Console.WriteLine("3>>1 : {0}", 3 >> 1);

        // Assignment Operators
        x += 2;
        Console.WriteLine("Value of X is " + x);

        // Conditional Operator
        int res = x > y ? 5 : 6;
        Console.WriteLine(res);
    }
}