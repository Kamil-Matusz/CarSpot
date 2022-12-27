using CarSpot.Api.Entities;
using CarSpot.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarSpot.Api.Controllers
{
    [ApiController]
    [Route("reservations")]
    public class ReservationsController : ControllerBase
    {
       private readonly ReservationsService _reservationsService = new();
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetAll() => Ok(_reservationsService.GetAllWeekly());

        [HttpGet("{id:guid}")]
        public ActionResult<Reservation> Get(Guid id)
        {
           var reservation = _reservationsService.Get(id);
            if(reservation is null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult Post(Reservation reservation)
        {
            var id =  _reservationsService.Create(reservation);
            if(id is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = reservation.ReservationId }, null);
        }

        [HttpPut("{id:guid}")]
        public ActionResult Put(Guid id, Reservation reservation)
        {
            reservation.ReservationId = id;
            if(_reservationsService.Update(reservation))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id:guid")]
        public ActionResult Delete(Guid id)
        {
            if (_reservationsService.Delete(id))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
