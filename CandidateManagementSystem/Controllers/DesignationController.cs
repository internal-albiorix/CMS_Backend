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

    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;
        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }

        [HttpPost("CreateDesignation")]
        [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> CreateDesignation([FromBody] DesignationDto designationDto)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _designationService.InsertDesignation(designationDto);
                result.Data = data;
                result.Success = true;
                result.Message = "Designation Created.";
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

        [HttpDelete("DeleteDesignation/{DesignationId}")]
        [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> DeleteDesignation(int designationId)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _designationService.DeleteDesignation(designationId);
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

        [HttpPut("UpdateDesignation")]
        [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> UpdateDesignation([FromBody] DesignationModel designationModel)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _designationService.UpdateDesignation(designationModel, designationModel.Id);
                if (data)
                {
                    result.Data = designationModel;
                    result.Success = true;
                    result.Message = "Designation Updated.";
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

        [HttpGet("GetDesignationById/{DesignationId}")]
        [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> GetById(int designationId)
        {
            var result = new ResponseModel();
            try
            {
                var designationDto = await _designationService.GetDesignationById(designationId);
                result.Data = designationDto;
                result.Success = true;
                result.Message = designationDto == null ? "No data found" : null;
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

        [HttpGet("GetAllDesignation")]
        [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> GetAll()
        {
            var result = new ResponseModel();
            try
            {
                var designationDto = await _designationService.GetDesignation();
                result.Data = designationDto;
                result.Success = true;
                result.Message = designationDto == null ? "No data found" : null;
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
