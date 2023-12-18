namespace InStock.Common.Models
{
    public class DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public double TotalDays => (End.Date - Start.Date).TotalDays;

        public DateRange(DateTime start, DateTime end)
        {
            if (end.Date < start.Date)
            {
                throw new ArgumentException("End date must some after start date");
            }

            Start = start;
            End = end;
        }
    }
}
