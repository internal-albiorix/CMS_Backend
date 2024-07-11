using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Model.Request;
using CandidateManagementSystem.Model.Response;
using CandidateManagementSystem.Repository;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using CandidateManagementSystem.Helper;
using CandidateManagementSystem.Repository.Helper.Interface;

namespace CandidateManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly ICandidateManagementRepository<UserModel> _repo;
        private readonly IUserRepository _userRepo;
        private readonly IEmailHelper _emailHelper;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
       

        public UserService(ICandidateManagementRepository<UserModel> repo, IUserRepository userRepo,ApplicationDbContext context,IConfiguration configuration ,IEmailHelper emailHelper)
        {
            _repo = repo;
            _userRepo = userRepo;
            _context = context;
            _emailHelper = emailHelper;
            _configuration = configuration;
        }

        public async Task<ResponseModel> Authenticate(AuthenticateRequest model)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.Success = true;
                responseModel.StatusCode = HttpStatusCode.OK;
                var result = await _repo.Authenticate(model);
                if (result.Token == null)
                {
                    responseModel.Data = null;
                }
                else
                {
                    responseModel.Data = result;
                }
                return responseModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> ChangedPassword(ChangePasswordDto changedPasswordDto)
        {
            try
            {
                var existingUser = await _repo.GetByIdAsync(changedPasswordDto.Id);
                if (existingUser != null)
                {
                    if (existingUser.Password != changedPasswordDto.OldPassword)
                    {
                        return false;
                    }

                    existingUser.Password = changedPasswordDto.Password;
                    existingUser.UpdatedBy = CurrentUser.User.FullName;
                    existingUser.UpdatedDate = DateTime.Now;
                    return await _repo.PutAsync(existingUser, existingUser.Id);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public Task<bool> DeleteUser(int id)
        {
            try
            {
                return _repo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserDto> GetUserById(int id)
        {
            try
            {
                var resultModel = await _userRepo.GetUserByIdAsync(id);
                var resultDto = CMSAutoMapper.Mapper.Map<UserDto>(resultModel);
                return resultDto;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
       
        public async Task<List<UserDto>> GetInterviewerList()
        {
            try
            {

                var resultModel = await _userRepo.GetInterviewerList();
                var resultDto = resultModel.Select(model => CMSAutoMapper.Mapper.Map<UserDto>(model)).ToList(); ;
                return resultDto;
            }
            catch (Exception ex)
            {
                return new List<UserDto>();
            }
        }

        public async Task<int> GetCandidateCount()
        {
            try
            {
                return await _userRepo.GetCandidateCount();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> GetCandidateCountByStatus(List<string> statusName)
        {
            try
            {
                return await _userRepo.GetCandidateCountByStatus(statusName);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            try
            {

                var resultModel =  await _userRepo.GetAllUser();
                var resultDto = resultModel.Select(model => CMSAutoMapper.Mapper.Map<UserDto>(model)).ToList();
                return resultDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserDto> InsertUser(UserDto user)
        {
            try
            {
                var userModel = CMSAutoMapper.Mapper.Map<UserModel>(user);
                userModel.InsertedBy = CurrentUser.User.FullName;
                userModel.UpdatedDate = DateTime.Now;
                userModel.UserTechnologies = user.TechnologyIds.Select(techId => new TechnologyAssociation
                {
                    TechnologyId = techId
                }).ToList();
                var result = await _repo.PostAsync(userModel);
                var resultDto = CMSAutoMapper.Mapper.Map<UserDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> CheckEmailIsAlreadyExist(string email, int id)
        {
            try
            {
              return  _userRepo.CheckEmailAlreadyExist(email,id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserDto> GetByEmailAsync(string email)
        {
            try
            {
                var resultModel= await _userRepo.GetByEmailAsync(email);
                var resultDto = CMSAutoMapper.Mapper.Map<UserDto>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateUser(UserModel user, int id)
        {
            try
            {
                _context.TechnologyAssociation.RemoveRange(_context.TechnologyAssociation.Where(tech => tech.UserId == id));
                var existingUser = await _repo.GetByIdAsync(id);
                existingUser.FullName = user.FullName;
                existingUser.Password = user.Password != null ? user.Password : existingUser.Password;
                existingUser.Email = user.Email;
                existingUser.MobileNumber = user.MobileNumber;
                existingUser.UserTechnologies = user.TechnologyIds.Select(techId => new TechnologyAssociation
                {
                    TechnologyId = techId
                }).ToList();
                existingUser.DesignationId = user.DesignationId;
                existingUser.Status = user.Status;
                existingUser.Role = user.Role;
                existingUser.UpdatedBy = CurrentUser.User.FullName;
                existingUser.UpdatedDate = DateTime.Now;

                return await _repo.PutAsync(existingUser, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public async Task<bool> SentEmailForgotPassword(UserDto userDto)
        {
            try
            {
                var resetToken = Guid.NewGuid().ToString();
                var ResetPasswordTokenExpires = DateTime.UtcNow.AddMinutes(15);
                var forgotpassword = new ForgotPasswordModel
                {
                    UserId = userDto.Id,
                    Token = resetToken,
                    TokenExpires = ResetPasswordTokenExpires
                };
                await _userRepo.CreateAsync(forgotpassword);
                var baseUrl=_configuration["BaseHost"];
                // Send reset password email
                var subject = "Reset Your Password";
                var body = $"Click the link below to reset your password:<br><a href='{baseUrl}/reset-password/{resetToken}'>Reset Password</a>";
                _emailHelper.SentEmail(new List<string> { userDto.Email }, subject, body);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public async Task<ForgotPasswordModel> GetForgotPasswordEntryByTokenAsync(string token)
        {
            return await _userRepo.GetByTokenAsync(token);
        }

        public async Task<bool> ResetPasswordAsync(int userId, string newPassword)
        {
            var user = await _repo.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            user.Password = newPassword; 
            user.UpdatedDate = DateTime.UtcNow;
            bool reset= await _repo.PutAsync(user,user.Id);
            if (reset)
            {
               _userRepo.DeleteByUserIdAsync(userId);
            }
            return true;
        }
    }
}
