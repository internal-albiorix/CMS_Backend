using CandidateManagementSystem.Model.Response;
using CandidateManagementSystem.Services.Interface;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Repository.Helper;

namespace CandidateManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeFrameController : ControllerBase
    {
        private readonly ITimeFrameService _timeFrameService;

        public TimeFrameController(ITimeFrameService timeFrameService)
        {
            _timeFrameService = timeFrameService;
        }
        [HttpGet("GetAllTimeFrame")]
       // [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> GetAllTimeframe()
        {

            var result = new ResponseModel();
            try
            {
                result.Data = await _timeFrameService.GetTimeFrame();
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
