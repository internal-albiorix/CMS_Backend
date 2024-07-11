using CandidateManagementSystem.Helper;
using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.Response;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services;
using System.Net;
using CandidateManagementSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using CandidateManagementSystem.Repository.Helper;


namespace CandidateManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiriesController : ControllerBase
    {
        private readonly IInquiriesService _inquiriesService;
        private IWebHostEnvironment _env;
        public InquiriesController(IInquiriesService inquiriesService, IWebHostEnvironment env)
        {
          _inquiriesService = inquiriesService;
            _env = env;
        }
        [HttpPost("CreateInquiries")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> CreateInquiries(IFormFile? resumeFile, [FromForm] InquiriesDto inquiriesDto)
        {
            var result = new ResponseModel();
            try
            {
                if (resumeFile != null)
                {
                    string fileRes = FileSave.SaveResume(resumeFile, _env);
                    if (string.IsNullOrEmpty(fileRes))
                    {
                        result.Data = null;
                        result.Message = "Error while saving resume";
                        result.StatusCode = HttpStatusCode.Forbidden;
                    }
                    inquiriesDto.Resume = fileRes;
                }
                var data = await _inquiriesService.InsertInquiries(inquiriesDto);
                result.Data = data;
                result.Success = true;
                result.Message = "Inquiries Created.";
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
        [HttpGet("GetAllInquiries")]
        [Authorize(UserRoles.Admin, UserRoles.HR, UserRoles.Interviewer)]
        public async Task<ActionResult> GetAll()
        {
            var result = new ResponseModel();
            try
            {
                result.Data = await _inquiriesService.GetInquiries();
                result.Success = true;
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

        [HttpGet("GetInquiriesById/{InquiriesId}")]
        [Authorize(UserRoles.Admin, UserRoles.HR, UserRoles.Interviewer)]
        public async Task<ActionResult> GetById(int InquiriesId)
        {
            var result = new ResponseModel();
            try
            {
                var inquiriesDto = await _inquiriesService.GetInquiriesById(InquiriesId);
                result.Data = inquiriesDto;
                result.Success = true;
                result.Message = inquiriesDto == null ? "No data found" : null;
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

        [HttpPut("UpdateInquiries")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> UpdateInquiries(IFormFile? resumeFile, [FromForm] InquiriesDto inquiriesDto)
        {
            var result = new ResponseModel();
            try
            {
                if (resumeFile != null)
                {
                    string fileRes = FileSave.SaveResume(resumeFile, _env);
                    if (string.IsNullOrEmpty(fileRes))
                    {
                        result.Data = null;
                        result.Message = "Error while saving resume";
                        result.StatusCode = HttpStatusCode.Forbidden;
                    }
                    inquiriesDto.Resume = fileRes;
                }
                var data = await _inquiriesService.UpdateInquiries(inquiriesDto, inquiriesDto.Id);
                if (data)
                {
                    result.Data = inquiriesDto;
                    result.Success = true;
                    result.Message = "Inquiries Updated.";
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

        [HttpDelete("DeleteInquiries/{InquiriesId}")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> DeleteInquiries(int InquiriesId)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _inquiriesService.DeleteInquiries(InquiriesId);
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
    }
}
