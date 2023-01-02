using CarSpot.Api.Services;

namespace CarSpot.Tests.Shared
{
    public class TestClock : IClock
    {
        public DateTime CurrentDate() => new DateTime(2023, 01, 03);
    }
}
