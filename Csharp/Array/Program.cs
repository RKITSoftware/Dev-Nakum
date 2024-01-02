class Program
{
    public static void Main(string[] args)
    {
        int[] luckyNumbers = [1, 34, 6, 3, 322, 232];
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

        // jagged array -- array of arrays
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
}