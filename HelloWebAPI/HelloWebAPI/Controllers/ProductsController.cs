using HelloWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloWebAPI.Controllers
{
    /// <summary>
    /// 产品资源
    /// </summary>
    public class ProductsController : ApiController
    {
        readonly Product[] _products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };
        /// <summary>
        /// 获取所有的产品
        /// </summary>
        /// <remarks>获取所有的产品产品Id</remarks>
        /// <returns></returns>
        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }
        
        /// <summary>
        /// 根据产品Id获取到产品
        /// </summary>
        /// <param name="id">产品Id</param>
        ///<remarks></remarks>
        /// <returns>根据产品Id获取到产品</returns>
        /// <exception cref="HttpResponseException"></exception>
        public Product GetProductById(int id)
        {
            var product = _products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }
        /// <summary>
        /// 根据产品类别获取到产品
        /// </summary>
        /// <param name="category">产品类别</param>
        /// <returns></returns>
        [HttpGet, Route("Product/{category}")]
        public List<Product> GetProductsByCategory(string category)
        {
            return _products.Where(
                (p) => string.Equals(p.Category, category,
                    StringComparison.OrdinalIgnoreCase)).ToList();
        }

    }
}
