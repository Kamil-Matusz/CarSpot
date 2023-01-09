namespace CarSpot.Api.Services
{
    public class Clock : IClock
    {
        public DateTime CurrentDate() => DateTime.UtcNow;
    }
}
