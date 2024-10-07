using System.ComponentModel.DataAnnotations;

namespace Terminplaner_be.Models
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Passwort muss mindestens 8 Zeichen lang sein und muss mindestens einen Groß- und Kleinbuchstaben, ein Sonderzeichen und eine Zahl enthalten.")]
        public string Password { get; set; } = string.Empty;

        public ICollection<AppointmentEntity> Appointments { get; set; } = new List<AppointmentEntity>();
    }
}
