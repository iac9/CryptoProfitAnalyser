namespace CryptoProfitAnalyser.Domain
{
    public class DateRange
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException($"Start date \'{startDate}\' cannot occur after end date \'{endDate}\'.");
            }

            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
