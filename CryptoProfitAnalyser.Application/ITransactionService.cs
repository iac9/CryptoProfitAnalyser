using CryptoProfitAnalyser.Domain;

namespace CryptoProfitAnalyser.Application
{
    public interface ITransactionService
    {
        Task<OrderHistory> GetOrderHistory(DateRange dateRange, string coinSymbol);
    }
}
    