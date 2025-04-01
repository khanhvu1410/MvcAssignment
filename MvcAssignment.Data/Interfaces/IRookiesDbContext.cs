using MvcAssignment.Data.Models;

namespace MvcAssignment.Data.Interfaces
{
    public interface IRookiesDbContext
    {
        public List<Person> Persons { get; }
    }
}
