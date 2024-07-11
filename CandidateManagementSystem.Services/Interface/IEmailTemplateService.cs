using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagementSystem.Model.Dto;

namespace CandidateManagementSystem.Services.Interface
{
    public interface IEmailTemplateService
    {
        Task<IEnumerable<EmailTemplateDto>> GetEmailTemplate();
        Task<EmailTemplateDto> GetEmailTemplateById(int id);
        Task<bool> DeleteEmailTemplate(int id);
        Task<EmailTemplateDto> InsertEmailTemaplate(EmailTemplateDto emailTemplateDto);
        Task<bool> UpdateEmailTemplate(EmailTemplateDto emailTemplateDto, int id);
        Task<string> GetTemplateContentByTypeAsync(string templateType);
        Task<string> RenderEmailTemplate(List<CurrentOpeningDto> jobOpenings, string template);
        Task<string> SentEmailCurrentOpening(string templateType);
        Task<IEnumerable<EmailLogDto>> GetEmailLog();

    }
}
