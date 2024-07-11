using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Model.Response;
using CandidateManagementSystem.Repository.Helper;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services;
using CandidateManagementSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CandidateManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CurrentOpeningController : ControllerBase
    {
        private readonly ICurrentOpeningService _CurrentOpeningService;
       
        public CurrentOpeningController(ICurrentOpeningService CurrentOpeningService)
        {
            _CurrentOpeningService = CurrentOpeningService;
        }

        [HttpPost("CreateCurrentOpening")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> CreateCurrentOpening([FromBody] CurrentOpeningDto CurrentOpeningModel)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _CurrentOpeningService.InsertCurrentOpening(CurrentOpeningModel);
                result.Data = data;
                result.Success = true;
                result.Message = "CurrentOpening Created.";
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

        [HttpDelete("DeleteCurrentOpening/{CurrentOpeningId}")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> DeleteCurrentOpening(int CurrentOpeningId)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _CurrentOpeningService.DeleteCurrentOpening(CurrentOpeningId);
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

        [HttpPut("UpdateCurrentOpening")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> UpdateCurrentOpening([FromBody] CurrentOpeningDto CurrentOpeningDto)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _CurrentOpeningService.UpdateCurrentOpening(CMSAutoMapper.Mapper.Map<CurrentOpeningModel>(CurrentOpeningDto), CurrentOpeningDto.Id);
                if (data)
                {
                    result.Data = CurrentOpeningDto;
                    result.Success = true;
                    result.Message = "CurrentOpening Updated.";
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

        [HttpGet("GetCurrentOpeningById/{CurrentOpeningId}")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> GetById(int CurrentOpeningId)
        {
            var result = new ResponseModel();
            try
            {
                var currentOpeningDto = await _CurrentOpeningService.GetCurrentOpeningById(CurrentOpeningId);
                result.Data = currentOpeningDto;
                result.Success = true;
                result.Message = currentOpeningDto == null ? "No data found" : null;
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

      

        [HttpGet("GetAllCurrentOpening")]
        public async Task<ActionResult> GetAll()
        {
            var result = new ResponseModel();
            try
            {
                var data = await _CurrentOpeningService.GetCurrentOpening();
                result.Data = data;
                result.Success = true;
                result.Message = data == null ? "No data found" : null;
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
