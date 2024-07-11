using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Model.Dto
{
    public class UserDto : Common
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int DesignationId { get; set; }
        public DesignationModel DesignationModel { get; set; }
        public List<TechnologyModel> TechnologyModel { get; set; }
        public List<int> TechnologyIds { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public int Role { get; set; }
    }
}
