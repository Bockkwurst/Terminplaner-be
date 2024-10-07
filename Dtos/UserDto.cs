using System.ComponentModel.DataAnnotations;

namespace Terminplaner_be.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
    }
}
