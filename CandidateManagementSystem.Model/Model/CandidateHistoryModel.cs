using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CandidateManagementSystem.Model.Model
{
    [Table("CandidateHistory")]
    public class CandidateHistoryModel :Common
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("CandidateModel")]
        public int CandidateId { get; set; }
        public CandidateModel CandidateModel { get; set; }
        [ForeignKey("StatusModel")]
        public int? StatusId { get; set; }
        public StatusModel StatusModel { get; set; }
        [ForeignKey("InterviewRoundModel")]
        public int? InterviewRoundId { get; set; }
        public InterviewRoundModel InterviewRoundModel { get; set; }
        [ForeignKey("InterviewerModel")]
        public int? InterviewerId { get; set; }
        public UserModel InterviewerModel { get; set; }
        public string Message { get; set; }
        public string InterviewStartDate { get; set; }
        public string TimeLineDate { get; set; }
    }
}
