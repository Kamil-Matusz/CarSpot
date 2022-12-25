namespace CarSpot.Api.ValueObject
{
    public sealed record Week
    {
        public Date From_Date { get; }
        public Date To_Date { get; }

        public Week(DateTimeOffset value)
        {
            var pastDays = value.DayOfWeek is DayOfWeek.Sunday ? 7 : (int)value.DayOfWeek;
            var remainingDays = 7 - pastDays;
            From_Date = new Date(value.AddDays(-1 * pastDays));
            To_Date = new Date(value.AddDays(remainingDays));
        }

        public override string ToString() => $"{From_Date} -> {To_Date}";
    }
}
