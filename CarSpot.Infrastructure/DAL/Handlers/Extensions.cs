using CarSpot.Api.DTO;
using CarSpot.Api.Entities;
using CarSpot.Application.DTO;
using CarSpot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.DAL.Handlers
{
    internal static class Extensions
    {
        // method which convert value do DTO
        public static WeeklyParkingSpotDto AsDto(this WeeklyParkingSpot entity)
       => new()
       {
           Id = entity.WeeklyParkingSpotId.Value.ToString(),
           Name = entity.ParkingSpotName,
           Capacity = entity.Capacity,
           From = entity.Week.From.Value.DateTime,
           To = entity.Week.To.Value.DateTime,
           Reservations = entity.Reservations.Select(x => new ReservationDto
           {
               ReservationDtoId = x.ReservationId,
               BookerName = x is VehicleReservation vr ? vr.BookerName : null,
               ReservationDate = x.ReservationDate.Value.Date
           })
       };

        public static UserDto AsDto(this User entity)
        => new()
        {
            UserId = entity.UserId,
            Username = entity.Username,
            Fullname = entity.FullName
        };
    }
}
