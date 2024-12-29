namespace CryptoProfitAnalyser.Domain
{
    public class OrderHistory
    {
        public IEnumerable<Transaction> BuyOrders { get; init; } = [];
        public IEnumerable<Transaction> SellOrders { get; init; } = [];
    }
}
