using MvcAssignment.Data.Models;

namespace MvcAssignment.Data.Interfaces
{
    public interface IPersonRepository
    {
        public void Create(Person person);

        public void Update(Person person);

        public List<Person> GetAll();

        public void Delete(int id);
    }
}
