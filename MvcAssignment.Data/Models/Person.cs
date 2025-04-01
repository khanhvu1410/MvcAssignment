using System.ComponentModel.DataAnnotations;
using MvcAssignment.Data.Enums;

namespace MvcAssignment.Data.Models
{
    public class Person
    {
        public int Id {  get; set; }

        [Required]
        public string? FirstName {  get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public string? BirthPlace { get; set; }    

        [Required]
        public bool IsGraduated { get; set; }
    }
}
