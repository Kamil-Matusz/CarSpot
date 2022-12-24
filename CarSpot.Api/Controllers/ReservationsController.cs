using CarSpot.Api.Commands;
using CarSpot.Api.DTO;
using CarSpot.Api.Entities;
using CarSpot.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarSpot.Api.Controllers
{
    [Route("reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsService _reservationService = new();

        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> Get()
        {
            return Ok(_reservationService.GetAllWeekly());
        }

        [HttpGet("{id:guid}")]
        public ActionResult<ReservationDto> Get(Guid id)
        {
           var reservation = _reservationService.Get(id);
            if(reservation is null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult Post(CreateReservation command)
        {
            var id = _reservationService.Create(command with { ReservationId = Guid.NewGuid()});
            if(id is null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new {id}, null);
        }

        [HttpPut("{id:guid}")]
        public ActionResult Put(Guid id,ChangeReservationLicensePlate command)
        {
            if(_reservationService.Update(command with {ReservationId = id }))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            if (_reservationService.Delete(new DeleteReservation(id)))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
