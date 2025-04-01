using MvcAssignment.Data.Models;

namespace MvcAssignment.Business.Interfaces
{
    public interface IPersonService
    {
        public List<Person> GetAll();

        public List<Person> GetMaleMembers();

        public Person GetOldestMember();

        public List<string> GetFullnames();

        public List<Person> GetMembersByBirthYear(int year);

        public List<Person> GetMembersByBirthYearGreater(int year);

        public List<Person> GetMembersByBirthYearLess(int year);

        public MemoryStream WriteMembersToExcel();
    }
}
