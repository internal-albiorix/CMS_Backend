using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagementSystem.Model.Dto
{
    public class EmailLogDto
    {
        public int Id { get; set; }
        public string Recipient { get; set; }
        public DateTime SentDate { get; set; }
        public string Status { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
