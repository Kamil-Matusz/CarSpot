using CarSpot.Api.Entities;
using CarSpot.Api.Exceptions;
using CarSpot.Api.Services;
using CarSpot.Core.Entities;
using CarSpot.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace CarSpot.Tests.Entities
{
    public class WeeklyParkingSpotTests
    {
        private readonly DateTime _date;
        private readonly IClock _clock;
        private readonly WeeklyParkingSpot _weeklyParkingSpot;
        private readonly Date _now;
        public WeeklyParkingSpotTests()
        {
            new WeeklyParkingSpot(Guid.NewGuid(), new Week(_clock.CurrentDate()), "P1");
            _now = new Date(DateTime.Parse("2022-02-15"));
        }

        [Fact]
        public void given_reservation_for_already_existing_date_add_reservation_should_fail()
        {
            var reservationDate = _now.AddDays(1);
            var reservation = new VehicleReservation(Guid.NewGuid(), Guid.NewGuid(), "Joe Doe", "XYZ123", reservationDate);
            _weeklyParkingSpot.AddReservation(reservation, reservationDate);

            //ACT
            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, reservationDate));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ParkingSpotAlreadyReservedException>();
        }

        [Fact]
        public void given_reservation_for_not_taken_date_add_reservation_should_success()
        {
            // Arrange
            var reservationDate = _now.AddDays(1);
            var reservation = new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.WeeklyParkingSpotId, "John Doe", "XYZ123", reservationDate);

            // Act
            _weeklyParkingSpot.AddReservation(reservation, _now);

            // Assert
            _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
        }

        [Theory]
        [InlineData("2020-02-02")]
        [InlineData("2025-02-02")]
        [InlineData("2022-02-24")]
        public void given_invalid_date_add_reservation_should_fail(string dateString)
        {
            var invalidDate = DateTime.Parse(dateString);

            //ARRANGE
            var reservation = new VehicleReservation(Guid.NewGuid(),_weeklyParkingSpot.WeeklyParkingSpotId, "Joe Doe", "XYZ123", new Date(invalidDate));

            //ACT
            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, _now));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidReservationDateException>();
        }

    }
}
