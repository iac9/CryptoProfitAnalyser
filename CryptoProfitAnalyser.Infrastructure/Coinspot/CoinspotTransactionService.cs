using CryptoProfitAnalyser.Application;
using CryptoProfitAnalyser.Domain;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace CryptoProfitAnalyser.Infrastructure
{
    public class CoinspotTransactionService : ITransactionService
    {
        private readonly string apiKey;
        private readonly string apiSecret;
        private const string baseUrl = "https://www.coinspot.com.au/api/v2";

        public CoinspotTransactionService()
        {
            var apiKey = Environment.GetEnvironmentVariable("COINSPOT_API_KEY");
            var apiSecret = Environment.GetEnvironmentVariable("COINSPOT_API_SECRET");

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));

            if (apiSecret == null)
                throw new ArgumentNullException(nameof(apiSecret));

            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }

        public async Task<OrderHistory> GetOrderHistory(DateRange dateRange, string coinType)
        {
            const string dateTimeFormat = "yyyy-MM-dd";
            var startDateUtc = dateRange.StartDate.ToUniversalTime().ToString(dateTimeFormat);
            var endDateUtc = dateRange.EndDate.ToUniversalTime().ToString(dateTimeFormat);

            var postData = new Dictionary<string, object>
            {
                { "cointype", coinType },
                { "startdate  ", startDateUtc },
                { "enddate  ", endDateUtc },
                { "markettype ", "AUD" },
                { "limit ", "500" }
            };

            var jsonReponse = await APIQuery("/ro/my/orders/completed", postData);
            var coinspotOrderHistory = JsonConvert.DeserializeObject<CoinspotOrderHistory>(jsonReponse);

            return coinspotOrderHistory == null
                ? throw new FormatException()
                : new OrderHistory
                {
                    BuyOrders = ConvertToTransactions(coinspotOrderHistory.BuyOrders),
                    SellOrders = ConvertToTransactions(coinspotOrderHistory.SellOrders)
                };
        }

        public async Task<string> APIQuery(string endpoint, IDictionary<string, object> postData)
        {
            var requestUri = new Uri(baseUrl + endpoint);

            var nonce = DateTime.Now.Ticks.ToString();
            postData.Add("nonce", nonce);

            var postDataJson = JsonConvert.SerializeObject(postData);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent(postDataJson, Encoding.UTF8, "application/json")
            };

            var signature = HexHash(postDataJson, apiSecret);

            requestMessage.Content.Headers.Add("key", apiKey);
            requestMessage.Content.Headers.Add("sign", signature);

            var httpClient = new HttpClient
            {
                BaseAddress = requestUri
            };
            var response = await httpClient.PostAsync(requestUri, requestMessage.Content);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        static string HexHash(string message, string key)
        {
            var keyByte = new ASCIIEncoding().GetBytes(key);
            var messageBytes = new ASCIIEncoding().GetBytes(message);
            var hashmessage = new HMACSHA512(keyByte).ComputeHash(messageBytes);

            // to lowercase hexits
            return string.Concat(Array.ConvertAll(hashmessage, x => x.ToString("x2")));
        }

        private static IEnumerable<Transaction> ConvertToTransactions(IEnumerable<CoinspotTransaction> coinspotTransactions)
        {
            foreach (var coinspotTransaction in coinspotTransactions)
            {
                yield return new Transaction
                {
                    CoinSymbol = coinspotTransaction.Coin,
                    DateOccurred = Utilities.ParseUtcDateTimeString(coinspotTransaction.SoldDate),
                    Quantity = coinspotTransaction.Amount,
                    Rate = coinspotTransaction.Rate,
                    Fee = coinspotTransaction.AudTotal * 0.01M,
                };
            }
        }
    }
}
