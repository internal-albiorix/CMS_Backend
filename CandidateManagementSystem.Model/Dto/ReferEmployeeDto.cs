using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Model.Dto
{
    public class ReferEmployeeDto:Common
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string CandidateFullName { get; set; }
        public string CandidateEmail { get; set; }
        public string CandidateMobileNumber { get; set; }

        public List<int>? TechnologyIds { get; set; }
        public List<TechnologyModel> TechnologyModel { get; set; }
        public string CandidateExperience { get; set; }
        public string Resume { get; set; }
    }
}
