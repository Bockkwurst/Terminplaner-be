using System;
using System.ComponentModel.DataAnnotations;
using Terminplaner_be.Utility;

namespace Terminplaner_be.Models
{
    public class AppointmentEntity
    {
        public Guid Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool AllDay { get; set; }

        [HexColor]
        public string? Color { get; set; }
        [HexColor]
        public string? SecondaryColor { get; set; }
    }
}
