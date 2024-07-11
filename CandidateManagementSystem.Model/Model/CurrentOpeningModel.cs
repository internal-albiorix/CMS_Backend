using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidateManagementSystem.Model.Model
{
    [Table("CurrentOpening")]
    public class CurrentOpeningModel : Common
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("DesignationModel")]
        public int DesignationId { get; set; }
        public DesignationModel DesignationModel { get; set; }
        [ForeignKey("TechnologyModel")]
        public int TechnologyId { get; set; }
        public TechnologyModel TechnologyModel { get; set; }
        public string Experience { get; set; }
        public int Noofopening { get; set; }
      
    }
}
