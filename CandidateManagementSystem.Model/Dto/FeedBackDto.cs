using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Model.Dto
{
    public class FeedBackDto : Common
    {
       
        public int Id { get; set; }
        public string TodayDate { get; set; }
        public string InterviewerFullName { get; set; }
        public string CandidateFullName { get; set; }
        public string Experience { get; set; }

        public List<int> TechnologyIds { get; set; }
        public List<TechnologyModel> TechnologyModel { get; set; }

        public int InterviewRoundId { get; set; }
        public string Communication { get; set; }
        public string Practicalcompletion { get; set; }
        public string Codingstandard { get; set; }
        public string Techanicalround { get; set; }
        public string RecommandedforPractical { get; set; }
        public string Comments { get; set; }

        public int CandidateId { get; set; }
    }
}
