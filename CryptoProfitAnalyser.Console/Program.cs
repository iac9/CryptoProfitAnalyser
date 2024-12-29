// See https://aka.ms/new-console-template for more information
using CryptoProfitAnalyser.Application.Implementations;
using CryptoProfitAnalyser.Domain;
using CryptoProfitAnalyser.Infrastructure;
using CryptoProfitAnalyser.Infrastructure.Coinspot;
using Newtonsoft.Json;

var startDate = new DateTime(2000, 1, 1);
var endDate = new DateTime(2025, 01, 01);
var coinSymbol = "eth";

var dateRange = new DateRange(startDate, endDate);
var coinspotTransactionService = new CoinspotTransactionService();
var orderHistory = await coinspotTransactionService.GetOrderHistory(dateRange, coinSymbol);
var json = JsonConvert.SerializeObject(orderHistory, Formatting.Indented);
Console.WriteLine(json);

var analyserService = new CryptoProfitAnalyserService(coinspotTransactionService);
var realisedProfit = await analyserService.GetCoinRealisedProfit(dateRange, coinSymbol);
var totalRealisedProfit = await analyserService.GetTotalRealisedProfit(dateRange);

Console.WriteLine(realisedProfit);
Console.WriteLine(totalRealisedProfit);
