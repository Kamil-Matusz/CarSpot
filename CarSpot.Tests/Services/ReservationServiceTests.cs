using CarSpot.Api.Commands;
using CarSpot.Api.Entities;
using CarSpot.Api.Services;
using CarSpot.Core.Repositories;
using CarSpot.Tests.Shared;
using Shouldly;
using Xunit;

namespace CarSpot.Tests.Services
{
    public class ReservationServiceTests
    {
        /*private readonly IClock _clock = new TestClock();
        private readonly ReservationsService _reservationsService;
        private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;
        public ReservationServiceTests()
        {
            _weeklyParkingSpots = new List<WeeklyParkingSpot>()
            {
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), _clock.CurrentDate(), _clock.CurrentDate().AddDays(7), "P1"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), _clock.CurrentDate(), _clock.CurrentDate().AddDays(7), "P2"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), _clock.CurrentDate(), _clock.CurrentDate().AddDays(7), "P3"),
            };
            _reservationsService= new ReservationsService(_clock,_weeklyParkingSpots);
        }

        [Fact]
        public async Task given_reservation_for_not_taken_date_create_reservation_should_success()
        {
            // Arrange
            var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000001"),Guid.NewGuid(),"John Doe","XYZ123",DateTime.UtcNow.AddDays(1));

            // Assert
            var reservationId = await _reservationsService.Create(command);

            // Act
            reservationId.ShouldNotBeNull();
            reservationId.Value.ShouldBe(command.ReservationId);
        }*/

        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
        private readonly IReservationsService _reservationsService;
      
        public ReservationServiceTests()
        {
           _clock = new TestClock();
        }

        [Fact]
        public async Task given_reservation_for_not_taken_date_create_reservation_should_success()
        {
            // Arrange
            var weeklyParkingSpot = (await _weeklyParkingSpotRepository.GetAllWeeklyAsync()).First();
            var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000001"), Guid.NewGuid(), "John Doe", "XYZ123", DateTime.UtcNow.AddDays(1));

            // Assert
            var reservationId = await _reservationsService.CreateAsync(command);

            // Act
            reservationId.ShouldNotBeNull();
            reservationId.Value.ShouldBe(command.ReservationId);
        }
    }
}
