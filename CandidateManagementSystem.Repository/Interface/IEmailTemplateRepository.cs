using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Repository.Interface
{
    public interface IEmailTemplateRepository
    {
        Task<string> GetTemplateContentByTypeAsync(string templateType);
        Task<IEnumerable<EmailLogModel>> GetAllEmailLog();
    }
}
