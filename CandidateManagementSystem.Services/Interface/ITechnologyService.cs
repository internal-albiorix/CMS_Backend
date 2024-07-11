using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Services.Interface
{
    public interface ITechnologyService
    {
        Task<IEnumerable<TechnologyDto>> GetTechnology();
        Task<TechnologyDto> GetTechnologyById(int id);
        Task<TechnologyDto> InsertTechnology(TechnologyDto technology);
        Task<bool> DeleteTechnology(int id);
        Task<bool> UpdateTechnology(TechnologyModel technology, int id);
    }
}
