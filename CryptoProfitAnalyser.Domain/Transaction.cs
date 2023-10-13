using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProfitAnalyser.Domain
{
    public class Transaction
    {
        public string CoinSymbol { get; set; }
        public DateTime DateOccurred { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Fee { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
