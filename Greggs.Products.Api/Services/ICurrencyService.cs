using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Services
{
    public interface ICurrencyService
    {
        public decimal Convert(decimal value, Currency currentCurrency, Currency currencyToConvert);
    }
}
