//using CryptoProfitAnalyser.Domain;

//namespace CryptoProfitAnalyser.Application
//{
//    public class CryptoProfitAnalyserService : ICryptoProfitAnalyserService
//    {
//        private readonly ITransactionService transactionService;

//        public CryptoProfitAnalyserService(ITransactionService transactionService)
//        {
//            this.transactionService = transactionService;
//        }

//        public decimal GetCoinRealisedProfit(DateRange dateRange, string coinSymbol)
//        {
//            var transactions = transactionService.GetOrderHistory(dateRange, coinSymbol);
//            var buyTransactions = transactions.Where(t => t.TransactionType == TransactionType.Buy).ToList();
//            var sellTransactions = transactions.Where(t => t.TransactionType == TransactionType.Sell).ToList();
//            var netProfit = 0m;

//            foreach ( var sellTransaction in sellTransactions )
//            {
//                do 
//                {
//                    var lastBuyTransaction = buyTransactions.Last();

//                    if (sellTransaction.Quantity <= lastBuyTransaction.Quantity)
//                    {
//                        buyTransactions[^1].Quantity -= sellTransaction.Quantity;
//                        sellTransaction.Quantity = 0;
//                    }
//                    else
//                    {
//                        buyTransactions.RemoveAt(buyTransactions.Count - 1);
//                        sellTransaction.Quantity -= lastBuyTransaction.Quantity;
//                    }

//                    netProfit += (sellTransaction.Rate - lastBuyTransaction.Rate) * sellTransaction.Quantity;

//                } while (sellTransaction.Quantity > buyTransactions.Last().Quantity);
//            }

//            return netProfit;
//        }

//        public decimal GetTotalRealisedProfit(DateRange dateRange)
//        {
//            return transactionService.GetAllTransactedCoinSymbols(dateRange).Sum(c => GetCoinRealisedProfit(dateRange, c));
//        }
//    }
//}
