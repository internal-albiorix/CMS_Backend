using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;

using System.Linq;
using System.Text.Json.Serialization;

namespace CandidateManagementSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private IConfiguration _configuration;
      
        private readonly ApplicationDbContext _context;
        public UserRepository(IConfiguration config,ApplicationDbContext context)
        {
            _configuration = config;
            _context = context;
           
        }

        public async Task<IEnumerable<UserModel>> GetAllUser()
        {
            try
            {
                var usersListModel = await _context.UserModels
                .Where(e => e.IsActive)
                .Include(e => e.DesignationModel)
                .Include(e=>e.UserTechnologies)
                    .ThenInclude(ut => ut.Technology)
                .ToListAsync();

                foreach (var user in usersListModel)
                {
                    user.TechnologyIds = user.UserTechnologies.Select(ut => ut.TechnologyId).ToList();
                    user.TechnologyModel = user.UserTechnologies.Select(ut => ut.Technology).ToList();
                }
                return usersListModel;
            }
            catch (Exception ex)
            {
                return new List<UserModel>();
            }
            
        }

        public async Task<UserModel> GetUserByIdAsync(int userId)
        {
            try
            {
                // Fetch the user by UserId
                var userModel = await _context.UserModels.AsNoTracking()
                    .Where(e => e.IsActive && e.Id == userId)
                    .Include(e => e.DesignationModel)
                    .Include(ut=> ut.UserTechnologies)
                        .ThenInclude (ut => ut.Technology)
                    .FirstOrDefaultAsync();

                if (userModel != null)
                {
                    if (userModel.UserTechnologies.Count > 0)
                    {
                        // Assign technology ids and models to the userDto
                        userModel.TechnologyIds = userModel.UserTechnologies.Select(ut => ut.TechnologyId).ToList();
                        userModel.TechnologyModel = userModel.UserTechnologies.Select(x => x.Technology).ToList();
                    }
                    //var userDto = CMSAutoMapper.Mapper.Map<UserDto>(userModel);

                    return userModel;
                }
                else
                {
                    throw new InvalidOperationException($"User with ID {userId} not found.");
                }
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"User with ID {userId} not found.");
            }
        }
         
        public async Task<List<UserModel>> GetInterviewerList()
        {
            try
            {
                 return await _context.UserModels.Where(x => x.IsActive && x.Role == (int)UserRoles.Interviewer).ToListAsync();
               
            }
            catch (Exception ex)
            {
                return new List<UserModel>();
            }
        }

        public async Task<UserModel> GetByEmailAsync(string email)
        {
            try
            {
                return await _context.UserModels.FirstOrDefaultAsync(user => user.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving data.", ex);
            }
        }
        public async Task<int> GetCandidateCount()
        {
            try
            {
                return await _context.CandidateModels.Where(x => x.IsActive).CountAsync();
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
                var candidateCounts = await _context.CandidateModels
                .Where(e => e.IsActive &&
                //e.StatusId != null &&
                e.StatusModel != null &&
                statusName.Contains(e.StatusModel.StatusName))
                .CountAsync();
                return candidateCounts;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<bool> CreateAsync(ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                await _context.ForgotPasswordModels.AddAsync(forgotPasswordModel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while saving data.", ex);
            }
        }
        public async Task<bool> CheckEmailAlreadyExist(string email, int id)
        {
            try
            {
                var userCount = await _context.UserModels
                    .Where(x => x.IsActive && x.Email == email && x.Id != id)
                    .CountAsync();

                return userCount > 0;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        public async Task<ForgotPasswordModel> GetByTokenAsync(string token)
        {
            try
            {
                return await _context.ForgotPasswordModels.FirstOrDefaultAsync(u => u.Token == token);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving data.", ex);
            }
        }
        public  void DeleteByUserIdAsync(int userId)
        {
            try
            {
                var forgotUser = _context.ForgotPasswordModels.Where(p => p.UserId == userId).ToList();
                if (forgotUser.Any())
                {
                    _context.ForgotPasswordModels.RemoveRange(forgotUser);
                     _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving data.", ex);
            }
        }
    }
}
