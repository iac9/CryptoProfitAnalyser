using CryptoProfitAnalyser.Application;
using CryptoProfitAnalyser.Domain;

namespace CryptoProfitAnalyser.Infrastructure.Coinspot
{
    public static class CoinspotMapper
    {
        public static IEnumerable<Transaction> ToTransactions(this IEnumerable<CoinspotTransaction> coinspotTransactions)
        {
            foreach (var coinspotTransaction in coinspotTransactions)
            {
                yield return new Transaction
                {
                    CoinSymbol = coinspotTransaction.Coin,
                    Quantity = coinspotTransaction.Amount,
                    DateOccurred = Utilities.ParseUtcDateTimeString(coinspotTransaction.SoldDate),
                    Rate = coinspotTransaction.Rate,
                    Fee = coinspotTransaction.AudTotal * 0.01M,
                };
            }
        }
    }
}
