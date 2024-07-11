using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Model.Dto
{
    public class CandidateHistoryDto : Common
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public int? StatusId { get; set; }

        public int? InterviewRoundId { get; set; }

        public int? InterviewerId { get; set; }
        public string Message { get; set; }
        public string InterviewStartDate { get; set; }
    }
}
