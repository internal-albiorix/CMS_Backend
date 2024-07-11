using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystem.Services
{
    public class ReferemployeeService : IReferemployeeService
    {
        #region private variables
        private readonly ICandidateManagementRepository<ReferEmployeeModel> _repo;
        private readonly IReferEmployeeRepository _referEmpRepo;
        private readonly ApplicationDbContext _context;
        #endregion

        #region constructure
        public ReferemployeeService(ICandidateManagementRepository<ReferEmployeeModel> repo, IReferEmployeeRepository referEmpRepo,ApplicationDbContext context)
        {
            _repo = repo;
            _referEmpRepo = referEmpRepo;
            _context = context;
        }
        #endregion

        #region methods
        public Task<bool> DeleteReferEmployee(int id)
        {
            try
            {
                return _repo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ReferEmployeeDto>> GetReferEmployee()
        {
            try
            {
                var resultModel = await _referEmpRepo.GetAllReferEmployee();
                var resultDto = resultModel.Select(model => CMSAutoMapper.Mapper.Map<ReferEmployeeDto>(model)).ToList();
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReferEmployeeDto> GetReferEmployeeById(int id)
        {
            try
            {
                var resultModel = await _referEmpRepo.GetReferEmployeeByIdAsync(id);
                var resultDto = CMSAutoMapper.Mapper.Map<ReferEmployeeDto>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ReferEmployeeDto> InsertReferEmployee(ReferEmployeeDto referemployee)
        {
            try
            {
                var referemployeeModel = CMSAutoMapper.Mapper.Map<ReferEmployeeModel>(referemployee);
                referemployeeModel.InsertedBy = CurrentUser.User.FullName;
                referemployeeModel.InsertedDate = DateTime.Now;
                referemployeeModel.ReferEmployeeTechnologies = referemployee.TechnologyIds.Select(techId => new TechnologyAssociation
                {
                    TechnologyId = techId
                }).ToList();
                var result =  await _repo.PostAsync(referemployeeModel);
                var resultDto = CMSAutoMapper.Mapper.Map<ReferEmployeeDto>(result);
                return resultDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateReferEmployee(ReferEmployeeDto referemployeeDto, int id)
        {
            try
            {
                _context.TechnologyAssociation.RemoveRange(_context.TechnologyAssociation.Where(tech => tech.ReferEmployeeId == id));
                var existingReferEmployee = await _repo.GetByIdAsync(id);
                existingReferEmployee.FullName = referemployeeDto.FullName;
                existingReferEmployee.Email= referemployeeDto.Email;
                existingReferEmployee.MobileNumber= referemployeeDto.MobileNumber;
                existingReferEmployee.CandidateFullName= referemployeeDto.CandidateFullName;
                existingReferEmployee.CandidateEmail = referemployeeDto.CandidateEmail;
                existingReferEmployee.CandidateMobileNumber = referemployeeDto.CandidateMobileNumber;
                // existingReferEmployee.TechnologyIds = referemployeeDto.TechnologyIds;
                existingReferEmployee.ReferEmployeeTechnologies = referemployeeDto.TechnologyIds.Select(techId => new TechnologyAssociation
                {
                    TechnologyId = techId
                }).ToList();
                existingReferEmployee.CandidateExperience = referemployeeDto.CandidateExperience;
                if(!string.IsNullOrEmpty(referemployeeDto.Resume) &&  referemployeeDto.Resume != "undefined")
                {
                    existingReferEmployee.Resume = referemployeeDto.Resume;
                }
                existingReferEmployee.UpdatedDate = DateTime.Now;
                existingReferEmployee.UpdatedBy = CurrentUser.User.FullName;
                return await _repo.PutAsync(existingReferEmployee, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
