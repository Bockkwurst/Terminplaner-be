using System;
using System.ComponentModel.DataAnnotations;
using Terminplaner_be.Utility;
using Terminplaner_be.Dtos;

namespace Terminplaner_be.Dtos
{
    public class CreateAppointmentDto
    {
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public bool AllDay { get; set; }
        [HexColor]
        public string? Color { get; set; }
        [HexColor]
        public string? SecondaryColor { get; set; }
        public required string userId { get; set; }
    }
}
