using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagementSystem.Repository
{
    public class InterviewScheduleRepository : IInterviewScheduleRepository
    {
        private IConfiguration _configuration;
        
        private readonly ApplicationDbContext _context;

        public InterviewScheduleRepository(IConfiguration config,ApplicationDbContext context)
        {
            _configuration = config;
            _context = context;
           
        }
        public async Task<List<InterviewScheduleModel>> GetAllInterviewSchedule()
        {

            try
            {
                
                var result= await _context.InterviewScheduleModels
                          .Where(e => e.IsActive)
                          .Include(c => c.CandidateModel)
                          .Include(u => u.InterviewerModel)
                          .Include(i => i.InterviewRoundModel)
                           .Where(x => x.CandidateModel != null)
                          .ToListAsync();
                //var result = await _context.InterviewScheduleModels
                //          .Where(e => e.IsActive && e.CandidateModel !=null)
                       
                //          .Select(schedule => new InterviewScheduleModel
                //          {
                //              Id = schedule.Id,
                //              CandidateId = schedule.CandidateId,
                //              InterviewRoundId = schedule.InterviewRoundId,
                //              InterviewerId = schedule.InterviewerId,
                //              StartDate = schedule.StartDate,
                //              EndDate = schedule.EndDate,
                //              Title = schedule.Title,
                //              IsActive = schedule.IsActive,
                //              CandidateModel = schedule.CandidateModel,
                //              InterviewerModel = schedule.InterviewerModel,
                //              InterviewRoundModel = schedule.InterviewRoundModel
                //          })
                //          .ToListAsync();


                if ((int)UserRoles.Interviewer == CurrentUser.User.Role)
                {
                    result = result.Where(x => x.InterviewerId == CurrentUser.User.Id).ToList();
                }
                return result;
                //var InterviewScheduleList = await db.GetCollection<InterviewScheduleModel>("InterviewSchedule")
                //                                 .AsQueryable()
                //                                 .Where(e => e.IsActive)
                //                                 .ToListAsync();


                //var candidateIds = InterviewScheduleList.Select(e => e.CandidateId).ToList();
                //var interviewerIds = InterviewScheduleList.Select(e => e.InterviewerId).ToList();
                //var interviewRoundIds = InterviewScheduleList.Select(e => e.InterviewRoundId).ToList();

                //var candidates = await db.GetCollection<CandidateModel>("Candidate")
                //    .AsQueryable()
                //    .Where(c => c.IsActive && candidateIds.Contains(c.Id))
                //    .ToListAsync();

                //var users = await db.GetCollection<UserModel>("User")
                //    .AsQueryable()
                //    .Where(u => interviewerIds.Contains(u.Id))
                //    .ToListAsync();

                //var interviewRounds = await db.GetCollection<InterviewRoundModel>("InterviewRound")
                //    .AsQueryable()
                //    .Where(ir => interviewRoundIds.Contains(ir.Id))
                //    .ToListAsync();

                //var result = InterviewScheduleList.Select(schedule => new InterviewScheduleModel
                //{
                //    Id = schedule.Id,
                //    CandidateId = schedule.CandidateId,
                //    InterviewRoundId = schedule.InterviewRoundId,
                //    InterviewerId = schedule.InterviewerId,
                //    StartDate = schedule.StartDate,
                //    EndDate = schedule.EndDate,
                //    Title = schedule.Title,
                //    IsActive = schedule.IsActive,
                //    CandidateModel = candidates.FirstOrDefault(c => c.Id == schedule.CandidateId),
                //    InterviewerModel = users.FirstOrDefault(u => u.Id == schedule.InterviewerId),
                //    InterviewRoundModel = interviewRounds.FirstOrDefault(ir => ir.Id == schedule.InterviewRoundId)
                //}).Where(x => x.CandidateModel != null).ToList();


                              
            }
            catch (Exception ex)
            {
                return new List<InterviewScheduleModel>();
            }
        }

        public async Task<List<InterviewScheduleModel>> GetUpcomingInterviews()
        {

            try
            {

                var InterviewScheduleList = await _context.InterviewScheduleModels
                         .Where(e => e.IsActive)
                         .Include(c => c.CandidateModel)
                         .Include(u => u.InterviewerModel)
                         .Include(i => i.InterviewRoundModel).
                         OrderByDescending(schedule => schedule.InsertedDate)
                         .Take(3)
                         .Select(schedule => new InterviewScheduleModel
                         {
                             Id = schedule.Id,
                             StartDate = schedule.StartDate,
                             CandidateModel = schedule.CandidateModel,
                             InterviewerModel = schedule.InterviewerModel,
                             InterviewRoundModel = schedule.InterviewRoundModel
                         })
                        .Where(x => x.CandidateModel != null && (x.InterviewerModel.Id == CurrentUser.User.Id || CurrentUser.User.Role == (int)UserRoles.Admin || CurrentUser.User.Role == (int)UserRoles.HR))
                        .ToListAsync();
                return InterviewScheduleList;

                //var InterviewScheduleList = await db.GetCollection<InterviewScheduleModel>("InterviewSchedule")
                //                                 .AsQueryable()
                //                                 .Where(e => e.IsActive)
                //                                 .OrderByDescending(x => x.InsertedDate)
                //                                 .Take(3)
                //                                 .ToListAsync();
                //var candidateIds = InterviewScheduleList.Select(e => e.CandidateId).ToList();
                //var interviewerIds = InterviewScheduleList.Select(e => e.InterviewerId).ToList();
                //var interviewRoundIds = InterviewScheduleList.Select(e => e.InterviewRoundId).ToList();
                //var candidates = await db.GetCollection<CandidateModel>("Candidate")
                //    .AsQueryable()
                //    .Where(c => c.IsActive && candidateIds.Contains(c.Id))
                //    .ToListAsync();

                //var interviewRounds = await db.GetCollection<InterviewRoundModel>("InterviewRound")
                //  .AsQueryable()
                //  .Where(ir => interviewRoundIds.Contains(ir.Id))
                //  .ToListAsync();

                //var users = await db.GetCollection<UserModel>("User")
                //   .AsQueryable()
                //   .Where(u => interviewerIds.Contains(u.Id))
                //   .ToListAsync();

                //var result = InterviewScheduleList.Select(schedule => new InterviewScheduleModel
                //{
                //    Id = schedule.Id,
                //    StartDate = schedule.StartDate,
                //    CandidateModel = candidates.FirstOrDefault(c => c.Id == schedule.CandidateId),
                //    InterviewerModel = users.FirstOrDefault(u => u.Id == schedule.InterviewerId),
                //    InterviewRoundModel = interviewRounds.FirstOrDefault(ir => ir.Id == schedule.InterviewRoundId)
                //}).Where(x=>x.CandidateModel!=null).ToList();
                //result = result.Where(x => x.CandidateModel != null && x.InterviewerModel != null).ToList();
                //return result;
               // return new List<InterviewScheduleModel>();
            }
            catch (Exception ex)
            {
                return new List<InterviewScheduleModel>();
            }
        }

        public async Task<int> CandidateAssignAnyInterview(int candidateId)
        {
            try
            {
                var appointmentlistCount = await _context.InterviewScheduleModels
                                            .Where(x => x.IsActive && x.CandidateId == candidateId)
                                            .CountAsync();
                return appointmentlistCount;
            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
