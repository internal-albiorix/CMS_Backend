using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.Response;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CandidateManagementSystem.Repository.Helper;
using CandidateManagementSystem.Services.Interface;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CandidateManagementSystem.Repository.Helper.Interface;

namespace CandidateManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplateController : ControllerBase
    {
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ICurrentOpeningService _CurrentOpeningService;
        private readonly IEmailHelper _emailHelper;
        public EmailTemplateController(IEmailTemplateService emailTemplateService, ICurrentOpeningService currentOpeningService, IEmailHelper emailHelper)
        {
            _emailTemplateService = emailTemplateService;
            _CurrentOpeningService = currentOpeningService;
            _emailHelper = emailHelper;

        }
        [HttpPost("CreateEmailTemplate")]
        [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> CreateEmailTemplate([FromBody] EmailTemplateDto emailTemplateDto)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _emailTemplateService.InsertEmailTemaplate(emailTemplateDto);
                result.Data = data;
                result.Success = true;
                result.Message = "Template Created.";
                result.StatusCode = HttpStatusCode.OK;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return BadRequest(result);
            }
        }

        [HttpPut("UpdateEmailTemplate")]
        [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> UpdateEmailTemplate([FromBody] EmailTemplateDto emailTemplateDto)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _emailTemplateService.UpdateEmailTemplate(emailTemplateDto, emailTemplateDto.Id);
                if (data)
                {
                    result.Data = emailTemplateDto;
                    result.Success = true;
                    result.Message = "EmailTemplate Updated.";
                    result.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    result.Data = null;
                    result.Message = "No data found";
                    result.StatusCode = HttpStatusCode.NotFound;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return BadRequest(result);
            }
        }
        [HttpGet("GetEmailTemplateById/{EmailTemplateId}")]
        [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> GetById(int emailtemplateId)
        {
            var result = new ResponseModel();
            try
            {
                var emailtemplateDto = await _emailTemplateService.GetEmailTemplateById(emailtemplateId);
                result.Data = emailtemplateDto;
                result.Success = true;
                result.Message = emailtemplateDto == null ? "No data found" : null;
                result.StatusCode = HttpStatusCode.OK;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return BadRequest(result);
            }
        }

        [HttpGet("GetAllEmailTemplate")]
        [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> GetAll()
        {
            var result = new ResponseModel();
            try
            {
                var emailtemplateDto = await _emailTemplateService.GetEmailTemplate();
                result.Data = emailtemplateDto;
                result.Success = true;
                result.Message = emailtemplateDto == null ? "No data found" : null;
                result.StatusCode = HttpStatusCode.OK;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return BadRequest(result);
            }
        }
        [HttpDelete("DeleteEmailTemplate/{EmailTemplateId}")]
        [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> DeleteEmailTemplate(int emailtemplateId)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _emailTemplateService.DeleteEmailTemplate(emailtemplateId);
                if (data)
                {
                    result.Data = data;
                    result.Success = true;
                    result.StatusCode = HttpStatusCode.OK;
                    result.Message = "Record Deleted";
                }
                else
                {
                    result.Data = data;
                    result.Message = "No data found";
                    result.StatusCode = HttpStatusCode.NotFound;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return BadRequest(result);
            }
        }
        [HttpGet("SentEmailCurrentOpening/{TemplateType}")]
        [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> SentEmailCurrentOpening(string templateType)
        {
            var result = new ResponseModel();
            //Get template
            try
            {
                var message = await _emailTemplateService.SentEmailCurrentOpening(templateType);
                if (string.IsNullOrEmpty(message))
                {
                    result.Success = true;
                    result.StatusCode = HttpStatusCode.OK;
                    result.Message = "Email sent successfully.";
                }
                else
                {
                    result.Success = false;
                    result.StatusCode = HttpStatusCode.NotFound;
                    result.Message = message;
                   
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return BadRequest(result);
            }
        }
        [HttpGet("GetAllEmailLog")]
        //[Authorize(UserRoles.Admin)]
        public async Task<ActionResult> GetAllEmailLog()
        {
            var result = new ResponseModel();
            try
            {
                var emaillogDto = await _emailTemplateService.GetEmailLog();
                result.Data = emaillogDto;
                result.Success = true;
                result.Message = emaillogDto == null ? "No data found" : null;
                result.StatusCode = HttpStatusCode.OK;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Message = ex.Message;
                return BadRequest(result);
            }
        }

    }
}
