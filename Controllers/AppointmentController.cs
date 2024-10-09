using Microsoft.AspNetCore.Mvc;
using Terminplaner_be.Context;
using Terminplaner_be.Dtos;
using Terminplaner_be.Models;
using Terminplaner_be.Utility;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("create")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto? appointmentDto)
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

            var userId = appointmentDto.userId;

            Console.WriteLine($"Received appointmentDto: {appointmentDto}");
            Console.WriteLine($"userId: {appointmentDto.userId}");

            TimeSpan startTime = TimeSpan.ParseExact(appointmentDto.StartTime, "hh\\:mm", CultureInfo.InvariantCulture);
            TimeSpan endTime = TimeSpan.ParseExact(appointmentDto.EndTime, "hh\\:mm", CultureInfo.InvariantCulture);

            string userIdString = userId.ToString();

            if (userIdString == null)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            var appointment = new AppointmentEntity
            {
                Title = appointmentDto.Title,
                StartDate = appointmentDto.StartDate.Date,
                EndDate = appointmentDto.EndDate.Date,
                StartTime = appointmentDto.StartTime,
                EndTime = appointmentDto.EndTime,
                AllDay = appointmentDto.AllDay,
                Color = appointmentDto.Color,
                SecondaryColor = appointmentDto.SecondaryColor,
                User = await _context.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userIdString))
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return Ok(appointment);
        }

        private Guid GetLoggedInUserId()
        {
            var token = Request.Cookies["jwt"];

            if (string.IsNullOrEmpty(token))
            {
                return Guid.Empty;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            var userIdClaim = jwtSecurityToken!.Claims.FirstOrDefault(c => c.Type == "sub");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Guid.Empty;
            }

            return userId;
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
