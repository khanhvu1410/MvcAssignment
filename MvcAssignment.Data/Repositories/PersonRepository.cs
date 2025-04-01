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

        public void Create(Person person) => _context.Persons.Add(person);

        public List<Person> GetAll()
        {
            var persons = _context.Persons;

            if (persons.Count == 0)
            {
                throw new KeyNotFoundException("No person found.");
            }

            return persons;
        }

        public void Update(Person person)
        {
            var existingPerson = _context.Persons.FirstOrDefault(p => p.Id == person.Id);
            
            if (existingPerson == null)
            {
                throw new KeyNotFoundException($"No person with ID {person.Id} found.");
            }

            existingPerson.Id = person.Id;
            existingPerson.FirstName = person.FirstName;
            existingPerson.LastName = person.LastName;
            existingPerson.Gender = person.Gender;
            existingPerson.PhoneNumber = person.PhoneNumber;
            existingPerson.BirthPlace = person.BirthPlace;
            existingPerson.DateOfBirth = person.DateOfBirth;
            existingPerson.IsGraduated = person.IsGraduated;
        }

        public void Delete(int id)
        {
            var removedCount = _context.Persons.RemoveAll(p => p.Id == id);

            if (removedCount == 0)
            {
                throw new KeyNotFoundException($"No person with ID {id} found.");
            }
        }
    }
}
