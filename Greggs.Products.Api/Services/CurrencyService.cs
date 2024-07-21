using Greggs.Products.Api.Models;
using System;

namespace Greggs.Products.Api.Services
{
    public class CurrencyService : ICurrencyService
    {
        private const decimal GbpToEurosRate = 1.11m;

        public decimal Convert(decimal value, Currency currentCurrency, Currency currencyToConvert) 
        {
            return currentCurrency switch
            {
                Currency.GBP => currencyToConvert switch
                {
                    Currency.EUR => Math.Round(value * GbpToEurosRate, 2, MidpointRounding.AwayFromZero),
                    _ => throw new ArgumentException("Unsupported currency provided.")
                },
                _ => throw new ArgumentException("Unsupported currency provided.")
            };
        }
    }
}
