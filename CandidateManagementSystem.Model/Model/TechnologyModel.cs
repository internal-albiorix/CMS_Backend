using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidateManagementSystem.Model.Model
{
    [Table("Technology")]
    public class TechnologyModel : Common
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TechnologyName { get; set; }
        public string TechnologyDescription { get; set; }
        
        //public ICollection<UserTechnology> UserTechnologies { get; set; }
    }
}
