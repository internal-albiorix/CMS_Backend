using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Services.Interface
{
    public interface IInterviewRoundService
    {
        Task<IEnumerable<InterviewRoundDto>> GetInterviewRound();
        Task<InterviewRoundDto> GetInterviewRoundById(int id);
        Task<InterviewRoundDto> InsertInterviewRound(InterviewRoundDto InterviewRound);
        Task<bool> DeleteInterviewRound(int id);
        Task<bool> UpdateInterviewRound(InterviewRoundModel InterviewRound, int id);
    }
}
