using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;

namespace CandidateManagementSystem.Services
{
    public class FeedBackService : IFeedBackService
    {
        private readonly ICandidateManagementRepository<FeedBackModel> _repo;
        private readonly IFeedBackRepository _feedBackRepository;

        public FeedBackService(ICandidateManagementRepository<FeedBackModel> repo, IFeedBackRepository feedBackRepository)
        {
            _repo = repo;
            _feedBackRepository = feedBackRepository;
        }

        public Task<IEnumerable<FeedBackModel>> GetAllFeedBack()
        {
            throw new NotImplementedException();
        }

        public async Task<List<FeedBackDto>> GetFeedBackByCandidateId(int id)
        {
            try
            {

                var resultModel = await _feedBackRepository.GetFeedBackByCandidatesId(id);
                var resultDto = resultModel.Select(model => CMSAutoMapper.Mapper.Map<FeedBackDto>(model)).ToList();
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FeedBackDto> InsertCandidateFeedBack(FeedBackDto feedBackDto)
        {
            try
            {
                var feedbackModel = CMSAutoMapper.Mapper.Map<FeedBackModel>(feedBackDto);
                feedBackDto.InsertedBy = CurrentUser.User.FullName;
                feedbackModel.InsertedDate = DateTime.Now;
                feedbackModel.FeedbackTechnologies = feedBackDto.TechnologyIds.Select(techId => new TechnologyAssociation
                {
                    TechnologyId = techId
                }).ToList();
                var result = await _repo.PostAsync(feedbackModel);
                var resultDto = CMSAutoMapper.Mapper.Map<FeedBackDto>(result);
                return resultDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
