using CarSpot.Api.Entities;
using CarSpot.Api.Exceptions;
using Shouldly;
using Xunit;

namespace CarSpot.Tests.Entities
{
    public class WeeklyParkingSpotTests
    {
        private readonly DateTime _date;
        private readonly WeeklyParkingSpot _weeklyParkingSpot;
        public WeeklyParkingSpotTests()
        {
            _weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new DateTime(2022, 12, 26), new DateTime(2023, 01, 01), "P1");
            _date = new DateTime(2022, 12, 31);
        }

        [Fact]
        public void given_reservation_for_already_existing_date_add_reservation_should_fail()
        {
            // Arrange
            var reservationDate = _date.AddDays(1);
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.WeeklyParkingSpotId, "John Doe","XYZ123",reservationDate);
            var nextReservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.WeeklyParkingSpotId, "John Doe", "XYZ123", reservationDate);
            _weeklyParkingSpot.AddReservation(reservation,_date);

            // Act
            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(nextReservation, reservationDate));

            // Assert
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ParkingSpotAlreadyReservedException>();
        }

        [Fact]
        public void given_reservation_for_not_taken_date_add_reservation_should_success()
        {
            // Arrange
            var reservationDate = _date.AddDays(1);
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.WeeklyParkingSpotId, "John Doe", "XYZ123", reservationDate);

            // Act
            _weeklyParkingSpot.AddReservation(reservation, _date);

            // Assert
            _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
        }
    }
}
