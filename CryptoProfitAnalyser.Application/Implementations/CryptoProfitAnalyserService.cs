using CryptoProfitAnalyser.Application.Interfaces;
using CryptoProfitAnalyser.Domain;

namespace CryptoProfitAnalyser.Application.Implementations;

public class CryptoProfitAnalyserService(ITransactionService transactionService) : ICryptoProfitAnalyserService
{
    public async Task<decimal> GetCoinRealisedProfit(DateRange dateRange, string coinSymbol)
    {
        var orderHistory = await transactionService.GetOrderHistory(dateRange, coinSymbol);
        
        var buyTransactions = orderHistory.BuyOrders.ToList();
        var totalBuyAud = buyTransactions.Sum(t => t.TotalAud);
        var totalBuyQty = buyTransactions.Sum(t => t.Quantity);
        
        var sellTransactions = orderHistory.SellOrders.ToList();
        var totalSellAud = sellTransactions.Sum(t => t.TotalAud);
        var totalSellQty = sellTransactions.Sum(t => t.Quantity);
        
        return totalSellAud - totalBuyAud * (totalSellQty / totalBuyQty);
    }

    public async Task<decimal> GetTotalRealisedProfit(DateRange dateRange)
    {
        var orderHistory = await transactionService.GetOrderHistory(dateRange);

        var buyTransactions = orderHistory.BuyOrders.ToLookup(o => o.CoinSymbol);
        var sellTransactions = orderHistory.SellOrders.ToLookup(o => o.CoinSymbol);

        var coins = buyTransactions.Select(t => t.Key);

        var netProfit = 0M;
        foreach (var coin in coins)
        {
            var totalBuyAud = buyTransactions[coin].Sum(t => t.TotalAud);
            var totalBuyQty = buyTransactions[coin].Sum(t => t.Quantity);
            
            var totalSellAud = sellTransactions[coin].Sum(t => t.TotalAud);
            var totalSellQty = sellTransactions[coin].Sum(t => t.Quantity);
            
            netProfit += totalSellAud - totalBuyAud * (totalSellQty / totalBuyQty);
        }
        
        return netProfit;
    }
}