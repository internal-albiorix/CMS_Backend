using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagementSystem.Model.Model
{
    [Table("Inquiries")]
    public class InquiriesModel:Common
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        [NotMapped]
        public List<int> TechnologyIds { get; set; }
        //[NotMapped]
        //public List<TechnologyModel> TechnologyModel { get; set; }
        public ICollection<TechnologyAssociation> InquiriesTechnologies { get; set; }
        public string Experience { get; set; }
        public string Resume { get; set; }
    }
}
