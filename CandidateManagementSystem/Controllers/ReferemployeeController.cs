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

using Newtonsoft.Json;
using System.Net;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CandidateManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferemployeeController : ControllerBase
    {
        private IWebHostEnvironment _env;
        private readonly IReferemployeeService _referemployeeService;
        public ReferemployeeController(IReferemployeeService referemployeeService, IWebHostEnvironment env)
        {
            _env = env;
            _referemployeeService = referemployeeService;
        }

        [HttpPost("CreateReferemployee")]
        public async Task<ActionResult> CreateReferemployee(IFormFile resumeFile, [FromForm] ReferEmployeeDto referemployeeDto)
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
                    referemployeeDto.Resume = fileRes;
                }
                var data = await _referemployeeService.InsertReferEmployee(referemployeeDto);
                result.Data = data;
                result.Success = true;
                result.Message = "Refer Employee Created.";
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

        [HttpDelete("DeleteReferemployee/{ReferemployeeId}")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> DeleteReferemployee(int ReferemployeeId)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _referemployeeService.DeleteReferEmployee(ReferemployeeId);
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

        [HttpPut("UpdateReferemployee")]
      //  [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> UpdateReferemployee(IFormFile? resumeFile, [FromForm] ReferEmployeeDto referEmployeeDto)
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
                    referEmployeeDto.Resume = fileRes;
                }

                var data = await _referemployeeService.UpdateReferEmployee(referEmployeeDto, referEmployeeDto.Id);
                if (data)
                {
                    result.Data = referEmployeeDto;
                    result.Success = true;
                    result.Message = "Refer employee updated.";
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

        [HttpGet("GetReferemployeeById/{ReferemployeeId}")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> GetById(int ReferemployeeId)
        {
            var result = new ResponseModel();
            try
            {
                var referEmployeeDto = await _referemployeeService.GetReferEmployeeById(ReferemployeeId);
                result.Data = CMSAutoMapper.Mapper.Map<ReferEmployeeModel>(referEmployeeDto);
                result.Success = true;
                result.Message = referEmployeeDto == null ? "No data found" : null;
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

        [HttpGet("GetAllReferemployee")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> GetAll()
        {
            var result = new ResponseModel();
            try
            {
                var data = await _referemployeeService.GetReferEmployee();
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

        [HttpGet("DownloadResume/{fileName}")]
        [Authorize(UserRoles.Admin, UserRoles.HR,UserRoles.Interviewer)]
        public IActionResult Download(string fileName)
        {
          
            try
            {
                string path = _env.ContentRootPath + "\\files\\resume";
                var filePath = Path.Combine(path, fileName);
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
              
               return File(fileBytes, "application/octet-stream", fileName);
              
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error downloading file: {ex.Message}");
            }
        }
    }
}
