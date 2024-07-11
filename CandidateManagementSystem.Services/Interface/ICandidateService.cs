using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using Microsoft.AspNetCore.Mvc;

namespace CandidateManagementSystem.Services.Interface
{
    public interface ICandidateService
    {
        Task<IEnumerable<CandidateDto>> GetCandidate();
        Task<CandidateDto> GetCandidateById(int id);
        Task<bool> CheckEmailIsAlreadyExist(string email, int id);
        Task<CandidateDto> InsertCandidate(CandidateDto candidate);
        Task<bool> DeleteCandidate(int id);
        Task<bool> UpdateCandidate(CandidateDto candidate, int id);
        Task<List<CandidateModel>> GetAppointMentsCandidate();
        Task<IEnumerable<CandidateDto>> GetLatestCandidate();
        Task<IEnumerable<CandidateChartModel>> GetCandidateForChart();
        Task<IEnumerable<CandidateDto>> GetFilteredCandidateData(CandidateFilterDto filterDto);

        Task<bool> RejectInterviewSchedule(int id);

    }
}
