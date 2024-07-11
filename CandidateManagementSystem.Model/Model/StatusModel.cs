using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidateManagementSystem.Model.Model
{
    [Table("Status")]
    public class StatusModel : Common
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string StatusName { get; set; }
        public string? StatusDescription { get; set; }
        public bool DisplayInAppointSchedule { get; set; }
    }
}
