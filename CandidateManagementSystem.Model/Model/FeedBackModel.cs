using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidateManagementSystem.Model.Model
{
    [Table("FeedBack")]
    public class FeedBackModel :Common
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TodayDate { get; set; }
        public string InterviewerFullName { get; set; }
        public string CandidateFullName { get; set; }
        public string Experience { get; set; }
        [NotMapped]
        public List<int> TechnologyIds { get; set; }
        [NotMapped]
        public List<TechnologyModel> TechnologyModel { get; set; }
        public ICollection<TechnologyAssociation> FeedbackTechnologies { get; set; }
        [ForeignKey("InterviewRoundModel")]
        public int InterviewRoundId { get; set; }
        public InterviewRoundModel InterviewRoundModel { get; set; }
        public string Communication { get; set; }
        public string Practicalcompletion { get; set; }
        public string Codingstandard { get; set; }
        public string Techanicalround { get; set; }
        public string RecommandedforPractical { get; set; }
        public string Comments { get; set; }
        [ForeignKey("CandidateModel")]
        public int CandidateId { get; set; }
        public CandidateModel CandidateModel { get; set; }

    }
}
