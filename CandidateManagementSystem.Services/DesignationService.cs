using System.Net.WebSockets;
using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;

namespace CandidateManagementSystem.Services
{
    public class DesignationService : IDesignationService
    {
        #region private variables
        private readonly ICandidateManagementRepository<DesignationModel> _repo;
        #endregion

        #region constructure
        public DesignationService(ICandidateManagementRepository<DesignationModel> repo)
        {
            _repo = repo;
        }
        #endregion

        #region methods
        public Task<bool> DeleteDesignation(int id)
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

        public async Task<IEnumerable<DesignationDto>> GetDesignation()
        {
            try
            {
                var resultModel = await _repo.GetAll();
                var resultDto = resultModel.Select(model => CMSAutoMapper.Mapper.Map<DesignationDto>(model));
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DesignationDto> GetDesignationById(int id)
        {
            try
            {
                var resultModel = await _repo.GetByIdAsync(id);
                var resultDto = CMSAutoMapper.Mapper.Map<DesignationDto>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DesignationDto> InsertDesignation(DesignationDto designation)
        {
            try
            {
                var designationModel = CMSAutoMapper.Mapper.Map<DesignationModel>(designation);
                designationModel.InsertedBy = CurrentUser.User.FullName;
                designationModel.InsertedDate = DateTime.Now;
                var resultModel = await _repo.PostAsync(designationModel);
                var resultDto = CMSAutoMapper.Mapper.Map<DesignationDto>(resultModel);

                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateDesignation(DesignationModel designation, int id)
        {
            try
            {
                 var existingDesignation = await _repo.GetByIdAsync(id);
                existingDesignation.DesignationName = designation.DesignationName;
                existingDesignation.DesignationDescription = designation.DesignationDescription;
                existingDesignation.UpdatedBy = CurrentUser.User.FullName;
                existingDesignation.UpdatedDate = DateTime.Now;
                return await _repo.PutAsync(existingDesignation, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
