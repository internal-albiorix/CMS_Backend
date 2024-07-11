
using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Model.Request;
using CandidateManagementSystem.Model.Response;

namespace CandidateManagementSystem.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> GetUserById(int Id);
        Task<UserDto> InsertUser(UserDto user);
        Task<bool> DeleteUser(int Id);
        Task<bool> UpdateUser(UserModel user, int Id);
        Task<ResponseModel> Authenticate(AuthenticateRequest model);
        Task<List<UserDto>> GetInterviewerList();
        Task<int> GetCandidateCount();
        Task<bool> ChangedPassword(ChangePasswordDto changedPasswordDto);
        Task<bool> CheckEmailIsAlreadyExist(string email, int Id);
        Task<UserDto> GetByEmailAsync(string email);
        Task<int> GetCandidateCountByStatus(List<string> statusName);
        Task<bool> SentEmailForgotPassword(UserDto userDto);
        Task<bool> ResetPasswordAsync(int userId, string newPassword);
        Task<ForgotPasswordModel> GetForgotPasswordEntryByTokenAsync(string token);


    }
}
