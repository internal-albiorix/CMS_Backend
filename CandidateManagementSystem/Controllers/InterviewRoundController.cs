using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Model.Response;
using CandidateManagementSystem.Repository.Helper;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net;
using System.Net.Security;

namespace CandidateManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewRoundController : ControllerBase
    {
        private readonly IInterviewRoundService _InterviewRoundService;
        public InterviewRoundController(IInterviewRoundService InterviewRoundService)
        {
            _InterviewRoundService = InterviewRoundService;
        }
       
        [HttpPost("CreateInterviewRound")]
        [Authorize(UserRoles.Admin)]

        public async Task<ActionResult> CreateInterviewRound([FromBody] InterviewRoundDto interviewRoundDto)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _InterviewRoundService.InsertInterviewRound(interviewRoundDto);
                result.Data = data;
                result.Success = true;
                result.Message = "InterviewRound Created.";
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

        [HttpDelete("DeleteInterviewRound/{InterviewRoundId}")]
        [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> DeleteInterviewRound(int InterviewRoundId)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _InterviewRoundService.DeleteInterviewRound(InterviewRoundId);
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

        [HttpPut("UpdateInterviewRound")]
        [Authorize(UserRoles.Admin)]

        public async Task<ActionResult> UpdateInterviewRound([FromBody] InterviewRoundModel InterviewRoundModel)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _InterviewRoundService.UpdateInterviewRound(InterviewRoundModel, InterviewRoundModel.Id);
                if (data)
                {
                    result.Data = InterviewRoundModel;
                    result.Message = "InterviewRound Updated.";
                    result.Success = true;
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
        
        [HttpGet("GetInterviewRoundById/{InterviewRoundId}")]
        [Authorize(UserRoles.Admin)]

        public async Task<ActionResult> GetById(int InterviewRoundId)
        { 
            var result = new ResponseModel();
            try
            {
                var InterviewRoundDto = await _InterviewRoundService.GetInterviewRoundById(InterviewRoundId);
                result.Data = CMSAutoMapper.Mapper.Map<InterviewRoundModel>(InterviewRoundDto);
                result.Success = true;
                result.Message = InterviewRoundDto == null ? "No data found" : null;
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

        [HttpGet("GetAllInterviewRound")]

        public async Task<ActionResult> GetAll()
        {
            var result = new ResponseModel();
            try
            {
        
                var InterviewRoundDto = await _InterviewRoundService.GetInterviewRound();
                result.Data = CMSAutoMapper.Mapper.Map<IEnumerable<InterviewRoundModel>>(InterviewRoundDto);
                result.Success = true;
                result.Message = InterviewRoundDto == null ? "No data found" : null;
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
