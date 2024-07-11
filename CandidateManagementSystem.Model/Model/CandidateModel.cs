using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CandidateManagementSystem.Model.Model
{
    [Table("Candidates")]
    public class CandidateModel : Common
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        [NotMapped]
        public List<int> TechnologyIds { get; set; }
        [NotMapped]
        public List<TechnologyModel> TechnologyModel { get; set; }
        public ICollection<TechnologyAssociation> CandidateTechnologies { get; set; }
        public string Experience { get; set; }
        [ForeignKey("StatusModel")]
        public int StatusId { get; set; }
        public StatusModel StatusModel { get; set; }
        public string Resume { get; set; }
        [NotMapped]
        public string InterviewerName { get; set; }
        [NotMapped]
        public bool IsReject { get; set; }
    
    }
   
}
