using AnotherReferenceProject;

namespace References
{
    /// <summary>
    ///  Class that contains main method from main method call the another projects class method
    /// </summary>
    public class Program
    {
        // class class1 from another project
        static void Main(string[] args)
        {
            // call from anotherReferenceProject's class1 
            Class1.Print();

            AnotherReferenceProject.Program.Print();
        }
    }
}
