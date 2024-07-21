using Greggs.Products.Api.Models;
using System.Collections.Generic;

namespace Greggs.Products.Api.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(int? pageStart, int? pageSize);
    }
}
