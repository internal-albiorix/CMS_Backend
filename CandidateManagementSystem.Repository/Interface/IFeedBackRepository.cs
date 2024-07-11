using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Repository.Interface
{
    public interface IFeedBackRepository
    {
        Task<List<FeedBackModel>> GetFeedBackByCandidatesId(int candidateId);
    }
}
