using CryptoProfitAnalyser.Domain;

namespace CryptoProfitAnalyser.Application.Interfaces;

public interface ICryptoProfitAnalyserService
{
    Task<decimal> GetCoinRealisedProfit(DateRange dateRange, string coinSymbol);
    Task<decimal> GetTotalRealisedProfit(DateRange dateRange);
}