using System.Security.Cryptography;
using System.Text;
using CryptoProfitAnalyser.Application.Interfaces;
using CryptoProfitAnalyser.Domain;
using Newtonsoft.Json;

namespace CryptoProfitAnalyser.Infrastructure.Coinspot
{
    public class CoinspotTransactionService : ITransactionService
    {
        private readonly string _apiKey;
        private readonly string _apiSecret;
        private const string BaseUrl = "https://www.coinspot.com.au/api/v2/ro";

        public CoinspotTransactionService()
        {
            var apiKey = Environment.GetEnvironmentVariable("COINSPOT_API_KEY");
            var apiSecret = Environment.GetEnvironmentVariable("COINSPOT_API_SECRET");

            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _apiSecret = apiSecret ?? throw new ArgumentNullException(nameof(apiSecret));
        }

        public async Task<OrderHistory> GetOrderHistory(DateRange dateRange, string? coinType = null)
        {
            const string endpoint = "/my/orders/completed";
            const string dateTimeFormat = "yyyy-MM-dd";
            var startDateUtc = dateRange.StartDate.ToUniversalTime().ToString(dateTimeFormat);
            var endDateUtc = dateRange.EndDate.ToUniversalTime().ToString(dateTimeFormat);

            var postData = new Dictionary<string, string>
            {
                { "markettype", "AUD" },
                { "startdate", startDateUtc },
                { "enddate", endDateUtc },
            };

            if (coinType != null)
                postData.Add("cointype", coinType);
            
            var jsonResponse = await ApiQuery(endpoint, postData);
            var csOrderHistory = JsonConvert.DeserializeObject<CoinspotOrderHistory>(jsonResponse) ??
                                 throw new FormatException();

            return new OrderHistory
            {
                BuyOrders = csOrderHistory.BuyOrders.ToTransactions(),
                SellOrders = csOrderHistory.SellOrders.ToTransactions(),
            };
        }

        #region API Query

        private async Task<string> ApiQuery(string endpoint, Dictionary<string, string> postData)
        {
            var requestUri = new Uri(BaseUrl + endpoint);

            var nonce = DateTime.Now.Ticks.ToString();
            postData.Add("nonce", nonce);

            var postDataJson = JsonConvert.SerializeObject(postData);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent(postDataJson, Encoding.UTF8, "application/json")
            };

            var signature = HexHash(postDataJson, _apiSecret);

            requestMessage.Content.Headers.Add("key", _apiKey);
            requestMessage.Content.Headers.Add("sign", signature);

            var httpClient = new HttpClient
            {
                BaseAddress = requestUri
            };
            var response = await httpClient.PostAsync(requestUri, requestMessage.Content);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        private static string HexHash(string message, string key)
        {
            var keyByte = new ASCIIEncoding().GetBytes(key);
            var messageBytes = new ASCIIEncoding().GetBytes(message);
            var hashMessage = new HMACSHA512(keyByte).ComputeHash(messageBytes);

            // to lowercase hexits
            return string.Concat(Array.ConvertAll(hashMessage, x => x.ToString("x2")));
        }

        #endregion
    }
}