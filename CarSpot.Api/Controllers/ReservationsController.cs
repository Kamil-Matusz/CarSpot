﻿using CarSpot.Api.Commands;
using CarSpot.Api.DTO;
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
    }
}
