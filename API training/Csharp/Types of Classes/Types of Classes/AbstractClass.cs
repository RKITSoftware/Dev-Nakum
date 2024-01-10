using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types_of_Classes
{
    abstract class AbstractClass
    {
        /// <summary>
        ///     display name method - abstract 
        /// </summary>
        public abstract void DisplayName();
    }

    class DerivedClass : AbstractClass
    {
        /// <summary>
        ///     Implement displayName Method from abstract class
        /// </summary>
        public override void DisplayName()
        {
            Console.WriteLine("Hello Dev from Abstract class ");
        }
    }

}
