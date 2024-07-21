using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Greggs.Products.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataAccess<Product> _productAccess;
        private readonly ICurrencyService _currencyService;

        public ProductService(IDataAccess<Product> productAccess, ICurrencyService currencyService)
        {
            _productAccess = productAccess;
            _currencyService = currencyService;
        }
        public IEnumerable<Product> GetProducts(int? pageStart, int? pageSize)
        {
            return _productAccess.List(pageStart, pageSize);
        }

        public IEnumerable<Product> GetProductsInEuros(int? pageStart, int? pageSize) {
            var productList = _productAccess.List(pageStart, pageSize);

            var productsWithEuros = productList.Select(product => new Product
            {
                Name = product.Name,
                Price = _currencyService.Convert(product.Price, product.Currency, Currency.EUR),
                Currency = Currency.EUR,
            });

            return productsWithEuros;
        }
    }
}
