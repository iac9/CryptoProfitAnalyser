﻿using CryptoProfitAnalyser.Domain;

namespace CryptoProfitAnalyser.Application
{
    public interface ICryptoProfitAnalyserService
    {
        Task<decimal> GetCoinRealisedProfit(DateRange dateRange, string coinSymbol);
        decimal GetTotalRealisedProfit(DateRange dateRange);
    }
}
