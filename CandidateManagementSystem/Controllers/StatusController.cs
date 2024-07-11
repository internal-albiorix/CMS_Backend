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

namespace CandidateManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _StatusService;
        public StatusController(IStatusService StatusService)
        {
            _StatusService = StatusService;
        }
        [Authorize(UserRoles.Admin)]
        [HttpPost("CreateStatus")]
        public async Task<ActionResult> CreateStatus([FromBody] StatusDto StatusDto)
        {
            var result = new ResponseModel();
            try
            {

                var data = await _StatusService.InsertStatus(StatusDto);
                result.Data = data;
                result.Success = true;
                result.Message = "Status Created.";
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
        [Authorize(UserRoles.Admin)]
        [HttpDelete("DeleteStatus/{StatusId}")]
        public async Task<ActionResult> DeleteStatus(int StatusId)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _StatusService.DeleteStatus(StatusId);
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
        [Authorize(UserRoles.Admin)]
        [HttpPut("UpdateStatus")]
        public async Task<ActionResult> UpdateStatus([FromBody] StatusDto statusDto)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _StatusService.UpdateStatus(CMSAutoMapper.Mapper.Map<StatusModel>(statusDto), statusDto.Id);
                if (data)
                {
                    result.Data = statusDto;
                    result.Message = "Status Updated.";
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
        [Authorize(UserRoles.Admin)]
        [HttpGet("GetStatusById/{StatusId}")]
        public async Task<ActionResult> GetById(int StatusId)
        {
            var result = new ResponseModel();
            try
            {
                var statusDto = await _StatusService.GetStatusById(StatusId);
                result.Data = statusDto;
                result.Success = true;
                result.Message = statusDto == null ? "No data found" : null;
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
        [Authorize(UserRoles.Admin,UserRoles.HR,UserRoles.Interviewer)]
        [HttpGet("GetAllStatus")]
        public async Task<ActionResult> GetAll()
        {
            var result = new ResponseModel();
            try
            {
                var statusDto = await _StatusService.GetStatus();
                result.Data = statusDto;
                result.Success = true;
                result.Message = statusDto == null ? "No data found" : null;
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
