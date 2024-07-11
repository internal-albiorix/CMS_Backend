using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystem.Services
{
    public class CandidateHistoryService : ICandidateHistoryService
    {
        private readonly ICandidateManagementRepository<CandidateHistoryModel> _repo;
        private readonly ICandidateHistoryRepository _candidateHistoryRepository;
        public CandidateHistoryService(ICandidateManagementRepository<CandidateHistoryModel> repo, ICandidateHistoryRepository candidateHistoryRepository)
        {
            _repo = repo;
            _candidateHistoryRepository = candidateHistoryRepository;
        }
        public async Task<CandidateHistoryDto> InsertCandidateHistory(CandidateHistoryDto candidateHistory)
        {
            try
            {

                var candidateHistoryDto = new CandidateHistoryModel
                {
                    StatusId = candidateHistory.StatusId,
                    CandidateId = candidateHistory.CandidateId,
                    InterviewRoundId = candidateHistory.InterviewRoundId,
                    InterviewerId = candidateHistory.InterviewerId,
                    Message = candidateHistory.Message,
                    InterviewStartDate = candidateHistory.InterviewStartDate,
                    
                };
                //candidateHistorymodel.InsertedBy = CurrentUser.User.FullName;
                candidateHistoryDto.InsertedDate = DateTime.Now;

                var resultModel = await _repo.PostAsync(candidateHistoryDto);
                var resultDto = CMSAutoMapper.Mapper.Map<CandidateHistoryDto>(resultModel);
                return resultDto;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateCandidateHistory(CandidateHistoryDto candidateHistorydto, int id)
        {
            try
            {
                var exisitngCandidateHistory = await _repo.GetByIdAsync(id);
                exisitngCandidateHistory.Message = candidateHistorydto.Message;
                exisitngCandidateHistory.UpdatedBy = CurrentUser.User.FullName;
                exisitngCandidateHistory.UpdatedDate = DateTime.Now;
                return await _repo.PutAsync(exisitngCandidateHistory, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CandidateHistoryModel>> GetCandidateHistoryByCandidateId(int candidateid)
        {
            try
            {
                return await _candidateHistoryRepository.GetCandidateHistoryByCandidateId(candidateid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       
    }
}
