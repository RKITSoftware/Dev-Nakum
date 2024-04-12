using Authentication_Authorization.Models;
using System.Collections.Generic;
using System.Linq;

namespace Authentication_Authorization.BL 
{
    /// <summary>
    /// class to manage the products data
    /// </summary>
    public class BLProducts  
    {
        #region Private Members

        /// <summary>
        /// store the list of product's
        /// </summary>
        private static List<Products> _lstProducts = new List<Products>();

        /// <summary>
        /// Counter for generating unique product IDs
        /// </summary>
        private static int _id = 1; 

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves a list of all products.
        /// </summary>
        /// <returns>A list of all `Products` objects.</returns>
        public List<Products> GetAllProducts()
        {
            return _lstProducts; 
        }

        /// <summary>
        /// Adds a new product to the internal list.
        /// </summary>
        /// <param name="objProduct">The `Products` object representing the new product.</param>
        public void AddProduct(Products objProduct)
        {
            objProduct.Id = _id++; 
            _lstProducts.Add(objProduct);
        }

        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        /// <param name="id">Product's Id</param>
        /// <returns>The `Products` object matching the ID, or null if not found.</returns>
        public Products GetProductById(int id)
        {
            return _lstProducts.FirstOrDefault(p => p.Id == id);  
        }

        /// <summary>
        /// Removes a product from the Product's ID.
        /// </summary>
        /// <param name="id">Product's Id</param>
        /// <returns>True if the product is removed successfully, false otherwise.</returns>
        public bool RemoveProduct(int id)
        {
            Products productToRemove = GetProductById(id);
            if (productToRemove != null)
            {
                _lstProducts.Remove(productToRemove);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates an existing product's properties.
        /// </summary>
        /// <param name="id">Product's Id</param>
        /// <param name="objProducts">object of the products</param>
        /// <returns>True if the product is updated successfully, false otherwise.</returns>
        public bool UpdateProduct(int id, Products objProducts)
        {
            Products productToUpdate = GetProductById(id);
            if (productToUpdate != null)
            {
                productToUpdate.Price = objProducts.Price;
                productToUpdate.Description = objProducts.Description;
                productToUpdate.Name = objProducts.Name;
                return true;
            }
            return false;
        }

        #endregion
    }
}
