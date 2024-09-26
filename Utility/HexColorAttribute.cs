using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Terminplaner_be.Utility
{
    public class HexColorAttribute : ValidationAttribute
    {
        private static readonly Regex HexColorRegex = new Regex("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", RegexOptions.Compiled);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || HexColorRegex.IsMatch(value.ToString()))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("The field must be a valid Hex color code.");
        }
    }
}
