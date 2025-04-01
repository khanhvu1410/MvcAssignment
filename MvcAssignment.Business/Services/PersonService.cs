using MvcAssignment.Business.Interfaces;
using MvcAssignment.Data.Enums;
using MvcAssignment.Data.Interfaces;
using MvcAssignment.Data.Models;
using OfficeOpenXml;

namespace MvcAssignment.Business.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public List<Person> GetAll() => _personRepository.GetAll();

        public List<Person> GetMaleMembers() => _personRepository.GetAll().Where(p => p.Gender == Gender.Male).ToList();

        public Person GetOldestMember() => _personRepository.GetAll().OrderBy(p => p.DateOfBirth).First();

        public List<string> GetFullnames() => _personRepository.GetAll().Select(p => $"{p.LastName} {p.FirstName}").ToList();

        public List<Person> GetMembersByBirthYear(int year) => _personRepository.GetAll().Where(p => p.DateOfBirth.Year == year).ToList();

        public List<Person> GetMembersByBirthYearGreater(int year) => _personRepository.GetAll().Where(p => p.DateOfBirth.Year > year).ToList();

        public List<Person> GetMembersByBirthYearLess(int year) => _personRepository.GetAll().Where(p => p.DateOfBirth.Year < year).ToList();

        public MemoryStream WriteMembersToExcel()
        {
            var persons = _personRepository.GetAll();

            ExcelPackage.License.SetNonCommercialPersonal("My Name");

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Rookies");
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "First name";
            worksheet.Cells[1, 3].Value = "Last name";
            worksheet.Cells[1, 4].Value = "Gender";
            worksheet.Cells[1, 5].Value = "Date of birth";
            worksheet.Cells[1, 6].Value = "Phone number";
            worksheet.Cells[1, 7].Value = "Birth place";
            worksheet.Cells[1, 8].Value = "Is graduated";

            for (int i = 0; i < persons.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = persons[i].Id;
                worksheet.Cells[i + 2, 2].Value = persons[i].FirstName;
                worksheet.Cells[i + 2, 3].Value = persons[i].LastName;
                worksheet.Cells[i + 2, 4].Value = persons[i].Gender;
                worksheet.Cells[i + 2, 5].Value = persons[i].DateOfBirth.ToString("dd/MM/yyyy");
                worksheet.Cells[i + 2, 6].Value = persons[i].PhoneNumber;
                worksheet.Cells[i + 2, 7].Value = persons[i].BirthPlace;
                worksheet.Cells[i + 2, 8].Value = persons[i].IsGraduated;
            }

            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            return stream;
        }
    }
}
