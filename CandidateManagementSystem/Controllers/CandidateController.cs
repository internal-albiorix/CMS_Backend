using CandidateManagementSystem.Helper;
using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Model.Response;
using CandidateManagementSystem.Repository.Helper;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services;
using CandidateManagementSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CandidateManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : Controller
    {
        public readonly ICandidateService _candidateService;
        public readonly ICandidateHistoryService _candidateHistoryService;
        public readonly IUserService _userService;
        private IWebHostEnvironment _env;
        public CandidateController(ICandidateService candidateService, ICandidateHistoryService candidateHistoryService, IWebHostEnvironment env, IUserService userService)
        {
            _candidateService = candidateService;
            _candidateHistoryService = candidateHistoryService;
            _env = env;
            _userService = userService;
        }

        [HttpPost("CreateCandidate")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> CreateCandidate(IFormFile? resumeFile, [FromForm] CandidateDto candidateDto)
        {
            var result = new ResponseModel();
            try
            {
                var emailAlreadyExis = await _candidateService.CheckEmailIsAlreadyExist(candidateDto.Email, candidateDto.Id);
                if (emailAlreadyExis)
                {
                    result.Success = false;
                    result.Message = "Candidate Already Exist.";
                    result.StatusCode = HttpStatusCode.OK;
                    return Ok(result);
                }
                if (resumeFile != null)
                {
                    string fileRes = FileSave.SaveResume(resumeFile, _env);
                    if (string.IsNullOrEmpty(fileRes))
                    {
                        result.Data = null;
                        result.Message = "Error while saving resume";
                        result.StatusCode = HttpStatusCode.Forbidden;
                    }
                    candidateDto.Resume = fileRes;
                }
                var data = await _candidateService.InsertCandidate(candidateDto);
                result.Data = data;
                result.Success = true;
                result.Message = "Candidate Created.";
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

        [HttpDelete("DeleteCandidate/{CandidateId}")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> DeleteCandidate(int CandidateId)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _candidateService.DeleteCandidate(CandidateId);
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

        [HttpPut("UpdateCandidate")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> UpdateCandidate(IFormFile? resumeFile, [FromForm] CandidateDto candidateDto)
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
                    candidateDto.Resume = fileRes;
                }
                var data = await _candidateService.UpdateCandidate(candidateDto, candidateDto.Id);
                if (data)
                {
                    result.Data = candidateDto;
                    result.Success = true;
                    result.Message = "Candidate Updated.";
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

        [HttpGet("GetCandidateById/{CandidateId}")]
        [Authorize(UserRoles.Admin, UserRoles.HR, UserRoles.Interviewer)]
        public async Task<ActionResult> GetById(int CandidateId)
        {
            var result = new ResponseModel();
            try
            {
                var candidateDto = await _candidateService.GetCandidateById(CandidateId);
                result.Data = candidateDto;
                result.Success = true;
                result.Message = candidateDto == null ? "No data found" : null;
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

        [HttpGet("GetAllCandidate")]
        [Authorize(UserRoles.Admin, UserRoles.HR, UserRoles.Interviewer)]
        public async Task<ActionResult> GetAll()
        {
            var result = new ResponseModel();
            try
            {
                result.Data = await _candidateService.GetCandidate();
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

        [HttpGet("GetAppointMentsCandidate")]
        [Authorize(UserRoles.Admin, UserRoles.HR, UserRoles.Interviewer)]
        public async Task<ActionResult> GetAppointMentsCandidate()
        {
            var result = new ResponseModel();
            try
            {
                result.Data = await _candidateService.GetAppointMentsCandidate();
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

        [HttpGet("CandidateHistoryByCandidateId/{CandidateId}")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> CandidateHistoryByCandidateId(int CandidateId)
        {
            var result = new ResponseModel();
            try
            {
                var candidateHistoryDto = await _candidateHistoryService.GetCandidateHistoryByCandidateId(CandidateId);
                result.Data = candidateHistoryDto;
                result.Success = true;
                result.Message = candidateHistoryDto == null ? "No data found" : null;
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

        [HttpPost("InsertCandidateHistory")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> InsertCandidateHistory([FromBody] CandidateHistoryDto candidateHistory)
        {
            var result = new ResponseModel();
            try
            {
                var candidateHistoryDto = await _candidateHistoryService.InsertCandidateHistory(candidateHistory);
                result.Data = candidateHistoryDto;
                result.Success = true;
                result.Message = "Comment Added Successfully.";
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

        [HttpPut("UpdateCandidateHistory")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> UpdateCandidateHistory([FromBody] CandidateHistoryDto candidateHistoryDto)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _candidateHistoryService.UpdateCandidateHistory(candidateHistoryDto, candidateHistoryDto.Id);
                if (data)
                {
                    result.Data = candidateHistoryDto;
                    result.Success = true;
                    result.Message = "Comments Updated.";
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

        [HttpPost]
        [Route("GetReportCandidateData")]
        public async Task<ActionResult> GetFilteredCandidateData([FromBody] CandidateFilterDto filterDto)
        {
            var result = new ResponseModel();
            try
            {
                var candidatefilterDto = await _candidateService.GetFilteredCandidateData(filterDto);
                result.Data = candidatefilterDto;
                result.Success = true;
                result.Message = "candidate fetch";
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

        [HttpGet("RejectInterviewSchedule/{CandidateId}")]
        [Authorize(UserRoles.Interviewer)]
        public async Task<ActionResult> RejectInterviewSchedule(int CandidateId)
        {
                var result = new ResponseModel();
            try
            {
                var data = await _candidateService.RejectInterviewSchedule(CandidateId);
                if (data)
                {
                    result.Success = true;
                    result.Message = "Schedule Succesfully Rejected";
                    result.StatusCode = HttpStatusCode.OK;
                }
                else
                {

                    result.StatusCode = HttpStatusCode.BadRequest;
                    result.Message = "something went wrong";
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
