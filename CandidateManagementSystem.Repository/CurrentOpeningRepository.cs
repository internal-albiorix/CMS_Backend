using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace CandidateManagementSystem.Repository
{
    public class CurrentOpeningRepository : ICurrentOpeningRepository
    {

        private IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public CurrentOpeningRepository(IConfiguration config,ApplicationDbContext context)
        {
            _configuration = config;
            _context = context;
           
        }

        public async Task<List<CurrentOpeningDto>> GetAllCurrentOpenings()
        {

                var currentOpeningList = _context.CurrentOpeningModels
                .Include(co => co.TechnologyModel)
                .Include(co => co.DesignationModel)
                .Where(co => co.IsActive)
                .Select(co => new CurrentOpeningDto
                {
                    Id = co.Id,
                    DesignationId = co.DesignationId,
                    TechnologyId = co.TechnologyId,
                    Experience = co.Experience,
                    Noofopening = co.Noofopening,
                    TechnologyName = co.TechnologyModel.TechnologyName,
                    DesignationName = co.DesignationModel.DesignationName
                }).ToListAsync();
            return await currentOpeningList;
        }
    }


}
