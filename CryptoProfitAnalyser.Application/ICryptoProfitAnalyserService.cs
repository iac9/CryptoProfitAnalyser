using CryptoProfitAnalyser.Domain;

namespace CryptoProfitAnalyser.Application
{
    public interface ICryptoProfitAnalyserService
    {
        decimal GetCoinUnrealisedProfit(DateRange dateRange, string coinSymbol);
        decimal GetCoinRealisedProfit(DateRange dateRange, string coinSymbol);
        decimal GetTotalUnrealisedProfit(DateRange dateRange);
        decimal GetTotalRealisedProfit(DateRange dateRange);
    }
}
