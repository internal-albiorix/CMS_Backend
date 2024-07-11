using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.HelperModel;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace CandidateManagementSystem.Repository
{
    public class CandidateHistoryRepository : ICandidateHistoryRepository
    {
        private IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public CandidateHistoryRepository(IConfiguration config,ApplicationDbContext context)
        {
            _configuration = config;
            _context = context;
            
        }
        public async Task<List<CandidateHistoryModel>> GetCandidateHistoryByCandidateId(int CandidateId)
        {
            try
            {
                //var candidateHostoryList = db.GetCollection<CandidateHistoryModel>("CandidateHistory").AsQueryable()
                //                            .Where(e => e.CandidateId == CandidateId && e.IsActive).ToList();
                //var statusList = db.GetCollection<StatusModel>("Status").AsQueryable()
                //                            .Where(e => e.IsActive).ToList();
                //var interviewRoundList = db.GetCollection<InterviewRoundModel>("InterviewRound").AsQueryable()
                //                            .Where(e => e.IsActive).ToList();
                //var interviewerList = db.GetCollection<UserModel>("User").AsQueryable()
                //                            .Where(e =>  e.IsActive && (int)UserRoles.Interviewer == e.Role).ToList();

                var candidateHistoryList = await _context.CandidateHistoryModels
                                             .Where(e => e.CandidateId == CandidateId && e.IsActive)
                                             .Include(s => s.StatusModel)
                                             .Include(iv => iv.InterviewerModel)
                                             .Include(ir => ir.InterviewRoundModel)
                                             .Where(iv => iv.InterviewerModel == null || iv.InterviewerModel.Role == (int)UserRoles.Interviewer)
                                              .OrderByDescending(ch => ch.InsertedDate)
                                            .Select(item => new CandidateHistoryModel
                                            {
                                                Id = item.Id,
                                                CandidateId = item.CandidateId,
                                                StatusId = item.StatusId,
                                                StatusModel = item.StatusModel,
                                                InterviewRoundId = item.InterviewRoundId,
                                                InterviewRoundModel = item.InterviewRoundModel,
                                                InterviewerId = item.InterviewerId, 
                                               InterviewerModel = item.InterviewerModel,
                                                Message = item.Message,
                                                TimeLineDate =ExtensionClass.ToDate(item.InsertedDate.ToString()),
                                                InterviewStartDate =(item.InterviewStartDate!=null)?ExtensionClass.ToDate(item.InterviewStartDate.ToString()):null,
                                            })
                                           .ToListAsync();
                                           
                                           

                //foreach (var item in candidateHostoryList)
                //{

                //    item.TimeLineDate = ExtensionClass.ToDate(item.InsertedDate.ToString());
                //    item.StatusModel = statusList.FirstOrDefault(e => e.Id == item.StatusId);
                //    item.InterviewRoundModel = interviewRoundList.FirstOrDefault(e => e.Id == item.InterviewRoundId);
                //    item.InterviewerModel = interviewerList.FirstOrDefault(e=>e.Id == item.InterviewerId);
                //    item.InterviewStartDate = ExtensionClass.ToDate(item.InterviewStartDate?.ToString());
                //}
                
                return candidateHistoryList;
            }
            catch (Exception ex)
            {
                return new List<CandidateHistoryModel>();
            }
        }
    }
}
