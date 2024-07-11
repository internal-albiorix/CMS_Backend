using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Model.Response;
using CandidateManagementSystem.Repository.Helper;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CandidateManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologyController : ControllerBase
    {
        private readonly ITechnologyService _technologyService;
        public TechnologyController(ITechnologyService technologyService)
        {
            _technologyService = technologyService;
        }

        [HttpPost("CreateTechnology")]
        [Authorize(UserRoles.Admin)]

        public async Task<ActionResult> CreateTechnology([FromBody] TechnologyDto technologyDto)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _technologyService.InsertTechnology(technologyDto);
                result.Data = data;
                result.Success = true;
                result.Message = "Technology Created.";
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

        [HttpDelete("DeleteTechnology/{TechnologyId}")]
        [Authorize(UserRoles.Admin)]

        public async Task<ActionResult> DeleteTechnology(int technologyId)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _technologyService.DeleteTechnology(technologyId);
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

        [HttpPut("UpdateTechnology")]
        [Authorize(UserRoles.Admin)]

        public async Task<ActionResult> UpdateTechnology([FromBody] TechnologyDto technologyDto)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _technologyService.UpdateTechnology(CMSAutoMapper.Mapper.Map<TechnologyModel>(technologyDto), technologyDto.Id);
                if (data)
                {
                    result.Data = technologyDto;
                    result.Message = "Technology Updated.";
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

        [HttpGet("GetTechnologyById/{TechnologyId}")]
        [Authorize(UserRoles.Admin)]

        public async Task<ActionResult> GetById(int technologyId)
        {
            var result = new ResponseModel();
            try
            {
                var technologyDto = await _technologyService.GetTechnologyById(technologyId);
                result.Data = CMSAutoMapper.Mapper.Map<TechnologyModel>(technologyDto);
                result.Success = true;
                result.Message = technologyDto == null ? "No data found" : null;
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

        [HttpGet("GetAllTechnology")]

        public async Task<ActionResult> GetAll()
        {
            var result = new ResponseModel();
            try
            {

                var technologyDto = await _technologyService.GetTechnology();
                result.Data = CMSAutoMapper.Mapper.Map<IEnumerable<TechnologyModel>>(technologyDto);
                result.Success = true;
                result.Message = technologyDto == null ? "No data found" : null;
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
