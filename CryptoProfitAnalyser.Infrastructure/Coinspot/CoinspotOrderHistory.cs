namespace CryptoProfitAnalyser.Infrastructure
{
    public class CoinspotOrderHistory
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public IEnumerable<CoinspotTransaction> BuyOrders { get; set; } = Enumerable.Empty<CoinspotTransaction>();
        public IEnumerable<CoinspotTransaction> SellOrders { get; set; } = Enumerable.Empty<CoinspotTransaction>();

    }

    public class CoinspotTransaction
    {
        public string Coin { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public string Market { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
        public bool Otc { get; set; }
        public string SoldDate { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public decimal AudFeeExGst { get; set; }
        public decimal AudGst { get; set; }
        public decimal AudTotal { get; set; }
    }
}
