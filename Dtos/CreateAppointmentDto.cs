using System;
using System.ComponentModel.DataAnnotations;
using Terminplaner_be.Utility;

namespace Terminplaner_be.Dtos
{
    public class CreateAppointmentDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool AllDay { get; set; }
        [HexColor]
        public string Color { get; set; }
        [HexColor]
        public string SecondaryColor { get; set; }
    }
}
