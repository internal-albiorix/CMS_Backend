using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Services.Interface
{
    public interface IDesignationService
    {
        Task<IEnumerable<DesignationDto>> GetDesignation();
        Task<DesignationDto> GetDesignationById(int id);
        Task<DesignationDto> InsertDesignation(DesignationDto designation);
        Task<bool> DeleteDesignation(int id);
        Task<bool> UpdateDesignation(DesignationModel designation, int id);
    }
}
