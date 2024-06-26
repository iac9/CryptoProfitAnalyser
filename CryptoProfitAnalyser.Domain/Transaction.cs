﻿namespace CryptoProfitAnalyser.Domain
{
    public class Transaction
    {
        public string CoinSymbol { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public DateTime DateOccurred { get; set; }
        public decimal Rate { get; set; }
        public decimal Fee { get; set; }
    }
}
