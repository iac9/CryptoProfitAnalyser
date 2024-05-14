namespace CryptoProfitAnalyser.Domain
{
    public class Transaction
    {
        public Coin Coin { get; set; }
        public DateTime DateOccurred { get; set; }
        public decimal Rate { get; set; }
        public decimal Fee { get; set; }
    }
}
