using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Model.Response
{
    public class AuthenticateResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public int Role { get; set; }
        public int userId { get; set; }

        public AuthenticateResponse(UserModel user, string token, int role)
        {
            FullName = user.FullName;
            Email = user.Email;
            Token = token;
            Role = role;
            userId = user.Id;
        }
    }
}
