using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;

namespace CandidateManagementSystem.Services
{
    public class CurrentOpeningService : ICurrentOpeningService
    {
        #region private variables
        private readonly ICandidateManagementRepository<CurrentOpeningModel> _repo;
        private readonly ICurrentOpeningRepository _currentOpeningRepository;
        #endregion

        #region constructure
        public CurrentOpeningService(ICandidateManagementRepository<CurrentOpeningModel> repo, ICurrentOpeningRepository currentOpeningRepository)
        {
            _repo = repo;
            _currentOpeningRepository = currentOpeningRepository;
        }
        #endregion

        #region methods
        public async Task<bool> DeleteCurrentOpening(int id)
        {
            try
            {
                return await _repo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CurrentOpeningDto>> GetCurrentOpening()
        {
            try
            {
                return await _currentOpeningRepository.GetAllCurrentOpenings();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CurrentOpeningDto> GetCurrentOpeningById(int id)
        {
            try
            {
                var resultModel = await _repo.GetByIdAsync(id);
                var resultDto = CMSAutoMapper.Mapper.Map<CurrentOpeningDto>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<CurrentOpeningDto> InsertCurrentOpening(CurrentOpeningDto CurrentOpening)
        {
            try
            {
                var currentOpeningModel = CMSAutoMapper.Mapper.Map<CurrentOpeningModel>(CurrentOpening);
                currentOpeningModel.InsertedBy = CurrentUser.User.FullName;
                currentOpeningModel.InsertedDate = DateTime.Now;
                var resultModel = await _repo.PostAsync(currentOpeningModel);
                var resultDto = CMSAutoMapper.Mapper.Map<CurrentOpeningDto>(resultModel);

                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateCurrentOpening(CurrentOpeningModel CurrentOpening, int id)
        {
            try
            {
                var existingCurrentOpening = await _repo.GetByIdAsync(id);
                existingCurrentOpening.Noofopening = CurrentOpening.Noofopening;
                existingCurrentOpening.Experience= CurrentOpening.Experience;
                existingCurrentOpening.TechnologyId= CurrentOpening.TechnologyId;
                existingCurrentOpening.DesignationId= CurrentOpening.DesignationId;
                existingCurrentOpening.UpdatedBy = CurrentUser.User.FullName;
                existingCurrentOpening.UpdatedDate = DateTime.Now;
                return await _repo.PutAsync(existingCurrentOpening, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
