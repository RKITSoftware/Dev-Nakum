using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    // Encapsulation
    class Customer
    {
        #region Private Member

        string _firstName, _lastName;

        #endregion

        #region Constructor
        public Customer()
        {
            Console.WriteLine("No name is assigned");
        }
        public Customer(string firstName, string lastName)
        {
            this._firstName = firstName;
            this._lastName = lastName;
        }
        #endregion

        #region Public Methods
        public string FirstName
        {
            get { return _firstName; }
            set { this._firstName = value; }
        }

        /// <summary>
        ///     Print the full name of customer
        /// </summary>
        public void PrintFullName()
        {
            Console.WriteLine("Full name is " + this._firstName + " " + this._lastName);
        }
        #endregion
    }
}
