using CarSpot.Api.Commands;
using CarSpot.Api.DTO;
using CarSpot.Api.Entities;
using CarSpot.Api.Services;
using CarSpot.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CarSpot.Api.Controllers
{
    [ApiController]
    [Route("reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _reservationsService;
        public ReservationsController(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }
        /* public ReservationsController(IReservationsService reservationsService)
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAll() => Ok(await _reservationsService.GetAllWeeklyAsync());

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ReservationDto>> Get(Guid id)
        {
            var reservation = await _reservationsService.GetAsync(id);
            if (reservation is null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost("vehicle")]
        public async Task<ActionResult> Post(ReserveParkingSpotForVehicle command)
        {
            var id = await _reservationsService.CreateAsync(command with { ReservationId = Guid.NewGuid() });
            if (id is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { ReservationId = id }, null);
        }

        [HttpPost("cleaning")]
        public async Task<ActionResult> Post(ReserveParkingSpotForCleaning command)
        {
            await _reservationsService.ReserveForCleaningAsync(command);

            return Ok();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, ChangeReservationLicencePlate command)
        {

            if (await _reservationsService.UpdateAsync(command with { ReservationId = id }))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _reservationsService.DeleteAsync(new DeleteReservation(id)))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
