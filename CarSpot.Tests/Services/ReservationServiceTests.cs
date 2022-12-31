using CarSpot.Api.Commands;
using CarSpot.Api.Services;
using Shouldly;
using Xunit;

namespace CarSpot.Tests.Services
{
    public class ReservationServiceTests
    {
        private readonly ReservationsService _reservationsService;
        public ReservationServiceTests()
        {
            _reservationsService= new ReservationsService();
        }

        [Fact]
        public void given_reservation_for_not_taken_date_create_reservation_should_success()
        {
            // Arrange
            var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000001"),Guid.NewGuid(),"John Doe","XYZ123",DateTime.UtcNow.AddDays(1));

            // Assert
            var reservationId = _reservationsService.Create(command);

            // Act
            reservationId.ShouldNotBeNull();
            reservationId.Value.ShouldBe(command.ReservationId);
        }
    }
}
