using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Services.Interface
{
    public interface IFeedBackService
    {
        Task<IEnumerable<FeedBackModel>> GetAllFeedBack();
        Task<List<FeedBackDto>> GetFeedBackByCandidateId(int id);
        Task<FeedBackDto> InsertCandidateFeedBack(FeedBackDto feedBackDto);
    }
}
