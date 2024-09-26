using Microsoft.AspNetCore.Mvc;
using Terminplaner_be.Context;
using Terminplaner_be.Dtos;
using Terminplaner_be.Models;

namespace Terminplaner_be.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly TerminplanerDbContext _context;

        public AppointmentController( TerminplanerDbContext context )
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateAppointment([FromBody] CreateAppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
            {
                return BadRequest("Appointment Data is null.");
            }

            var appointment = new AppointmentEntity
            {
                Title = appointmentDto.Title,
                StartDate = appointmentDto.StartDate,
                EndDate = appointmentDto.EndDate,
                AllDay = appointmentDto.AllDay,
                Color = appointmentDto.Color,
                SecondaryColor = appointmentDto.SecondaryColor
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return Ok(appointment);
        }

        [HttpGet("{id}")]
        public IActionResult GetAppointment(Guid id) 
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
            {
                return NotFound($"Apointment with Id {id} not found.");
            }
            return Ok(appointment);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAppointment(Guid id, [FromBody] CreateAppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
            {
                return BadRequest("Appointment Data is null.");
            }

            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
            {
                return NotFound($"Appointment with ID {id} not found.");
            }

            appointment.Title = appointmentDto.Title;
            appointment.StartDate = appointmentDto.StartDate;
            appointment.EndDate = appointmentDto.EndDate;
            appointment.AllDay = appointmentDto.AllDay;
            appointment.Color = appointmentDto.Color;
            appointment.SecondaryColor = appointmentDto.SecondaryColor;

            _context.Appointments.Update(appointment);
            _context.SaveChanges();

            return Ok(appointment);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(Guid id) 
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
            {
                return NotFound($"Appointment with the ID {id} not found.");
            }

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
