using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Greggs.Products.UnitTests;

public class ProductServiceTests
{
    private readonly Mock<IDataAccess<Product>> _mockProductAccess;
    private readonly IProductService _productService;
    private readonly Mock<ICurrencyService> _mockCurrencyService;

    private static readonly IEnumerable<Product> MockProducts = new List<Product>()
    {
        new() { Name = "Sausage Roll", Price = 1m },
        new() { Name = "Vegan Sausage Roll", Price = 1.1m },
        new() { Name = "Steak Bake", Price = 1.2m },
        new() { Name = "Yum Yum", Price = 0.7m },
        new() { Name = "Pink Jammie", Price = 0.5m },
        new() { Name = "Mexican Baguette", Price = 2.1m },
        new() { Name = "Bacon Sandwich", Price = 1.95m },
        new() { Name = "Coca Cola", Price = 1.2m }
    };

    public ProductServiceTests() {
        _mockProductAccess = new Mock<IDataAccess<Product>>();
        _mockCurrencyService = new Mock<ICurrencyService>();
        _productService = new ProductService(_mockProductAccess.Object, _mockCurrencyService.Object);
    }

    private void SetUpProducts(int? pageStart, int? pageSize, IEnumerable<Product> products) {
        _mockProductAccess.Setup(x => x.List(pageStart, pageSize)).Returns(products);
    }
    
    [Fact]
    public void GetProduct_WithNoPagaingParameters_ReturnsAllProductItems()
    {
        SetUpProducts(null, null, MockProducts);

        var actual = _productService.GetProducts(null, null);

        AssertProductsMatchExpected(MockProducts, actual);
    }

    [Fact]
    public void GetProduct_WithPagingParameters_ReturnsPagedProductItems() {
        var reducedMockProducts = MockProducts.Skip(2).Take(2);
        SetUpProducts(2, 4, reducedMockProducts);

        var actual = _productService.GetProducts(2, 4);

        AssertProductsMatchExpected(reducedMockProducts, actual);
        Assert.Equal(reducedMockProducts.Count(), actual.Count());
    }

    [Fact]
    public void GetProductsInEuros_ReturnsProductsInEurosConversion() {
        SetUpProducts(null, null, MockProducts);

        foreach (var product in MockProducts) {
            _mockCurrencyService.Setup(service => service.Convert(product.Price, product.Currency, Currency.EUR)).Returns(Math.Round(product.Price * 1.11m, 2, MidpointRounding.AwayFromZero));
        }

        var actual = _productService.GetProductsInEuros(null, null);

        var expected = MockProducts.Select(product => new Product
        {
            Name = product.Name,
            Price = Math.Round(product.Price * 1.11m, 2, MidpointRounding.AwayFromZero),
            Currency = Currency.EUR,
        });

        AssertProductsMatchExpected(expected, actual);
        Assert.Equal(MockProducts.Count(), actual.Count());
    }

    private static void AssertProductsMatchExpected(IEnumerable<Product> expected, IEnumerable<Product> actual)
    {
        Assert.NotNull(actual);

        foreach (var productInDatabase in expected)
        {
            Assert.Contains(actual, product => product.Name == productInDatabase.Name && product.Price == productInDatabase.Price);
        }
    }
}