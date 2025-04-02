using MvcAssignment.Data.Models;

namespace MvcAssignment.Data.Interfaces
{
    public interface IPersonRepository
    {
        public Person Create(Person person);

        public Person Update(Person person);

        public List<Person> GetAll();

        public Person? GetById(int id);

        public void Delete(int id);
    }
}
