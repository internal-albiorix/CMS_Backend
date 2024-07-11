using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidateManagementSystem.Model.Model
{
    [Table("Users")]
    public class UserModel : Common
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        [ForeignKey("DesignationModel")]
        public int DesignationId { get; set; }
        public DesignationModel DesignationModel { get; set; }
        [NotMapped]
        public List<TechnologyModel> TechnologyModel { get; set; }
        [NotMapped]
        public List<int> TechnologyIds { get; set; }
        public ICollection<TechnologyAssociation> UserTechnologies { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public int Role { get; set; }
    }

    public static class CurrentUser
    {
        public static UserModel User { get; set; }
    }
}
