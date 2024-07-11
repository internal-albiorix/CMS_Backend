using System.Threading.Tasks;
using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAllUser();
        Task<List<UserModel>> GetInterviewerList();
        Task<int> GetCandidateCount();
        Task<bool> CheckEmailAlreadyExist(string email, int Id);
        Task<int> GetCandidateCountByStatus(List<string> statusName);
        Task<UserModel> GetUserByIdAsync(int userId);
        Task<UserModel> GetByEmailAsync(string email);
        Task<bool> CreateAsync(ForgotPasswordModel forgotPasswordModel);
        Task<ForgotPasswordModel> GetByTokenAsync(string token);
        void DeleteByUserIdAsync(int userId);
    }
}
