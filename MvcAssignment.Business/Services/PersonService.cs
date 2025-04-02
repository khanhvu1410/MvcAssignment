using MvcAssignment.Business.Interfaces;
using MvcAssignment.Shared.Enums;
using MvcAssignment.Data.Interfaces;
using OfficeOpenXml;
using MvcAssignment.Shared.DTOs;
using MvcAssignment.Data.Models;

namespace MvcAssignment.Business.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public List<PersonDTO> GetAllMembers()
        {
            var persons = _personRepository.GetAll().Select(p => p.ToPersonDTO()).ToList();

            return persons;
        }
        

        public List<PersonDTO> GetMaleMembers()
        {
            var persons = _personRepository.GetAll().Where(p => p.Gender == Gender.Male);

            var maleMemebers = persons.Select(p => p.ToPersonDTO()).ToList();

            return maleMemebers;
        }


        public PersonDTO GetOldestMember()
        {
            var persons = _personRepository.GetAll();

            var oldestPerson = persons.OrderBy(p => p.DateOfBirth).FirstOrDefault();

            if (oldestPerson == null)
            {
                throw new KeyNotFoundException("No person found.");
            }

            return oldestPerson.ToPersonDTO();
        }

        public List<string> GetFullnames() 
        {
            var fullnames = _personRepository.GetAll().Select(p => $"{p.LastName} {p.FirstName}").ToList();

            if (fullnames.Count == 0)
            {
                throw new KeyNotFoundException("No name found.");
            }

            return fullnames;
        } 


        public List<PersonDTO> GetMembersByBirthYear(int year)
        {
            var persons = _personRepository.GetAll().Where(p => p.DateOfBirth.Year == year);
            
            var result = persons.Select(p => p.ToPersonDTO()).ToList();
            
            return result;
        }

        public List<PersonDTO> GetMembersByBirthYearGreater(int year)
        {
            var persons = _personRepository.GetAll().Where(p => p.DateOfBirth.Year > year);
            
            var result = persons.Select(p => p.ToPersonDTO()).ToList();          
            
            return result;
        }
        
        public List<PersonDTO> GetMembersByBirthYearLess(int year)
        {
            var persons = _personRepository.GetAll().Where(p => p.DateOfBirth.Year < year);
            
            var result = persons.Select(p => p.ToPersonDTO()).ToList();         
            
            return result;
        }

        public MemoryStream WriteMembersToExcel()
        {
            var persons = _personRepository.GetAll();

            if (persons.Count == 0) 
            {
                throw new KeyNotFoundException("No person to write to Excel.");
            }

            ExcelPackage.License.SetNonCommercialPersonal("Khanh Vu");

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Rookies");
            worksheet.Cells[1, 1].Value = "First name";
            worksheet.Cells[1, 2].Value = "Last name";
            worksheet.Cells[1, 3].Value = "Gender";
            worksheet.Cells[1, 4].Value = "Date of birth";
            worksheet.Cells[1, 5].Value = "Phone number";
            worksheet.Cells[1, 6].Value = "Birth place";
            worksheet.Cells[1, 7].Value = "Is graduated";

            for (int i = 0; i < persons.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = persons[i].FirstName;
                worksheet.Cells[i + 2, 2].Value = persons[i].LastName;
                worksheet.Cells[i + 2, 3].Value = persons[i].Gender;
                worksheet.Cells[i + 2, 4].Value = persons[i].DateOfBirth.ToString("dd/MM/yyyy");
                worksheet.Cells[i + 2, 5].Value = persons[i].PhoneNumber;
                worksheet.Cells[i + 2, 6].Value = persons[i].BirthPlace;
                worksheet.Cells[i + 2, 7].Value = persons[i].IsGraduated;
            }

            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            return stream;
        }

        public PersonDTO CreateNewMember(PersonToCreateDTO personDto)
        {
            var persons = _personRepository.GetAll();
            var id = persons.Count == 0 ? 1 : persons.Max(p => p.Id) + 1;
            
            var person = new Person
            {
                Id = id,
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                Gender = personDto.Gender,
                DateOfBirth = personDto.DateOfBirth,
                PhoneNumber = personDto.PhoneNumber,
                BirthPlace = personDto.BirthPlace,
                IsGraduated = personDto.IsGraduated,
                CreatedDate = DateTime.Now,
            };

            return _personRepository.Create(person).ToPersonDTO();
        }

        public PersonDTO EditMember(PersonDTO personDto)
        {
            var existingPerson = _personRepository.GetById(personDto.Id);

            if (existingPerson == null)
            {
                throw new KeyNotFoundException($"No person with ID {personDto.Id} found.");
            }

            var person = new Person
            {
                Id = personDto.Id,
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                Gender = personDto.Gender,
                DateOfBirth= personDto.DateOfBirth,
                PhoneNumber = personDto.PhoneNumber,
                BirthPlace = personDto.BirthPlace,
                IsGraduated = personDto.IsGraduated,
                UpdatedDate = DateTime.Now,
            };
            
            return _personRepository.Update(person).ToPersonDTO();
        }
    
        public PersonDTO GetMemberById(int id)
        {
            var person = _personRepository.GetById(id);

            if (person == null)
            {
                throw new KeyNotFoundException($"No person with ID {id} found.");
            }

            return person.ToPersonDTO();
        }

        public void DeleteMember(int id)
        {
            var person = _personRepository.GetById(id);

            if (person == null)
            {
                throw new KeyNotFoundException($"No person with ID {id} found.");
            }

            _personRepository.Delete(id);
        }
    }
}
