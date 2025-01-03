using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Helper.Interface;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;
using Microsoft.Extensions.Primitives;

namespace CandidateManagementSystem.Services
{
    public class EmailTemplateService:IEmailTemplateService
    {
        private readonly ICandidateManagementRepository<EmailTemplateModel> _repo;
        private readonly IEmailTemplateRepository _emailtemplaterepo;
        private readonly ICurrentOpeningService _currentOpeningService;
        private readonly IEmailHelper _emailHelper;

        public EmailTemplateService(ICandidateManagementRepository<EmailTemplateModel> repo, 
            IEmailTemplateRepository emailtemplaterepo, ICurrentOpeningService currentOpeningService
            ,IEmailHelper emailHelper)  
        {
            _repo = repo;
            _emailtemplaterepo = emailtemplaterepo;
            _currentOpeningService = currentOpeningService;
            _emailHelper = emailHelper;

        }
        public async Task<IEnumerable<EmailTemplateDto>> GetEmailTemplate()
        {
            try
            {
                var resultModel = await _repo.GetAll();
                var resultDto = CMSAutoMapper.Mapper.Map<IEnumerable<EmailTemplateDto>>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<EmailTemplateDto> GetEmailTemplateById(int id)
        {
            try
            {
               
                var resultModel = await _repo.GetByIdAsync(id);
                var resultDto = CMSAutoMapper.Mapper.Map<EmailTemplateDto>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> GetTemplateContentByTypeAsync(string templateType)
        {
            try
            {
                return await _emailtemplaterepo.GetTemplateContentByTypeAsync(templateType);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task<bool> DeleteEmailTemplate(int id)
        {
            try
            {
                return _repo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmailTemplateDto> InsertEmailTemaplate(EmailTemplateDto emailTemplateDto)
        {
            try
            {
                var emailTemplateModel = CMSAutoMapper.Mapper.Map<EmailTemplateModel>(emailTemplateDto);
                emailTemplateModel.InsertedBy = CurrentUser.User.FullName;
                emailTemplateModel.InsertedDate = DateTime.Now;
                var resultModel = await _repo.PostAsync(emailTemplateModel);
                var resultDto = CMSAutoMapper.Mapper.Map<EmailTemplateDto>(resultModel);

                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateEmailTemplate(EmailTemplateDto emailTemplateDto,int id)
        {
            try
            {
                var existingemailtemplate = await _repo.GetByIdAsync(id);
                existingemailtemplate.TemplateName = emailTemplateDto.TemplateName;
                existingemailtemplate.TemplateType = emailTemplateDto.TemplateType;
                existingemailtemplate.TemplateContent = emailTemplateDto.TemplateContent;
                existingemailtemplate.IsActive= emailTemplateDto.IsActive;
                existingemailtemplate.UpdatedBy = CurrentUser.User.FullName;
                existingemailtemplate.UpdatedDate = DateTime.Now;
                return await _repo.PutAsync(existingemailtemplate, id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> SentEmailCurrentOpening(string templateType)
        {
            var template = await GetTemplateContentByTypeAsync(templateType);
            if (string.IsNullOrEmpty(template))
                return "Template not found";
            var currentOpenings = await _currentOpeningService.GetCurrentOpening();
            if (currentOpenings == null || !currentOpenings.Any())
                return "No current openings found.";
            var emailTemplate = await RenderEmailTemplate(currentOpenings, template);
            List<string> testEmails = new List<string>
                    {
                        "voqueugonefreu-7525@yopmail.com",
                        "fifasatrobra-1099@yopmail.com"
                    };
            var subject = "Current Opening";
            var body = emailTemplate;
            var mailSent = _emailHelper.SentEmail(testEmails, subject, body);
            if (!mailSent)
                return "Failed to send email.";
            return "";
        }
        public async Task<string> RenderEmailTemplate(List<CurrentOpeningDto> jobOpenings, string template)
        {
            try
            {
            string tableTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplate", "CurrentOpeningTableTemplate.html");
            string tableTemplate = await File.ReadAllTextAsync(tableTemplatePath);

            StringBuilder tableRows = new StringBuilder();
            var i=1;
            foreach(var opening in jobOpenings)
            {
                tableRows.AppendLine($"<tr><td>{i}</td><td>{opening.TechnologyName}</td><td>{opening.Experience}</td><td>{opening.Noofopening}</td></tr>");
                    i++;
            }

            tableTemplate = tableTemplate.Replace("$$tableData$$", tableRows.ToString());

            // Combine the table template with the database fetch template
            string emailTemplate = template.Replace("$$tableData$$", tableTemplate);

                return emailTemplate;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<EmailLogDto>> GetEmailLog()
        {
            try
            {
                var resultModel = await _emailtemplaterepo.GetAllEmailLog();
                var resultDto = CMSAutoMapper.Mapper.Map<IEnumerable<EmailLogDto>>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
