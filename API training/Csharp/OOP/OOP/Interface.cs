using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    //interface
    interface IShape
    {
        #region Private Method
        void Draw();
        int Area { get; }
        #endregion
    }

    class Square : IShape
    {
        #region Private Member
        int _length;
        #endregion

        #region Constructor
        public Square(int length)
        {
            this._length = length;
        }
        #endregion

        #region Public Method

        /// <summary>
        ///     Override the method from interface
        /// </summary>
        public void Draw()
        {
            Console.WriteLine("Drawing a Square");
        }

        /// <summary>
        ///     returns the area of sqare
        /// </summary>
        public int Area
        {
            get { return _length * _length; }
        }
        #endregion
    }

    interface IDrawable
    {
        void Draw2();
    }


    // multiple inheritance 
    class Circle2 : IShape, IDrawable
    {
        int _radius;
        public Circle2(int radius)
        {
            _radius = radius;
        }

        public int Area
        {
            get { return 3 * _radius * _radius; }
        }
        public void Draw()
        {
            Console.WriteLine($"Circle is drawing with radius {_radius} - implement IShape");
        }
        public void Draw2()
        {
            Console.WriteLine($"Circle is drawing with radius {_radius} - implement IDrawable");
        }
    }
}
