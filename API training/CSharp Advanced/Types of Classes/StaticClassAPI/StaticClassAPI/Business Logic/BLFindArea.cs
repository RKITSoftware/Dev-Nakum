using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaticClassAPI.Business_Logic
{
    /// <summary>
    /// class which can find the area of shape 
    /// </summary>
    public static class BLFindArea
    {
        /// <summary>
        /// find the area of square
        /// </summary>
        /// <param name="length">length of square</param>
        /// <returns>area of square</returns>
        public static int AreaOfSquare(int length)
        {
            return length * length;
        }

        /// <summary>
        /// find the area of rectangle
        /// </summary>
        /// <param name="length">length of rectangle</param>
        /// <param name="width">width of rectangle</param>
        /// <returns> area of rectangle </returns>
        public static int AreaOfRectangle(int length,int width)
        {
            return length * width;
        }
    }
}