using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;

namespace CandidateManagementSystem.Services
{
    public class TechnologyService : ITechnologyService
    {
        #region private variables
        private readonly ICandidateManagementRepository<TechnologyModel> _repo;
        #endregion

        #region constructure
        public TechnologyService(ICandidateManagementRepository<TechnologyModel> repo)
        {
            _repo = repo;
        }
        #endregion

        #region methods
        public Task<bool> DeleteTechnology(int id)
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

        public async Task<IEnumerable<TechnologyDto>> GetTechnology()
        {
            try
            {
                var resultModel = await _repo.GetAll();
                var resultDto = resultModel.Select(model => CMSAutoMapper.Mapper.Map<TechnologyDto>(model));
                return resultDto;
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }

        public async Task<TechnologyDto> GetTechnologyById(int id)
        {
            try
            {
                var resultModel = await _repo.GetByIdAsync(id);
                var resultDto = CMSAutoMapper.Mapper.Map<TechnologyDto>(resultModel);
                return resultDto;
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<TechnologyDto> InsertTechnology(TechnologyDto technology)
        {
            try
            {
                var technologyModel = CMSAutoMapper.Mapper.Map<TechnologyModel>(technology);
                technologyModel.InsertedBy = CurrentUser.User.FullName;
                technologyModel.InsertedDate = DateTime.Now;
                var resultModel = await _repo.PostAsync(technologyModel);
                var resultDto = CMSAutoMapper.Mapper.Map<TechnologyDto>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateTechnology(TechnologyModel technology, int id)
        {
            try
            {
                var existingTechnology = await _repo.GetByIdAsync(id);
                existingTechnology.TechnologyName = technology.TechnologyName;
                existingTechnology.TechnologyDescription = technology.TechnologyDescription;
                technology.UpdatedBy = CurrentUser.User.FullName;
                technology.UpdatedDate = DateTime.UtcNow;
                return await _repo.PutAsync(existingTechnology, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
