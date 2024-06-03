// See https://aka.ms/new-console-template for more information
using CryptoProfitAnalyser.Application;
using CryptoProfitAnalyser.Domain;
using CryptoProfitAnalyser.Infrastructure;
using Newtonsoft.Json;

var startDate = new DateTime(2023, 7, 1);
var endDate = new DateTime(2024, 6, 30);
var coinSymbol = "RUNE";

var dateRange = new DateRange(startDate, endDate);
var coinspotTransactionService = new CoinspotTransactionService();
var orderHistory = await coinspotTransactionService.GetOrderHistory(dateRange, coinSymbol);
var json = JsonConvert.SerializeObject(orderHistory, Formatting.Indented);
Console.WriteLine(json);

var analyserService = new CryptoProfitAnalyserService(coinspotTransactionService);
var realisedProfit = await analyserService.GetCoinRealisedProfit(dateRange, coinSymbol);
Console.WriteLine(realisedProfit);

var coinGeckoService = new CoinGeckoService();
var res = await coinGeckoService.GetHistoricalCoinPrice("bitcoin", new DateTime(2024, 01, 01));
Console.WriteLine(res);

