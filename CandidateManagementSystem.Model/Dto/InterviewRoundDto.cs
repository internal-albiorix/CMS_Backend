using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Model.Dto
{
    public class InterviewRoundDto : Common
    {
        public int Id { get; set; }
        public string InterviewRoundName { get; set; }
        public string InterviewRoundDescription { get; set; }
    }
}
