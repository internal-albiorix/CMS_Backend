using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Model.Dto
{
    public class CandidateDto : Common
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public ICollection<TechnologyAssociation> CandidateTechnologies { get; set; }
        public List<TechnologyModel> TechnologyModel { get; set; }
        public List<int> TechnologyIds { get; set; }
        public string Experience { get; set; }

        public int StatusId { get; set; }
        public StatusModel StatusModel { get; set; }
        public string Resume { get; set; }
        public bool IsReject { get; set; }
        public string InterviewerName { get; set; }

    }
    public class CandidateFilterDto
    {
        public int? TimeFrame { get; set; }
        public string[] Technologies { get; set; }
        public string[] CandidateStatus { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
