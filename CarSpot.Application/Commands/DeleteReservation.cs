using CarSpot.Application.Abstractions;

namespace CarSpot.Api.Commands
{
    public record DeleteReservation(Guid ReservationId) : ICommand;
}
