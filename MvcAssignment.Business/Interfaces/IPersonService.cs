using MvcAssignment.Shared.DTOs;

namespace MvcAssignment.Business.Interfaces
{
    public interface IPersonService
    {
        public List<PersonDTO> GetAllMembers();

        public List<PersonDTO> GetMaleMembers();

        public PersonDTO GetOldestMember();

        public List<string> GetFullnames();

        public List<PersonDTO> GetMembersByBirthYear(int year);

        public List<PersonDTO> GetMembersByBirthYearGreater(int year);

        public List<PersonDTO> GetMembersByBirthYearLess(int year);

        public MemoryStream WriteMembersToExcel();

        public PersonDTO CreateNewMember(PersonToCreateDTO person);

        public PersonDTO EditMember(PersonDTO member);

        public PersonDTO GetMemberById(int id);

        public void DeleteMember(int id);
    }
}
