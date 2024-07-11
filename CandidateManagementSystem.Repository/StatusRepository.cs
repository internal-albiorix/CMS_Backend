using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CandidateManagementSystem.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        
        public StatusRepository(IConfiguration config,ApplicationDbContext context)
        {
            _configuration = config;
            _context = context;
            
        }
        public async Task<StatusModel> GetStatusByName(string statusName)
        {
            try
            {
                return await _context.StatusModels.FirstOrDefaultAsync(x=>x.StatusName == statusName);
            }
            catch (Exception ex)
            {
                return new StatusModel();
            }
        }
    }
}
