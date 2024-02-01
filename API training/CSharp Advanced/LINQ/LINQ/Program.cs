namespace LINQ
{
    /// <summary>
    /// main class for executing the program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method - create the objects and called the method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            LinqWithDataTable objLinqWithDataTable = new LinqWithDataTable();
            objLinqWithDataTable.DataAdd();
            objLinqWithDataTable.ExecuteQuery();

            LinqWithList objLinqWithList = new LinqWithList();
            objLinqWithList.ListOperation();
        }
    }
}
