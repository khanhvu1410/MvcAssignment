using System.ComponentModel.DataAnnotations;
using MvcAssignment.Shared.Enums;

namespace MvcAssignment.Shared.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone number")]
        public required string PhoneNumber { get; set; }

        [Required]
        public required string BirthPlace { get; set; }

        [Required]
        public bool IsGraduated { get; set; }
    }
}
