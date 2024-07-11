using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Model.Response;
using CandidateManagementSystem.Repository.Helper;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CandidateManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICandidateService _candidateService;
        private readonly IInterviewScheduleService _interviewScheduleService;
        public DashboardController(IUserService userService, ICandidateService candidateService, IInterviewScheduleService interviewScheduleService)
        {
            _userService = userService;
            _candidateService = candidateService;
            _interviewScheduleService = interviewScheduleService;
        }

        [HttpGet("GetInterviewerList")]
        [Authorize(UserRoles.Admin, UserRoles.HR, UserRoles.Interviewer)]
        public async Task<ActionResult> GetInterviewerList()
        {
            var result = new ResponseModel();
            try
            {
                result.Data = await _userService.GetInterviewerList();
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

        [HttpGet("GetCandidateCount")]
        [Authorize(UserRoles.Admin, UserRoles.HR, UserRoles.Interviewer)]
        public async Task<ActionResult> GetCandidateCount()
        {
            var result = new ResponseModel();
            try
            {
                result.Data = await _userService.GetCandidateCount();
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

        [HttpGet("GetCandidateCountByStatus/{statusNames}")]
        [Authorize(UserRoles.Admin, UserRoles.HR, UserRoles.Interviewer)]
        public async Task<ActionResult> GetCandidateCountByStatus(string statusNames)
        {
            var result = new ResponseModel();
            try
            {
                var listOfStatus = statusNames.Split(",").ToList();
                result.Data = await _userService.GetCandidateCountByStatus(listOfStatus);
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


        [HttpGet("GetLatestCandidateList")]
        [Authorize(UserRoles.Admin, UserRoles.HR, UserRoles.Interviewer)]
        public async Task<ActionResult> GetLatestCandidateList()
        {
            var result = new ResponseModel();
            try
            {
                result.Data = await _candidateService.GetLatestCandidate();
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

        [HttpGet("GetUpcomingInterviews")]
        [Authorize(UserRoles.Admin, UserRoles.HR, UserRoles.Interviewer)]
        public async Task<ActionResult> GetUpcomingInterviews()
        {
            var result = new ResponseModel();
            try
            {
                result.Data = await _interviewScheduleService.GetUpcomingInterviews();
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

        [HttpGet("GetCandidatesForChart")]
        [Authorize(UserRoles.Admin, UserRoles.HR, UserRoles.Interviewer)]
        public async Task<ActionResult> GetCandidatesForChart()
        {
            var result = new ResponseModel();
            try
            {
                result.Data = await _candidateService.GetCandidateForChart();
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
    }
}
