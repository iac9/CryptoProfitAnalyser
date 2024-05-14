namespace CryptoProfitAnalyser.Application
{
    public static class Utilities
    {
        public static DateTime ParseUtcDateTimeString(string utcDateTime)
        {
            var parsedSuccessfully = DateTime.TryParse(utcDateTime, out DateTime dateTime);

            return parsedSuccessfully ? dateTime : throw new FormatException(utcDateTime);
        }
    }
}
