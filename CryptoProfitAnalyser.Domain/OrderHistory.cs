namespace CryptoProfitAnalyser.Domain
{
    public class OrderHistory
    {
        public IEnumerable<Transaction> BuyOrders { get; set; } = Enumerable.Empty<Transaction>();
        public IEnumerable<Transaction> SellOrders { get; set; } = Enumerable.Empty<Transaction>();
    }
}
