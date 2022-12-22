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
        public ActionResult<IEnumerable<Reservation>> Get()
        {
            return Ok(_reservationService.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Reservation> Get(int id)
        {
           var reservation = _reservationService.Get(id);
            if(reservation is null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Reservation reservation)
        {
            var id = _reservationService.Create(reservation);
            if(id is null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = reservation.ReservationId }, null);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Reservation reservation)
        {
            reservation.ReservationId = id;
            if(_reservationService.Update(reservation))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            if (_reservationService.Delete(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
