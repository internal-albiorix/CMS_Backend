using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace CandidateManagementSystem.Repository
{
    public class ReferEmployeeRepository : IReferEmployeeRepository
    {
        private IConfiguration _configuration;
       
        private readonly ApplicationDbContext _context;

        public ReferEmployeeRepository(IConfiguration config,ApplicationDbContext context)
        {
            _configuration = config;
            _context = context;
           
        }

        public async Task<List<ReferEmployeeModel>> GetAllReferEmployee()
        {

            try
            {
                var referEmployeeListModel = await _context.ReferEmployeeModels
                                        .Where(e => e.IsActive)
                                        .Include(ret => ret.ReferEmployeeTechnologies)
                                            .ThenInclude(ret => ret.Technology)
                                        .OrderByDescending(e => e.InsertedDate)
                                        .ToListAsync();

                foreach (var referEmployee in referEmployeeListModel)
                {
                    referEmployee.TechnologyIds = referEmployee.ReferEmployeeTechnologies.Select(ut => ut.TechnologyId).ToList();
                    referEmployee.TechnologyModel = referEmployee.ReferEmployeeTechnologies.Select(ut => ut.Technology).ToList();
                }
               // var referEmployeeDto = referEmployeeList.Select(model => CMSAutoMapper.Mapper.Map<ReferEmployeeDto>(model)).ToList();
                return referEmployeeListModel;


            }
            catch (Exception ex)
            {
                return new List<ReferEmployeeModel>();
            }
        }


        public async Task<ReferEmployeeModel> GetReferEmployeeByIdAsync(int ReferEmployeeId)
        {
            try
            {
                // Fetch the user by UserId
                var referEmployeeModel = await _context.ReferEmployeeModels.AsNoTracking()
                    .Where(e => e.IsActive && e.Id == ReferEmployeeId)
                    .Include(ut => ut.ReferEmployeeTechnologies)
                        .ThenInclude(ut => ut.Technology)
                    .FirstOrDefaultAsync();

                if (referEmployeeModel != null)
                {
                    if(referEmployeeModel.ReferEmployeeTechnologies.Count > 0)
                    {
                    // Assign technology ids and models to the userDto
                    referEmployeeModel.TechnologyIds = referEmployeeModel.ReferEmployeeTechnologies.Select(ut => ut.TechnologyId).ToList();
                    referEmployeeModel.TechnologyModel = referEmployeeModel.ReferEmployeeTechnologies.Select(x => x.Technology).ToList();
                    }

                    return referEmployeeModel;
                }
                else
                {
                    throw new InvalidOperationException($"ReferEmployee with ID {ReferEmployeeId} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"ReferEmployee with ID {ReferEmployeeId} not found.");
            }
        }

        //public async Task AssociateTechnologiesWithReferEmployee(int id, List<int> TechnologyIds)
        //{
        //    try
        //    {
        //        List<TechnologyAssociation> technologies = TechnologyIds
        //            .Select(x => new TechnologyAssociation
        //            {
        //                ReferEmployeeId = id,
        //                TechnologyId = x
        //            }).ToList();

        //        await _context.TechnologyAssociation.AddRangeAsync(technologies);
        //        // Save changes to the database
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

    }
}
