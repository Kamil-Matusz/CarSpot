using CarSpot.Api.Commands;
using CarSpot.Api.DTO;
using CarSpot.Api.Entities;
using CarSpot.Api.Services;
using CarSpot.Application.Abstractions;
using CarSpot.Application.Commands;
using CarSpot.Application.DTO;
using CarSpot.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CarSpot.Api.Controllers
{
    [ApiController]
    [Route("parking-spots")]
    public class ReservationsController : ControllerBase
    {
        /*
        private readonly IReservationsService _reservationsService;
        public ReservationsController(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }
         public ReservationsController(IReservationsService reservationsService)
         {
             _reservationsService= reservationsService;
         }

         [HttpGet]
         public ActionResult<IEnumerable<ReservationDto>> GetAll() => Ok(_reservationsService.GetAllWeekly());

         [HttpGet("{id:guid}")]
         public ActionResult<ReservationDto> Get(Guid id)
         {
            var reservation = _reservationsService.Get(id);
             if(reservation is null)
             {
                 return NotFound();
             }
             return Ok(reservation);
         }

         [HttpPost]
         public ActionResult Post(CreateReservation command)
         {
             var id =  _reservationsService.Create(command with { ReservationId = Guid.NewGuid()});
             if(id is null)
             {
                 return BadRequest();
             }

             return CreatedAtAction(nameof(Get), new {ReservationId = id}, null);
         }

         [HttpPut("{id:guid}")]
         public ActionResult Put(Guid id, ChangeReservationLicencePlate command)
         {

             if(_reservationsService.Update(command with { ReservationId = id}))
             {
                 return NoContent();
             }

             return NotFound();
         }

         [HttpDelete("{id:guid}")]
         public ActionResult Delete(Guid id)
         {
             if (_reservationsService.Delete(new DeleteReservation(id)))
             {
                 return NoContent();
             }

             return NotFound();
         }
     }*/

        private readonly ICommandHandler<ReserveParkingSpotForVehicle> _reserveParkingSpotsForVehicleHandler;
        private readonly ICommandHandler<ReserveParkingSpotForCleaning> _reserveParkingSpotsForCleaningHandler;
        private readonly ICommandHandler<ChangeReservationLicencePlate> _changeReservationLicencePlateHandler;
        private readonly ICommandHandler<DeleteReservation> _deleteReservationHandler;
        private readonly IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>>_getWeeklyParkingSpotsHandler;

        public ReservationsController(ICommandHandler<ReserveParkingSpotForVehicle> reserveParkingSpotsForVehicleHandler,
            ICommandHandler<ReserveParkingSpotForCleaning> reserveParkingSpotsForCleaningHandler,
            ICommandHandler<ChangeReservationLicencePlate> changeReservationLicencePlateHandler,
            ICommandHandler<DeleteReservation> deleteReservationHandler, IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> getWeeklyParkingSpotsHandler)
        {
            _reserveParkingSpotsForVehicleHandler = reserveParkingSpotsForVehicleHandler;
            _reserveParkingSpotsForCleaningHandler = reserveParkingSpotsForCleaningHandler;
            _changeReservationLicencePlateHandler = changeReservationLicencePlateHandler;
            _deleteReservationHandler = deleteReservationHandler;
            _getWeeklyParkingSpotsHandler = getWeeklyParkingSpotsHandler;
        }

        [HttpGet("reservations")]
        [SwaggerOperation("Displaying all reservations")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<WeeklyParkingSpotDto>>> Get([FromQuery] GetWeeklyParkingSpots query)
        => Ok(await _getWeeklyParkingSpotsHandler.HandleAsync(query));

        [HttpPost("{parkingSpotId:guid}/reservations/vehicle")]
        [SwaggerOperation("Creating reservation for the vehicle")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(Guid parkingSpotId, ReserveParkingSpotForVehicle command)
        {
            await _reserveParkingSpotsForVehicleHandler.HandlerAsync(command with
            {
                ReservationId = Guid.NewGuid(),
                ParkingSpotId = parkingSpotId,
            });
            return NoContent();
        }

        [HttpPost("reservations/cleaning")]
        [SwaggerOperation("Creating reservation for the cleaning parkingspots")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(ReserveParkingSpotForCleaning command)
        {
            await _reserveParkingSpotsForCleaningHandler.HandlerAsync(command);
            return NoContent();
        }

        [HttpPut("reservations/{reservationId:guid}")]
        [SwaggerOperation("Change vehicle license plate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put(Guid reservationId, ChangeReservationLicencePlate command)
        {
            await _changeReservationLicencePlateHandler.HandlerAsync(command with { ReservationId = reservationId });
            return NoContent();
        }

        [HttpDelete("reservations/{reservationId:guid}")]
        [SwaggerOperation("Delete reservation for the vehicle")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(Guid reservationId)
        {
            await _deleteReservationHandler.HandlerAsync(new DeleteReservation(reservationId));
            return NoContent();
        }
    }
}
