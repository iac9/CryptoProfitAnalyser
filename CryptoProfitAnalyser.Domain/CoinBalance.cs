namespace CryptoProfitAnalyser.Domain
{
    public class CoinBalance
    {
        public string CoinSymbol { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
    }
}
