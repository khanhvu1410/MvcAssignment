using MvcAssignment.Data.Models;
using MvcAssignment.Shared.Enums;
using MvcAssignment.Data.Interfaces;

namespace MvcAssignment.Data
{
    public class RookiesDbContext : IRookiesDbContext
    {
        private readonly List<Person> _persons = [
            new Person
            {
                Id = 1,
                FirstName = "Khánh",
                LastName = "Vũ",
                Gender = Gender.Male,
                DateOfBirth = DateTime.Parse("2003-10-14"),
                PhoneNumber = "0928492950",
                BirthPlace = "Hà Nội",
                IsGraduated = false,
                CreatedDate = DateTime.Parse("2025-04-01")
            },
            new Person
            {
                Id = 2,
                FirstName = "Văn Chiến",
                LastName = "Thái",
                Gender = Gender.Male,
                DateOfBirth = DateTime.Parse("2003-07-29"),
                PhoneNumber = "0928492123",
                BirthPlace = "Hà Nội",
                IsGraduated = false,
                CreatedDate= DateTime.Parse("2025-04-01")
            },
            new Person
            {
                Id = 3,
                FirstName = "Hồng Giang",
                LastName = "Nguyễn",
                Gender = Gender.Female,
                DateOfBirth = DateTime.Parse("2003-05-22"),
                PhoneNumber = "0921292950",
                BirthPlace = "Bắc Ninh",
                IsGraduated = false,
                CreatedDate= DateTime.Parse("2025-04-01")
            },
            new Person
            {
                Id = 4,
                FirstName = "Đức Lâm",
                LastName = "Trần",
                Gender = Gender.Male,
                DateOfBirth = DateTime.Parse("1993-03-22"),
                PhoneNumber = "0921212950",
                BirthPlace = "Nam Định",
                IsGraduated = true,
                CreatedDate= DateTime.Parse("2025-04-01")
            },
            new Person
            {
                Id = 5,
                FirstName = "Thùy Linh",
                LastName = "Trần",
                Gender = Gender.Female,
                DateOfBirth = DateTime.Parse("2000-01-25"),
                PhoneNumber = "0921222950",
                BirthPlace = "Thái Bình",
                IsGraduated = true,
                CreatedDate= DateTime.Parse("2025-04-01")
            }
        ];

        public List<Person> Persons => _persons;
    }
}
