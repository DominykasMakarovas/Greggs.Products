using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using System;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class CurrencyServiceTests
    {

        private readonly CurrencyService _currencyService;

        public CurrencyServiceTests() {
            _currencyService = new CurrencyService();
        }

        [Theory]
        [InlineData(1, 1.11)]
        [InlineData(100, 111)]
        [InlineData(0, 0.00)]
        [InlineData(0.5, 0.56)]
        public void Convert_GBPToEUR_ReturnsCorrectValue(decimal gbpValue, decimal expectedValue) {
            var result = _currencyService.Convert(gbpValue, Currency.GBP, Currency.EUR);

            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Convert_EURToGBP_ThrowsNewArgumentException() {
            var actual = Assert.Throws<ArgumentException>(() => _currencyService.Convert(10m, Currency.EUR, Currency.GBP));

            Assert.Equal("Unsupported currency provided.", actual.Message);
        }

        [Fact]
        public void Convert_GBPToInvalidCurrency_ThrowsNewArgumentException() {
            var actual = Assert.Throws<ArgumentException>(() => _currencyService.Convert(10m, Currency.GBP, (Currency) 123));

            Assert.Equal("Unsupported currency provided.", actual.Message);
        }
    }
}
