using System;

class Program
{
    public static void Main(string[] args)
    {
        string characterName = "Dev";       // used to store sequence of the character
        int characterAge = 21;      // used to store the numeric value
        char grade = 'A';       // used to store the character value

        // float, decimal and double stored the fraction value
        float percentage = 98.94f;
        double value = 121.1212;
        decimal decimalValue = 1231.123121312313131m;

        bool isValid = true;        // stored only true or false


        Console.WriteLine("Name of the character is " + characterName);
        Console.WriteLine("Age ${} the charater is " + characterAge);

        Console.WriteLine("float: " + percentage);
        Console.WriteLine("double: " + value);
        Console.WriteLine("decimal: " + decimalValue);
    }
}
