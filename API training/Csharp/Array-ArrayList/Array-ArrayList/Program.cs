using System;
using System.Collections;

namespace Array_ArrayList
{
    public class Program
    {
        /// <summary>
        ///     array list method 
        /// </summary>
        public static void CreateArrayList()
        {
            ArrayList objArrayList = new ArrayList();

            objArrayList.Add(1);
            objArrayList.Add(2);
            objArrayList.Add(3);
            objArrayList.Add(4);
            objArrayList.Add(5);

            Console.WriteLine($"Count of arrayList is {objArrayList.Count}");
            Console.WriteLine($"Count of arrayList is {objArrayList.Capacity}");
        }


        /// <summary>
        ///     array method
        /// </summary>
        public static void CreateArrayMethod()
        {
            // Initialize an array
            int[] numbers = { 5, 2, 8, 3, 1 };

            // Clone(): Creates a shallow copy of the array.
            int[] cloneArray = (int[])numbers.Clone();
            Console.WriteLine("Cloned Array: " + string.Join(", ", cloneArray));

            // Copy(): Copies elements from one array to another.
            int[] destinationArray = new int[numbers.Length];
            Array.Copy(cloneArray, destinationArray, cloneArray.Length);
            Console.WriteLine("Copied Array: " + string.Join(", ", destinationArray));

            // CopyTo(): Copies elements from the array to another destination array.
            int[] anotherDestinationArray = new int[numbers.Length];
            numbers.CopyTo(anotherDestinationArray, 0);
            Console.WriteLine("Array copied using CopyTo(): " + string.Join(", ", anotherDestinationArray));

            // ForEach(): Performs the specified action on each element of the array.
            Array.ForEach(numbers, num => Console.Write(num + " "));
            Console.WriteLine();

            // GetValue(): Gets the value at the specified position.
            Console.WriteLine("Value at index 2: " + numbers.GetValue(2));

            // IndexOf(): Returns the index of the specified value in the array.
            int indexOfValue = Array.IndexOf(numbers, 8);
            Console.WriteLine("Index of value 8: " + indexOfValue);

            // Resize(): Changes the size of the array.
            Array.Resize(ref numbers, 3);
            Console.WriteLine("Array after Resize(): " + string.Join(", ", numbers));

            // Reverse(): Reverses the order of the elements in the array.
            Array.Reverse(numbers);
            Console.WriteLine("Array after Reverse(): " + string.Join(", ", numbers));

            // Sort(): Sorts the elements in the array.
            Array.Sort(numbers);
            Console.WriteLine("Array after Sort(): " + string.Join(", ", numbers));

            // ToString(): Converts the array to a string representation.
            string arrayAsString = numbers.ToString();
            Console.WriteLine("Array as string: " + arrayAsString);

            // Clear(): Sets all the elements in the array to their default values (0 for integers).
            Array.Clear(numbers, 0, numbers.Length);
            Console.WriteLine("Array after Clear(): " + string.Join(", ", numbers));
        }


        /// <summary>
        ///     2D array
        /// </summary>
        public static void Create2DArray()
        {
            int[,] intTwoDArray = new int[,] { { 1, 2 }, { 3, 4 } };

            // The same array with dimensions 
            // specified 4 row and 2 column.
            int[,] intarray_d = new int[4, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.Write(intarray_d[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        ///     jagged array
        /// </summary>
        public static void CreateJaggedArray()
        {
            int[][] arr = new int[2][];
            arr[0] = new int[2] { 2, 3 };
            arr[1] = new int[3] { 32, 5, 65 };

            int[][] arrAnother = {
                new int[]{1,2,3,4,5},
                new int[]{6,7,8}
            };

            int row = arrAnother.Length;

            for (int i = 0; i < row; i++)
            {
                int col = arrAnother[i].Length;
                for (int j = 0; j < col; j++)
                {
                    Console.Write(arrAnother[i][j] + " ");
                }
                Console.WriteLine();
            }
        }


        static void Main(string[] args)
        {
            int[] luckyNumbers = { 1, 34, 6, 3, 322, 232 };
            foreach (int item in luckyNumbers)
            {
                Console.WriteLine(item);
            }

            // defining array with size 3. 
            // But not assigns values
            string[] friends = new string[3];
            friends[0] = "Tushar";
            friends[1] = "Divyesh";
            friends[2] = "Pratham";

            // defining array with size 5 and assigning
            // values at the same time
            int[] intArray = new int[5] { 1, 2, 3, 4, 5 };
            foreach (int item in intArray)
            {
                Console.Write(item + " ");
            }


            // 2d array
            //Create2DArray();


            // jagged array -- array of arrays
            //CreateJaggedArray();


            // array method
            
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Array Method Start");
            Console.WriteLine("");
            CreateArrayMethod();

            // array-
            //CreateArrayList();

            Console.ReadLine();
        }
    }
}
