using System.ComponentModel.DataAnnotations;

namespace Terminplaner_be.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
