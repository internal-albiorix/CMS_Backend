using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagementSystem.Model.Dto
{
    public class ChangePasswordDto
    {
        public int Id { get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
    }
    public class ForgotPasswordDto
    {
        public string Email { get; set; }
    }

    // ResetPasswordDto.cs
    public class ResetPasswordDto
    {
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
