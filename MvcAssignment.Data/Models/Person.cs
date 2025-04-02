using MvcAssignment.Shared.DTOs;
using MvcAssignment.Shared.Enums;

namespace MvcAssignment.Data.Models
{
    public class Person
    {
        public int Id {  get; set; }

        public required string FirstName {  get; set; }

        public required string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public required string PhoneNumber { get; set; }

        public required string BirthPlace { get; set; }    

        public bool IsGraduated { get; set; }

        public DateTime CreatedDate {  get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime DeletedDate {  get; set; }

        public PersonDTO ToPersonDTO()
        {
            return new PersonDTO
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                DateOfBirth = DateOfBirth,
                PhoneNumber = PhoneNumber,
                BirthPlace = BirthPlace,
                IsGraduated = IsGraduated,
            };
        }
    }
}
