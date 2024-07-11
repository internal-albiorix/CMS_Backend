using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;

namespace CandidateManagementSystem.Services
{
    public class StatusService : IStatusService
    {
        #region private variables
        private readonly ICandidateManagementRepository<StatusModel> _repo;
        #endregion

        #region constructure
        public StatusService(ICandidateManagementRepository<StatusModel> repo)
        {
            _repo = repo;
        }
       
        #endregion
        

        #region methods
        public Task<bool> DeleteStatus(int id)
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

        public async Task<IEnumerable<StatusDto>> GetStatus()
        {
            try
            {
                var resultModel = await _repo.GetAll();
                var resultDto = resultModel.Select(model => CMSAutoMapper.Mapper.Map<StatusDto>(model));
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        


        public async Task<StatusDto> GetStatusById(int id)
        {
            try
            {
                var resultModel = await _repo.GetByIdAsync(id);
                var resultDto = CMSAutoMapper.Mapper.Map<StatusDto>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<StatusDto> InsertStatus(StatusDto Status)
        {
            try
            {
                var statusModel = CMSAutoMapper.Mapper.Map<StatusModel>(Status);
                statusModel.InsertedBy = CurrentUser.User.FullName;
                statusModel.InsertedDate = DateTime.Now;
                var resultModel = await _repo.PostAsync(statusModel);
                var resultDto = CMSAutoMapper.Mapper.Map<StatusDto>(resultModel);

                return resultDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateStatus(StatusModel Status, int id)
        {
            try
            {
                var existingStatus = await _repo.GetByIdAsync(id);
                existingStatus.StatusName = Status.StatusName;
                existingStatus.StatusDescription = Status.StatusDescription;
                Status.UpdatedBy = CurrentUser.User.FullName;
                Status.UpdatedDate = DateTime.Now;
                existingStatus.DisplayInAppointSchedule = Status.DisplayInAppointSchedule;
                return await _repo.PutAsync(existingStatus, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
