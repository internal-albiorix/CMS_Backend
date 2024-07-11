using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;

namespace CandidateManagementSystem.Services
{
    public class InterviewRoundService : IInterviewRoundService
    {
        #region private variables
        private readonly ICandidateManagementRepository<InterviewRoundModel> _repo;
        #endregion

        #region constructure
        public InterviewRoundService(ICandidateManagementRepository<InterviewRoundModel> repo)
        {
            _repo = repo;
        }
        #endregion

        #region methods
        public async Task<bool> DeleteInterviewRound(int id)
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

        public async Task<IEnumerable<InterviewRoundDto>> GetInterviewRound()
        {
            try
            {
                var resultModel = await _repo.GetAll();
                var resultDto = resultModel.Select(model => CMSAutoMapper.Mapper.Map<InterviewRoundDto>(model));
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<InterviewRoundDto> GetInterviewRoundById(int id)
        {
            try
            {
                var resultModel = await _repo.GetByIdAsync(id);
                var resultDto = CMSAutoMapper.Mapper.Map<InterviewRoundDto>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<InterviewRoundDto> InsertInterviewRound(InterviewRoundDto InterviewRound)
        {
            try
            {
                var interviewRoundModel = CMSAutoMapper.Mapper.Map<InterviewRoundModel>(InterviewRound);
                interviewRoundModel.InsertedBy = CurrentUser.User.FullName;
                interviewRoundModel.InsertedDate = DateTime.Now;
                var resultModel = await _repo.PostAsync(interviewRoundModel);
                var resultDto = CMSAutoMapper.Mapper.Map<InterviewRoundDto>(resultModel);

                return resultDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateInterviewRound(InterviewRoundModel InterviewRound, int id)
        {
            try
            {
                var existingInterviewRound = await _repo.GetByIdAsync(id);
                existingInterviewRound.InterviewRoundName = InterviewRound.InterviewRoundName;
                existingInterviewRound.InterviewRoundDescription = InterviewRound.InterviewRoundDescription;
                InterviewRound.UpdatedBy = CurrentUser.User.FullName;
                InterviewRound.UpdatedDate = DateTime.UtcNow;
                return await _repo.PutAsync(existingInterviewRound, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
