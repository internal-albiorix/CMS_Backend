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
    public class FeedbackController : ControllerBase
    {
        public readonly IFeedBackService _feedBackService;
        public FeedbackController(IFeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }

        [HttpPost("InsertCandidateFeedBack")]
        [Authorize(UserRoles.Admin, UserRoles.HR,UserRoles.Interviewer)]
        public async Task<ActionResult> InsertCandidateFeedBack([FromBody] FeedBackDto feedBackDto)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _feedBackService.InsertCandidateFeedBack(feedBackDto);
                result.Data = CMSAutoMapper.Mapper.Map<FeedBackDto>(data);
                result.Success = true;
                result.Message = "FeedBack Inserted.";
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

        [HttpGet("GetFeedBackByCandidateId/{CandidateId}")]
        [Authorize(UserRoles.Admin, UserRoles.HR, UserRoles.Interviewer)]
        public async Task<ActionResult> GetFeedBackByCandidateId(int CandidateId)
        {
            var result = new ResponseModel();
            try
            {
                var feedbacks = await _feedBackService.GetFeedBackByCandidateId(CandidateId);
                result.Data = feedbacks;
                result.Success = true;
                result.Message = feedbacks == null ? "No data found" : null;
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
