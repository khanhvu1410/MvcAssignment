using MvcAssignment.Data.Interfaces;
using MvcAssignment.Data.Models;

namespace MvcAssignment.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public readonly IRookiesDbContext _context;

        public PersonRepository(IRookiesDbContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            _context.Persons.Add(person);
            return person;
        }

        public List<Person> GetAll() => _context.Persons;

        public Person? GetById(int id) => _context.Persons.FirstOrDefault(p => p.Id == id);
          

        public Person Update(Person person)
        {
            var existingPerson = _context.Persons.First(p => p.Id == person.Id);
            
            existingPerson.FirstName = person.FirstName;
            existingPerson.LastName = person.LastName;
            existingPerson.Gender = person.Gender;
            existingPerson.PhoneNumber = person.PhoneNumber;
            existingPerson.BirthPlace = person.BirthPlace;
            existingPerson.DateOfBirth = person.DateOfBirth;
            existingPerson.IsGraduated = person.IsGraduated;
            existingPerson.UpdatedDate = DateTime.Now;

            return person;
        }

        public void Delete(int id) => _context.Persons.RemoveAll(p => p.Id == id);
    }
}
