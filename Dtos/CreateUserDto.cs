using System.ComponentModel.DataAnnotations;

namespace Terminplaner_be.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public required string? Username { get; set; }

        public required string Password { get; set; }
    }
}
