using System;


namespace AccessModifiers
{
    /// <summary>
    /// public class which contains all the method and member is public
    /// </summary>
    public class PublicClass
    {
        #region Public Member
        public int publicField;
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

    /// <summary>
    /// Base class which contains protected member and method
    /// </summary>
    class BaseClass
    {
        #region Protected Member
        protected int protectedField = 234243;
        #endregion

        #region Protected Method
        /// <summary>
        ///     display the message
        /// </summary>
        protected void ProtectedMethod()
        {
            Console.WriteLine("Protected Method !!");
        }
        #endregion
    }


    /// <summary>
    /// Inheritate protected method from base class 
    /// </summary>
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

    /// <summary>
    /// private class which contains private member and method
    /// </summary>
    class PrivateClass
    {
        #region Private Member 
        private int _privateField;
        #endregion

        #region Private Method
        /// <summary>
        /// display the message
        /// </summary>
        private void PrivateMethod()
        {
            Console.WriteLine("Private Method !!");
        }
        #endregion
    }

    /// <summary>
    /// internal class which is called from anywhere within same assembly
    /// </summary>
    internal class InternalClass
    {
        #region Internal Member
        internal int internalField;
        #endregion

        #region Internal Method
        /// <summary>
        /// display the message
        /// </summary>
        internal void InternalMethod()
        {
            Console.WriteLine("Internal Method !!");
        }
        #endregion
    }

    /// <summary>
    /// class can contains protected internal member and method
    /// </summary>
    public class ProtectedInternal
    {
        #region Protected Internal Member
        protected internal int protectedInternalField;
        #endregion

        #region Protected Internal Method
        /// <summary>
        /// display the message
        /// </summary>
        protected internal void ProtectedInternalMethod()
        {
            Console.WriteLine("protected internal method");
        }
        #endregion
    }

    /// <summary>
    /// class which can derived from protected internal class 
    /// </summary>
    public class DerivedProtectedInternal : ProtectedInternal
    {
        /// <summary>
        /// Can access ProtectedInternalField and ProtectedInternalMethod here
        /// </summary>
        public void DerivedProtectedInternalMethod()
        {
            ProtectedInternalMethod();
        }
    }

    /// <summary>
    /// create the object of the all access modifiers.
    /// </summary>
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


            ProtectedInternal objProtectedInternal = new ProtectedInternal();
            objProtectedInternal.ProtectedInternalMethod();         // call the protected internal method


            DerivedProtectedInternal objDerivedProtectedInternal = new DerivedProtectedInternal();
            objDerivedProtectedInternal.DerivedProtectedInternalMethod();       // call the protected internal method
        }
    }

}
