using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Model.Dto
{
    public class InquiriesDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public List<int> TechnologyIds { get; set; }
        public ICollection<TechnologyAssociation> InquiriesTechnologies { get; set; }
        public string Experience { get; set; }
        public string Resume { get; set; }
    }
}
