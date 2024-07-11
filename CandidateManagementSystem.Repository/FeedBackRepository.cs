using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace CandidateManagementSystem.Repository
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private IConfiguration _configuration;
      
        private readonly ApplicationDbContext _context;

        public FeedBackRepository(IConfiguration config,ApplicationDbContext context)
        {
            _configuration = config;
            _context = context;
            
        }
        public async Task<List<FeedBackModel>> GetFeedBackByCandidatesId(int candidateId)
        {

            try
            {
                // Fetch the user by UserId
                var feedbackListModel = await _context.FeedBackModels.AsNoTracking()
                    .Where(e => e.IsActive && e.CandidateId == candidateId)
                    .Include(ft => ft.FeedbackTechnologies)
                        .ThenInclude(ft => ft.Technology)
                    .Include(ir => ir.InterviewRoundModel)
                    .OrderByDescending(x => x.InsertedDate)
                    .ToListAsync();

                foreach (var feedback in feedbackListModel)
                {
                    feedback.TechnologyIds = feedback.FeedbackTechnologies.Select(ut => ut.TechnologyId).ToList();
                    feedback.TechnologyModel = feedback.FeedbackTechnologies.Select(ut => ut.Technology).ToList();
                }
                return feedbackListModel;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"candidate with ID {candidateId} not found.");
            }

            //try
            //{
            //    var feedbackList = await db.GetCollection<FeedBackModel>("FeedBack").Aggregate()
            //                             .Match(e => e.IsActive && e.CandidateId == candidateId)
            //                             //.Lookup(
            //                             //          foreignCollection: db.GetCollection<TechnologyDto>("Technology"),
            //                             //          localField: e => e.TechnologyModel,
            //                             //          foreignField: s => s.Id,
            //                             //          @as: (FeedBackModel e) => e.TechnologyModel
            //                             //        )
            //                                     .Lookup(
            //                                      foreignCollection: db.GetCollection<InterviewRoundModel>("InterviewRound"),
            //                                      localField: e => e.InterviewRoundId,
            //                                      foreignField: a => a.Id,
            //                                      @as: (FeedBackModel e) => e.InterviewRoundModel
            //                                     ).
            //                                     Unwind<FeedBackModel, FeedBackModel>(e => e.InterviewRoundModel)
            //                                   .ToListAsync();

            //    return feedbackList.OrderByDescending(x => x.InsertedDate).ToList();
            //}
            //catch (Exception ex)
            //{
            //    return new List<FeedBackModel>();
            //}

        }
    }
}
