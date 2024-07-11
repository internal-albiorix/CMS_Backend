

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidateManagementSystem.Model.Model
{
    [Table("InterviewRound")]
    public class InterviewRoundModel : Common
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string InterviewRoundName { get; set; }
        public string InterviewRoundDescription { get; set; }
    }
}
