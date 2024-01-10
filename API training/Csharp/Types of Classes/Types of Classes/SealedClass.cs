using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types_of_Classes
{
    public sealed class SealedClass
    {
        public string Name { get; set; }
    }

    // can not derived or subclass 
    //public class DerivedSealedClass : SealedClass
    //{

    //}
}
