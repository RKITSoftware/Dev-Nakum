using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    // single level inheritance
    class VIPCoustomer : Customer
    {
        #region Public Member

        public bool isVIP;

        #endregion

        #region Constructor
        public VIPCoustomer(string firstName, string lastName) : base(firstName, lastName)
        {
            this.isVIP = true;
        }
        #endregion

        #region Public Methods

        /// <summary>
        ///     Print the full name 
        ///     if customer is VIP - print the message 
        /// </summary>
        public void PrintFullName()
        {
            base.PrintFullName();
            if (isVIP)
            {
                Console.WriteLine("Thank you for using our VIP service !!");
            }
        }
        #endregion
    }

    // multi level inheritance
    class ExtraVIPCustomer : VIPCoustomer
    {
        public bool isExtraVIP;
        public ExtraVIPCustomer(string firstName, string lastName) : base(firstName, lastName)
        {
            isExtraVIP = true;
        }

        public new void PrintFullName()
        {
            base.PrintFullName();
            if (isExtraVIP)
            {
                Console.WriteLine("Thank you for using our Extra VIP service !!");
            }

        }
    }


    // hybrid Inheritance 
    class A
    {
        public void MethodA()
        {
            Console.WriteLine("inside into method A");
        }
    }
    class B:A
    {
        public void MethodB()
        {
            MethodA();
            Console.WriteLine("inside into method B");
        }
    }
    class C:A
    {
        public void MethodC()
        {
            MethodA();
            Console.WriteLine("inside into method C");
        }
    }
    class D:C
    {
        public void MethodD()
        {
            MethodC();
            Console.WriteLine("inside into method D");
        }
    }
}
