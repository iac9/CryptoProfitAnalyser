using CryptoProfitAnalyser.Domain;
using System.Diagnostics;

namespace CryptoProfitAnalyser.Application
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetCoinTransactions(DateRange dateRange, string coinSymbol);
        IEnumerable<string> GetAllTransactedCoinSymbols(DateRange dateRage);
    }
}
