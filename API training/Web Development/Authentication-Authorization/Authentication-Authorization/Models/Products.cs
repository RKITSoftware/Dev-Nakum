
namespace Authentication_Authorization.Models
{
    /// <summary>
    /// schema of products
    /// </summary>
    public class Products
    {
        /// <summary>
        /// Product's Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product's Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product's Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Product's Price
        /// </summary>
        public int Price { get; set; }
    }
}