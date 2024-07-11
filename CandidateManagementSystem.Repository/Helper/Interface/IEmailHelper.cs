using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagementSystem.Repository.Helper.Interface
{
    public interface IEmailHelper
    {
        //bool SendEmail(string toEmail, string subject, string body, bool isBodyHtml = true);
        bool SentEmail(List<string> toEmails, string subject, string body);
    }

}
