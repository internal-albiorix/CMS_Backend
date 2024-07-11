using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Model.Dto
{
    public class InterviewScheduleDto : Common
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Title { get; set; }

        public int CandidateId { get; set; }

        public int InterviewerId { get; set; }

        public int InterviewRoundId { get; set; }
        public string? EventId { get; set; }
        public string? GoogleMeetLink { get; set; }
        public bool? IsReject { get; set; }
    }
}
