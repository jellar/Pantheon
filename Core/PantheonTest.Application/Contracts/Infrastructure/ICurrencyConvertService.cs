using System.Threading.Tasks;

namespace PantheonTest.Application.Contracts.Infrastructure
{
    public interface ICurrencyConvertService
    {
        Task<decimal> GetCurrencyConvert(string currencySymbol);
    }
}