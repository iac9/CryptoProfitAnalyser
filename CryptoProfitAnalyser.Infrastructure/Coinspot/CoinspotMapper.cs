using CryptoProfitAnalyser.Application;
using CryptoProfitAnalyser.Domain;

namespace CryptoProfitAnalyser.Infrastructure.Coinspot
{
    public static class CoinspotMapper
    {
        public static IEnumerable<Transaction> ToTransactions(this IEnumerable<CoinspotTransaction> csTxns) =>
            csTxns.Select(t => new Transaction
            {
                CoinSymbol = t.Coin,
                Quantity = t.Amount,
                DateOccurredUtc = Utilities.ParseUtcDateTimeString(t.SoldDate),
                Rate = t.Market.Contains("AUD") ? t.Rate : t.AudTotal / t.Amount,
                Fee = t.AudFeeExGst + t.AudGst,
                TotalAud = t.AudTotal
            });
    }
}
