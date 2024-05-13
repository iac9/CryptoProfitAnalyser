using CryptoProfitAnalyser.Domain;
using System.Diagnostics;

namespace CryptoProfitAnalyser.Application
{
    public interface ITransactionService
    {
        Task<OrderHistory> GetOrderHistory(DateRange dateRange, string coinSymbol);
    }
}
