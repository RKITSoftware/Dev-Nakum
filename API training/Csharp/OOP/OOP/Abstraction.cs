using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    // Abstraction
    abstract class Shape
    {
        #region Public Method
        /// <summary>
        ///     abstract method draw
        /// </summary>
        public abstract void Draw();

        public void Welcome()
        {
            Console.WriteLine("This is a welcome message from abstract class");
        }
        #endregion
    }

    class Circle : Shape
    {
        #region Public Method 

        /// <summary>
        ///     override abstract method
        /// </summary>
        public override void Draw()
        {
            Console.WriteLine("Circle is draw");
        }
        #endregion  
    }
}
