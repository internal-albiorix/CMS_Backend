using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Services.Interface
{
    public interface ICurrentOpeningService
    {
        Task<List<CurrentOpeningDto>> GetCurrentOpening();
        Task<CurrentOpeningDto> GetCurrentOpeningById(int id);
        Task<CurrentOpeningDto> InsertCurrentOpening(CurrentOpeningDto CurrentOpening);
        Task<bool> DeleteCurrentOpening(int id);
        Task<bool> UpdateCurrentOpening(CurrentOpeningModel CurrentOpening, int id);
    }
}
