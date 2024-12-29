using CryptoProfitAnalyser.Domain;

namespace CryptoProfitAnalyser.Application.Interfaces;

public interface ITransactionService
{
    Task<OrderHistory> GetOrderHistory(DateRange dateRange, string? coinSymbol = null);
}