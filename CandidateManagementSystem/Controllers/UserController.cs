using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Model.Request;
using CandidateManagementSystem.Model.Response;
using CandidateManagementSystem.Repository.Helper;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services;
using CandidateManagementSystem.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CandidateManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("CreateUser")]
        [Authorize(UserRoles.Admin,UserRoles.HR)]
        public async Task<ActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var result = new ResponseModel();
            try
            {
                var emailAlreadyExis = await _userService.CheckEmailIsAlreadyExist(userDto.Email, userDto.Id);
                if (emailAlreadyExis)
                {
                    result.Success = true;
                    result.Message = "User Email Already Exist.";
                    result.StatusCode = HttpStatusCode.OK;
                    return Ok(result);
                }
                var data = await _userService.InsertUser(userDto);
                result.Data = data;
                result.Success = true;
                result.Message = "User Created.";
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

        [HttpDelete("DeleteUser/{UserId}")]
        [Authorize(UserRoles.Admin)]
        public async Task<ActionResult> DeleteUser(int UserId)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _userService.DeleteUser(UserId);
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

        [HttpPut("UpdateUser")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            var result = new ResponseModel();
            try
            {
                var emailAlreadyExis = await _userService.CheckEmailIsAlreadyExist(userDto.Email, userDto.Id);
                if (emailAlreadyExis)
                {
                    result.Success = true;
                    result.Message = "User Email Already Exist.";
                    result.StatusCode = HttpStatusCode.OK;
                    return Ok(result);
                }

                var data = await _userService.UpdateUser(CMSAutoMapper.Mapper.Map<UserModel>(userDto), userDto.Id);
                if (data)
                {
                    result.Data = userDto;
                    result.Success = true;
                    result.Message = "User Updated.";
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

        [HttpGet("GetUserById/{UserId}")]
        [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> GetById(int UserId)
        {
            var result = new ResponseModel();
            try
            {
                var userDto = await _userService.GetUserById(UserId);
                result.Data = userDto;
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
        [HttpGet("GetAllUsers")]
       [Authorize(UserRoles.Admin, UserRoles.HR)]
        public async Task<ActionResult> GetAll()
        {
            var result = new ResponseModel();
            try
            {
                var data = await _userService.GetUsers();
                result.Data = data;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest model)
        {
            var result = new ResponseModel();
            try
            {
                result = await _userService.Authenticate(model);
                if (result.Data == null)
                {
                    result.StatusCode = HttpStatusCode.Unauthorized;
                    result.Message = "Email or password not correct..!! Please try again.";
                    return Unauthorized(result);
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

        [HttpPut("ChangedPassword")]
        [Authorize(UserRoles.Admin,UserRoles.HR,UserRoles.Interviewer)]
        public async Task<IActionResult> ChangedPassword([FromBody] ChangePasswordDto changedPasswordDto)
        {
            var result = new ResponseModel();
            try
            {
                var data = await _userService.ChangedPassword(changedPasswordDto);
                if (data)
                {
                    result.Data = changedPasswordDto;
                    result.Success = true;
                    result.Message = "Password Updated Successfully.";
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
                throw ex;
            }

        } 
       
        [Authorize]
        [HttpPost("ValidateToken")]
        public async Task<IActionResult> ValidateToken()
        {
            ResponseModel response = new ResponseModel();
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Success = true;
            response.Message = "Token validated";
            return Ok(response);
        }
        [HttpPut("SentEmailForgotPassword")]
         public async Task<IActionResult> SentEmailForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var result = new ResponseModel();
            try
            {
                var existingUser = await _userService.GetByEmailAsync(forgotPasswordDto.Email);
                if (existingUser == null)
                { 
                    
                    result.Message = "User Not Exists";
                    result.StatusCode = HttpStatusCode.NotFound;
                    return Ok(result);
                }
                var data = await _userService.SentEmailForgotPassword(existingUser);
                if (data)
                {
                    result.Success = true;
                    result.Message = "Email sent Successfully.";
                    result.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    result.Data = null;
                    result.Message = "Failed to sent the mail";
                    result.StatusCode = HttpStatusCode.NotFound;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var result = new ResponseModel();
            try
            {
                var forgotPasswordEntry = await _userService.GetForgotPasswordEntryByTokenAsync(resetPasswordDto.Token);
                if (forgotPasswordEntry == null || forgotPasswordEntry.TokenExpires < DateTime.UtcNow)
                {
                    result.Success = false;
                    result.Message = "Invalid or expired token";
                    result.StatusCode = HttpStatusCode.BadRequest;
                    return Ok(result);
                }

                var success = await _userService.ResetPasswordAsync(forgotPasswordEntry.UserId, resetPasswordDto.Password);
                if (success)
                {
                    result.Success = true;
                    result.Message = "Password has been reset successfully";
                    result.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Failed to reset password";
                    result.StatusCode = HttpStatusCode.InternalServerError;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                result.Success = false;
                result.Message = "An error occurred.";
                result.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, result);
            }
        }

    }
}
