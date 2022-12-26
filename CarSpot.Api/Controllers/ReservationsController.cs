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
        public ActionResult<IEnumerable<Reservation>> GetAll() => Ok(_reservationsService.GetAll());

        [HttpGet("{id:int}")]
        public ActionResult<Reservation> Get(int id)
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

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Reservation reservation)
        {
            reservation.ReservationId = id;
            if(_reservationsService.Update(reservation))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            if (_reservationsService.Delete(id))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
