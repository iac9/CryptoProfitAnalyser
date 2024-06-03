using Newtonsoft.Json.Linq;

namespace CryptoProfitAnalyser.Infrastructure
{
    public class CoinGeckoService
    {
        private const string baseUrl = "https://api.coingecko.com/api/v3";

        public async Task<Decimal> GetHistoricalCoinPrice(string coin, DateTime dateTime)
        {
            var requestUri = new Uri(baseUrl + $"/coins/{coin}/history?date={dateTime:dd-MM-yyyy}&localization=false");

            var httpClient = new HttpClient
            {
                BaseAddress = requestUri
            };
            var response = await httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject jsonResponse = JObject.Parse(responseBody);

            Console.WriteLine(jsonResponse.ToString());

            var marketData = jsonResponse["market_data"];
            var currentPrice = marketData["current_price"];
            return currentPrice["aud"].Value<decimal>();
        }
    }
}
