using System;


namespace AccessModifiers
{
    public class PublicClass
    {
        #region Public Member
        public int PublicField;
        #endregion

        #region Public Method
        /// <summary>
        ///     Called Internal Class 
        /// </summary>
        public void PublicMethod()
        {
            // called Internal class - same assembly
            InternalClass objInternalClass = new InternalClass();
            objInternalClass.InternalMethod();

            Console.WriteLine("Public Method !!");
        }
        #endregion
    }

    class BaseClass
    {
        #region Protected Member
        protected int protectedField = 234243;
        #endregion

        #region Protected Method
        protected void ProtectedMethod()
        {
            Console.WriteLine("Protected Method !!");
        }
        #endregion
    }


    // Inheritate protected method from base class 
    class DerivedClass : BaseClass
    {
        #region Public Method 

        /// <summary>
        ///     Access the protected method from base class 
        /// </summary>
        public void SomeMethod()
        {
            Console.WriteLine(protectedField); // Accessible in derived class
            ProtectedMethod(); // Accessible in derived class
        }
        #endregion
    }


    class PrivateClass
    {
        #region Private Member 
        private int privateField;
        #endregion

        #region Private Method
        private void PrivateMethod()
        {
            Console.WriteLine("Private Method !!");
        }
        #endregion
    }

    internal class InternalClass
    {
        #region Internal Member
        internal int InternalField;
        #endregion

        #region Internal Method
        internal void InternalMethod()
        {
            Console.WriteLine("Internal Method !!");
        }
        #endregion
    }
    class Progrram
    {
        public static void Main(string[] args)
        {
            PublicClass objPublicClass = new PublicClass();
            objPublicClass.PublicMethod();          // call the internal method

            InternalClass objInternalClass = new InternalClass();
            objInternalClass.InternalMethod();      // call the internal method


            DerivedClass objDerivedClass = new DerivedClass();
            objDerivedClass.SomeMethod();       // call the protected method
        }
    }

}
