namespace CryptoProfitAnalyser.Domain
{
    public class Transaction
    {
        public required string CoinSymbol { get; set; }
        public required DateTime DateOccurredUtc { get; set; }
        public required decimal Quantity { get; set; }
        public required decimal Rate { get; set; }
        public required decimal Fee { get; set; }
        public required decimal TotalAud { get; set; }
    }
}
