using CandidateManagementSystem.Model.Dto;

namespace CandidateManagementSystem.Repository.Interface
{
    public interface ICurrentOpeningRepository
    {
         Task<List<CurrentOpeningDto>> GetAllCurrentOpenings();
    }
}
