// See https://aka.ms/new-console-template for more information
using CryptoProfitAnalyser.Domain;
using CryptoProfitAnalyser.Infrastructure;
using Newtonsoft.Json;

var startDate = new DateTime(2023, 7, 1);
var endDate = new DateTime(2024, 6, 30);

var dateRange = new DateRange(startDate, endDate);
var coinspotTransactionService = new CoinspotTransactionService();
var orderHistory = await coinspotTransactionService.GetOrderHistory(dateRange, "BTC");
var json = JsonConvert.SerializeObject(orderHistory, Formatting.Indented);

Console.WriteLine(json);
