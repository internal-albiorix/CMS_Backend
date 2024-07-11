using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystem.Repository
{
    public class EmailTemplateRepository:IEmailTemplateRepository
    {
        private readonly ApplicationDbContext _context;
        public EmailTemplateRepository(ApplicationDbContext context) { 
            _context = context;
        }
        public async Task<String> GetTemplateContentByTypeAsync(string templateType)
        {
            try
            {
                return await _context.EmailTemplateModels
                              .Where(et => et.TemplateType == templateType && et.IsActive)
                              .Select(et => et.TemplateContent)
                              .FirstOrDefaultAsync();
                              
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving data.", ex);
            }
        }

        public async Task<IEnumerable<EmailLogModel>> GetAllEmailLog()
        {
            try
            {
                return await _context.emailLogs.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving data.", ex);
            }
        }
    }
}
