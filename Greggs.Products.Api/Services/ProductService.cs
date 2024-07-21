using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using System.Collections.Generic;

namespace Greggs.Products.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataAccess<Product> _productAccess;

        public ProductService(IDataAccess<Product> productAccess)
        { 
            _productAccess = productAccess;
        }
        public IEnumerable<Product> GetProducts(int? pageStart, int? pageSize)
        {
            return _productAccess.List(pageStart, pageSize);
        }
    }
}
