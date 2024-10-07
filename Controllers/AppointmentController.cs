using Microsoft.AspNetCore.Mvc;
using Terminplaner_be.Context;
using Terminplaner_be.Dtos;
using Terminplaner_be.Models;
using Terminplaner_be.Utility;
using System.Globalization;

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

            if (!TimeValidator.IsValidTime(appointmentDto.StartTime))
            {
                return BadRequest("Invalid Start Time.");
            }

            if (!TimeValidator.IsValidTime(appointmentDto.EndTime))
            {
                return BadRequest("Invalid End Time.");
            }

            TimeSpan startTime = TimeSpan.ParseExact(appointmentDto.StartTime, "hh\\:mm", CultureInfo.InvariantCulture);
            TimeSpan endTime = TimeSpan.ParseExact(appointmentDto.EndTime, "hh\\:mm", CultureInfo.InvariantCulture);

            var appointment = new AppointmentEntity
            {
                Title = appointmentDto.Title,
                StartDate = appointmentDto.StartDate.Date,
                EndDate = appointmentDto.EndDate.Date,
                StartTime = appointmentDto.StartTime,
                EndTime = appointmentDto.EndTime,
                AllDay = appointmentDto.AllDay,
                Color = appointmentDto.Color,
                SecondaryColor = appointmentDto.SecondaryColor
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return Ok(appointment);
        }

        [HttpGet("{id}")]
        public IActionResult GetAppointmentById(Guid id) 
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
            {
                return NotFound($"Apointment with Id {id} not found.");
            }
            return Ok(appointment);
        }

        [HttpGet("all")]
        public IActionResult GetAllAppointments()
        {
            try
            {
                var appointments = _context.Appointments.ToList();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("/search/{title}")]
        public IActionResult GetAppointmentByTitle(string title)
        {
            try
            {
                var appointment = _context.Appointments.FirstOrDefault(a => a.Title == title);

                if (appointment == null)
                {
                    return NotFound($"Appointment with Title {title} not found.");
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
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
            try
            {
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
