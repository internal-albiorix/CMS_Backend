using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagementSystem.Model.Model
{
    public class TechnologyAssociation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public UserModel User { get; set; }
        [ForeignKey("Candidate")]
        public int? CandidateId { get; set; }
        public CandidateModel Candidate { get; set; }
        [ForeignKey("FeedBackModel")]
        public int? FeedbackId { get; set; }
        public FeedBackModel Feedback { get; set; }
        [ForeignKey("ReferEmployee")]
        public int? ReferEmployeeId { get; set; }
        public ReferEmployeeModel ReferEmployee { get; set; }
        [ForeignKey("Inquiries")]
        public int? InquiriesId { get; set; }
        public InquiriesModel Inquiries { get; set; }

        [ForeignKey("Technology")]
        public int TechnologyId { get; set; }
        public TechnologyModel Technology { get; set; }
    }
}
