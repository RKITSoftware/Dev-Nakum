class Progrram
{
    public class PublicClass
    {
        public int PublicField;
        public void PublicMethod()
        {
            // called Internal class - same assembly
            InternalClass internalClass = new InternalClass();
            internalClass.InternalMethod();

            Console.WriteLine("Public Method !!");
        }
    }

    class BaseClass
    {
        protected int protectedField  = 234243;
        protected void ProtectedMethod()
        {
            Console.WriteLine("Protected Method !!");
        }
    }

    class DerivedClass : BaseClass
    {
        void SomeMethod()
        {
            Console.WriteLine(protectedField); // Accessible in derived class
            ProtectedMethod(); // Accessible in derived class
        }
    }


    class PrivateClass
    {
        private int privateField;
        private void PrivateMethod()
        {
            Console.WriteLine("Private Method !!");
        }
    }

    internal class InternalClass
    {
        internal int InternalField;
        internal void InternalMethod()
        {
            Console.WriteLine("Internal Method !!");
        }
    }



    public static void Main(string[] args)
    {
        PublicClass publicClass = new PublicClass();
        publicClass.PublicMethod();
    }
}