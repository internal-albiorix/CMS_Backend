using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidateManagementSystem.Model.Model
{
    [Table("InterviewSchedule")]
    public class InterviewScheduleModel : Common
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string? Title { get; set; }
        [ForeignKey("CandidateModel")]
        public int CandidateId { get; set; }
        public CandidateModel CandidateModel { get; set; }
        [ForeignKey("InterviewerModel")]
        public int? InterviewerId { get; set; }
        public UserModel InterviewerModel { get; set; }
        [ForeignKey("InterviewRoundModel")]
        public int? InterviewRoundId { get; set; }
        public InterviewRoundModel InterviewRoundModel { get; set; }
        public string? EventId {  get; set; }
        public string? GoogleMeetLink {  get; set; }
        public bool? IsReject { get; set; }

    }

}
