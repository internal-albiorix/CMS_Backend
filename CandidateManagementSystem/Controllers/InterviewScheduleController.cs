using Azure.Core;
using CandidateManagementSystem.Helper;
using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Model.Response;
using CandidateManagementSystem.Repository.Helper;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services;
using CandidateManagementSystem.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CandidateManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewScheduleController : ControllerBase
    {
        public readonly IInterviewScheduleService _interviewScheduleService;
        public readonly ICandidateHistoryService _candidateHistoryService;
        public readonly IGoogleCalanderService _googleCalanderService;
        public InterviewScheduleController(IInterviewScheduleService interviewScheduleService, ICandidateHistoryService candidateHistoryService,IGoogleCalanderService googleCalanderService)
        {
            _interviewScheduleService = interviewScheduleService;
            _candidateHistoryService = candidateHistoryService;
            _googleCalanderService = googleCalanderService;
        }

        [HttpPost("CreateInterviewSchedule")]
        //[Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> CreateInterviewSchedule([FromBody] InterviewScheduleDto interviewScheduleDto)
        {
            var result = new ResponseModel();
            try
            {
                string startDateTimeString = "2024-06-01T09:00:00";
                string endDateTimeString = "2024-06-01T10:00:00";
                DateTime startDateTime = DateTime.Parse(startDateTimeString);
                DateTime endDateTime = DateTime.Parse(endDateTimeString);

                //var createdEvent = await _googleCalanderService.CreateEventAsync(startDateTime, endDateTime, "Interview with Candidate");
                var data = await _interviewScheduleService.InsertInterviewSchedule(interviewScheduleDto);
                result.Data = data;
                result.Success = true;
                result.Message = "InterviewSchedule Created.";
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

        [HttpPut("UpdateInterviewSchedule")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> UpdateInterviewSchedule([FromBody] InterviewScheduleDto interviewScheduleDto)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _interviewScheduleService.UpdateInterviewSchedule(interviewScheduleDto, interviewScheduleDto.Id);
                if (data)
                {
                    result.Data = interviewScheduleDto;
                    result.Success = true;
                    result.Message = "InterviewSchedule Updated.";
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

        [HttpDelete("DeleteInterviewSchedule/{InterviewScheduleId}")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> DeleteInterviewSchedule(int InterviewScheduleId)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _interviewScheduleService.DeleteInterviewSchedule(InterviewScheduleId);
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

        [HttpGet("GetAllInterviewSchedule")]
        [Authorize(UserRoles.Admin, UserRoles.HR, UserRoles.Interviewer)]
        public async Task<ActionResult> GetAll()
        {
            var result = new ResponseModel();
            try
            {
                result.Data = await _interviewScheduleService.GetInterviewSchedule();
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
