using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidateManagementSystem.Model.Model
{
    [Table("ReferEmployee")]
    public class ReferEmployeeModel : Common
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string CandidateFullName { get; set; }
        public string CandidateEmail { get; set; }
        public string CandidateMobileNumber { get; set; }
        [NotMapped]
        public List<int> TechnologyIds { get; set; }
        [NotMapped]
        public List<TechnologyModel> TechnologyModel { get; set; }

        public ICollection<TechnologyAssociation> ReferEmployeeTechnologies { get; set; }
        
        public string CandidateExperience { get; set; }
        public string Resume { get; set; }

    }
}
