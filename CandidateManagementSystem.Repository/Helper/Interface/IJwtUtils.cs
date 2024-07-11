using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Repository.Helper.Interface
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(int userId);
        public string ValidateJwtToken(string token);
    }
}
