using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Helper.Interface;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CandidateManagementSystem.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }
       
        public async Task Invoke(HttpContext context, ICandidateManagementRepository<UserModel> _repo, IJwtUtils _jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                var userDto = await _repo.GetByIdAsync(Convert.ToInt32(userId));
                CurrentUser.User = CMSAutoMapper.Mapper.Map<UserModel>(userDto);
                context.Items["User"] = CurrentUser.User;
            }
            
            await _next(context);
        }
    }
}
