using CryptoProfitAnalyser.Domain;

namespace CryptoProfitAnalyser.Application
{
    public class CryptoProfitAnalyserService(ITransactionService transactionService) : ICryptoProfitAnalyserService
    {
        private readonly ITransactionService transactionService = transactionService;

        public async Task<decimal> GetCoinRealisedProfit(DateRange dateRange, string coinSymbol)
        {
            var orderHistory = await transactionService.GetOrderHistory(dateRange, coinSymbol);
            var buyTransactions = orderHistory.BuyOrders.OrderByDescending(o => o.DateOccurred).ToList();
            var sellTransactions = orderHistory.SellOrders.OrderByDescending(o => o.DateOccurred).ToList();
            var netProfit = 0m;

            foreach (var sellTransaction in sellTransactions)
            {
                do
                {
                    var lastBuyTransaction = buyTransactions.Last();

                    if (sellTransaction.Coin.Quantity <= lastBuyTransaction.Coin.Quantity)
                    {
                        buyTransactions[^1].Coin.Quantity -= sellTransaction.Coin.Quantity;
                        sellTransaction.Coin.Quantity = 0;
                    }
                    else
                    {
                        buyTransactions.RemoveAt(buyTransactions.Count - 1);
                        sellTransaction.Coin.Quantity -= lastBuyTransaction.Coin.Quantity;
                    }

                    netProfit += (sellTransaction.Rate - lastBuyTransaction.Rate) * sellTransaction.Coin.Quantity;

                } while (sellTransaction.Coin.Quantity > buyTransactions.Last().Coin.Quantity);
            }

            return netProfit;
        }

        public decimal GetTotalRealisedProfit(DateRange dateRange)
        {
            throw new NotImplementedException();
        }
    }
}
