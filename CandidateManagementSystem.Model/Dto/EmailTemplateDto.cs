using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagementSystem.Model.Dto
{
    public class EmailTemplateDto
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string TemplateType { get; set; }
        public string TemplateContent { get; set; }
        public bool IsActive {  get; set; }
    }
}
