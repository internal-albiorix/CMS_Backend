using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Interface;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagementSystem.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private IConfiguration _configuration;
        private readonly ApplicationDbContext _context;



        public CandidateRepository(IConfiguration config, ApplicationDbContext context)
        {
            _configuration = config;
            _context = context;

        }
        public async Task<List<CandidateModel>> GetAllCandidates()
        {

            try
            {
                var candidateListModel = await _context.CandidateModels
                         .Where(e => e.IsActive)
                         .Include(s => s.StatusModel)
                         .Include(ct => ct.CandidateTechnologies)
                              .ThenInclude(ct => ct.Technology)
                         .OrderByDescending(e => e.InsertedDate)
                         .ToListAsync();

                var candidateIds = candidateListModel.Select(c => c.Id).ToList();

                var interviewData = await _context.InterviewScheduleModels
                                    .Where(i => candidateIds.Contains(i.CandidateId))
                                    .Include(i => i.InterviewerModel)
                                    .GroupBy(i => i.CandidateId)
                                    .Select(g => g.OrderByDescending(i => i.Id).FirstOrDefault())
                                    .ToListAsync();

                foreach (var candidate in candidateListModel)
                {
                    var interview = interviewData.FirstOrDefault(i => i.CandidateId == candidate.Id);
                    if (interview != null)
                    {
                        candidate.IsReject = Convert.ToBoolean(interview.IsReject);
                        candidate.InterviewerName = interview.InterviewerModel.FullName;
                    }
                    candidate.TechnologyIds = candidate.CandidateTechnologies.Select(ut => ut.TechnologyId).ToList();
                    candidate.TechnologyModel = candidate.CandidateTechnologies.Select(ut => ut.Technology).ToList();
                }

                if ((int)UserRoles.Interviewer == CurrentUser.User.Role)
                {
                    var appointmentList = _context.InterviewScheduleModels.AsQueryable()
                                         .Where(e => e.IsActive && e.InterviewerId == CurrentUser.User.Id && e.IsReject != true)
                                         .Select(x => x.CandidateId).ToList();
                    candidateListModel = candidateListModel.Where(x => appointmentList.Contains(x.Id)).ToList();
                }
                return candidateListModel;

                // return candidateList.OrderByDescending(x => x.InsertedDate).ToList();
            }
            catch (Exception ex)
            {
                return new List<CandidateModel>();
            }
        }

        public async Task<CandidateModel> GetCandidateByIdAsync(int CandidateId)
        {
            try
            {
                // Fetch the user by UserId
                var candidateModel = await _context.CandidateModels.AsNoTracking()
                    .Where(e => e.IsActive && e.Id == CandidateId)
                    .Include(ut => ut.CandidateTechnologies)
                        .ThenInclude(ut => ut.Technology)
                    .FirstOrDefaultAsync();

                if (candidateModel != null)
                {
                    if (candidateModel.CandidateTechnologies.Count > 0)
                    {
                        // Assign technology ids and models 
                        candidateModel.TechnologyIds = candidateModel.CandidateTechnologies.Select(ut => ut.TechnologyId).ToList();
                        candidateModel.TechnologyModel = candidateModel.CandidateTechnologies.Select(x => x.Technology).ToList();
                    }

                    return candidateModel;
                }
                else
                {
                    throw new InvalidOperationException($"Candidate with ID {CandidateId} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Candidate with ID {CandidateId} not found.");
            }
        }

        public async Task<List<CandidateModel>> GetLastestCandidates()
        {

            try
            {
                var candidateList = await _context.CandidateModels
                                     .Where(e => e.IsActive)
                                     .Include(ct => ct.CandidateTechnologies)
                                        .ThenInclude(ct => ct.Technology)
                                     .OrderByDescending(e => e.InsertedDate)
                                     .Take(3)
                                     .ToListAsync();
                foreach (var candidate in candidateList)
                {
                    candidate.TechnologyIds = candidate.CandidateTechnologies.Select(ut => ut.TechnologyId).ToList();
                    candidate.TechnologyModel = candidate.CandidateTechnologies.Select(ut => ut.Technology).ToList();
                }
                return candidateList;

            }
            catch (Exception ex)
            {
                return new List<CandidateModel>();
            }
        }

        public async Task<List<CandidateChartModel>> GetCandidatesForChart()
        {

            try
            {
                var candidateList = await _context.CandidateModels
                                     .Where(e => e.IsActive)
                                     .Include(ct => ct.CandidateTechnologies)
                                        .ThenInclude(ct => ct.Technology)
                                     .ToListAsync();
                var candidateChartIdCounter = 1;

                var candidateCharts = candidateList
                                        .SelectMany(candidate => candidate.CandidateTechnologies.Select(ut => ut.Technology))
                                        .GroupBy(tech => tech.TechnologyName)
                                        .Select(group => new CandidateChartModel
                                        {
                                            Id = candidateChartIdCounter++,
                                            Label = group.Key,
                                            Value = group.Count()
                                        })
                                        .ToList();
                return candidateCharts;
            }
            catch (Exception ex)
            {
                return new List<CandidateChartModel>();

            }
        }

        public async Task<List<CandidateModel>> GetAppointMentsCandidate()
        {

            try
            {
                var candidateList = await _context.CandidateModels
                   .Where(c => c.IsActive)
                   .Include(sc => sc.StatusModel)
                   .Where(c => c.StatusModel != null && c.StatusModel.DisplayInAppointSchedule)
                   .ToListAsync();
                return candidateList;

            }
            catch (Exception ex)
            {
                return new List<CandidateModel>();
            }
        }
        public async Task<IEnumerable<CandidateModel>> GetFilteredCandidateData(CandidateFilterDto filterParams)
        {
            var query = _context.CandidateModels.AsNoTracking().AsQueryable();

            if (filterParams.TimeFrame.HasValue)
            {
                var fromDate = DateTime.Now.AddDays(-filterParams.TimeFrame.Value);
                query = query.Where(c => c.InsertedDate >= fromDate);
            }

            if (filterParams.FromDate.HasValue)
            {
                query = query.Where(c => c.InsertedDate >= filterParams.FromDate.Value);
            }

            if (filterParams.ToDate.HasValue)
            {
                query = query.Where(c => c.InsertedDate <= filterParams.ToDate.Value);
            }

            if (filterParams.Technologies != null && filterParams.Technologies.Any())
            {
                query = query.Where(c => c.CandidateTechnologies.Any(ct => filterParams.Technologies.Contains(ct.Technology.TechnologyName)));
            }

            if (filterParams.CandidateStatus != null && filterParams.CandidateStatus.Any())
            {
                query = query.Where(c => filterParams.CandidateStatus.Contains(c.StatusModel.StatusName));
            }

            var candidateList = await query
                .Include(c => c.CandidateTechnologies)
                    .ThenInclude(ct => ct.Technology)
                .Include(c => c.StatusModel)
                .ToListAsync();

            //foreach (var candidate in candidateList)
            //{
            //    if (candidate.CandidateTechnologies.Any())
            //    {
            //        candidate.TechnologyIds = candidate.CandidateTechnologies.Select(ct => ct.TechnologyId).ToList();
            //        candidate.TechnologyModel = candidate.CandidateTechnologies.Select(ct => ct.Technology).ToList();
            //    }
            //}

            return candidateList;
        }
        public async Task<bool> RemoveCandidateTechnologies(int candidateId)
        {
            try
            {
                var technologies = _context.TechnologyAssociation.Where(tech => tech.CandidateId == candidateId);
                _context.TechnologyAssociation.RemoveRange(technologies);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CheckEmailAlreadyExist(string email, int id)
        {
            try
            {
                var userCount = await _context.CandidateModels
                    .Where(x => x.IsActive && x.Email == email && x.Id != id)
                    .CountAsync();

                return userCount > 0;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public async Task<InterviewScheduleModel> GetScheduleCandidate(int id)
        {
            try
            {
                var ScheduleCandidate = await _context.InterviewScheduleModels
                                        .Where(c => c.CandidateId == id)
                                        .OrderByDescending(i => i.Id)
                                        .FirstOrDefaultAsync();

                if (ScheduleCandidate != null)
                {
                    ScheduleCandidate.IsReject = true;
                    _context.InterviewScheduleModels.Update(ScheduleCandidate);
                    _context.SaveChanges();
                }
                return ScheduleCandidate;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
