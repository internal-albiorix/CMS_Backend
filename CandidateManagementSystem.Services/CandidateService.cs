using System.Reflection.Metadata.Ecma335;
using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CandidateManagementSystem.Services
{
    public class CandidateService : ICandidateService
    {
        #region private variables
        private readonly ICandidateManagementRepository<CandidateModel> _repo;
        private readonly ICandidateRepository _candidateRepo;
        private readonly IStatusRepository _statusRepository;
        private readonly ICandidateHistoryService _candidateHistoryService;
       
       
        #endregion

        #region constructure
        public CandidateService(ICandidateManagementRepository<CandidateModel> repo, ICandidateRepository candidateRepo, IStatusRepository statusRepository, ICandidateHistoryService candidateHistoryService)
        {
            _repo = repo;
            _candidateRepo = candidateRepo;
            _statusRepository = statusRepository;
            _candidateHistoryService = candidateHistoryService;
           
        }
        #endregion

        #region methods
        public Task<bool> DeleteCandidate(int id)
        {
            try
            {
                return _repo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task<bool> CheckEmailIsAlreadyExist(string email, int id)
        {
            try
            {
                return _candidateRepo.CheckEmailAlreadyExist(email, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<CandidateDto>> GetCandidate()
        {
            try
            {
                var resultModel = await _candidateRepo.GetAllCandidates();
                var resultDto = resultModel.Select(model => CMSAutoMapper.Mapper.Map<CandidateDto>(model));
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CandidateDto>> GetLatestCandidate()
        {
            try
            {
                var resultModel = await _candidateRepo.GetLastestCandidates();
                var resultDto =resultModel.Select(model=>CMSAutoMapper.Mapper.Map<CandidateDto>(model));
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CandidateDto> GetCandidateById(int id)
        {
            try
            {
                var resultModel = await _candidateRepo.GetCandidateByIdAsync(id);
                var resultDto = CMSAutoMapper.Mapper.Map<CandidateDto>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<CandidateModel>> GetAppointMentsCandidate()
        {
            try
            {
                return await _candidateRepo.GetAppointMentsCandidate();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<CandidateDto>> GetFilteredCandidateData(CandidateFilterDto filterDto)
        {
            try
            {
                
               var resultModel = await _candidateRepo.GetFilteredCandidateData(filterDto);
                var resultDto = CMSAutoMapper.Mapper.Map<IEnumerable<CandidateDto>>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<CandidateDto> InsertCandidate(CandidateDto candidate)
        {
            try
            {
                var candidatemodel = CMSAutoMapper.Mapper.Map<CandidateModel>(candidate); 
                var defaultStatus = await _statusRepository.GetStatusByName("New");
                candidatemodel.StatusModel =  defaultStatus;
               candidatemodel.InsertedBy = CurrentUser.User.FullName;
                candidatemodel.InsertedDate = DateTime.Now;
               
                candidatemodel.CandidateTechnologies= candidate.TechnologyIds.Select(techId => new TechnologyAssociation
                {
                    TechnologyId = techId
                }).ToList();
                var resultModel =  await _repo.PostAsync(candidatemodel);
                var resultDto = CMSAutoMapper.Mapper.Map<CandidateDto>(resultModel);
                var candidateHistorymodel = new CandidateHistoryDto
                {
                    CandidateId = resultDto.Id,
                    StatusId = resultDto.StatusId
                };
                await _candidateHistoryService.InsertCandidateHistory(candidateHistorymodel);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateCandidate(CandidateDto candidate, int id)
        {
            try
            {
                await _candidateRepo.RemoveCandidateTechnologies(id);
                var exisitngCandidate = await _repo.GetByIdAsync(id);
                if (exisitngCandidate.StatusId != candidate.StatusId)
                {
                    var candidateHistorymodel = new CandidateHistoryDto
                    {
                        CandidateId = exisitngCandidate.Id,
                        StatusId = candidate.StatusId
                    };
                    await _candidateHistoryService.InsertCandidateHistory(candidateHistorymodel);
                }
                exisitngCandidate.FullName = candidate.FullName;
                exisitngCandidate.MobileNumber = candidate.MobileNumber;
                exisitngCandidate.Email = candidate.Email;
                exisitngCandidate.CandidateTechnologies = candidate.TechnologyIds.Select(techId => new TechnologyAssociation
                {
                    TechnologyId = techId
                }).ToList();
                exisitngCandidate.Experience = candidate.Experience;
                exisitngCandidate.StatusId= candidate.StatusId;
                exisitngCandidate.UpdatedBy = CurrentUser.User.FullName;
                exisitngCandidate.UpdatedDate = DateTime.Now;
                if(!string.IsNullOrEmpty(candidate.Resume) && candidate.Resume != "undefined")
                {
                    exisitngCandidate.Resume = candidate.Resume;
                }
                var data = await _repo.PutAsync(exisitngCandidate, id);
                
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CandidateChartModel>> GetCandidateForChart()
        {
            try
            {
                return await _candidateRepo.GetCandidatesForChart();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RejectInterviewSchedule(int id)
        {
            var ScheduleCandidate = await _candidateRepo.GetScheduleCandidate(id);

            if (ScheduleCandidate != null)
            {
                var candidateHistory = new CandidateHistoryDto
                {
                    CandidateId = Convert.ToInt32(ScheduleCandidate.CandidateId),
                    InterviewerId = Convert.ToInt32(ScheduleCandidate.InterviewerId),
                    InterviewRoundId = Convert.ToInt32(ScheduleCandidate.InterviewRoundId),
                    InterviewStartDate = ScheduleCandidate.StartDate,
                    UpdatedDate= DateTime.Now,
                    Message = $"{ScheduleCandidate.InterviewerModel.FullName} Is Not Available On {DateTime.Now}"
                };
                await _candidateHistoryService.InsertCandidateHistory(candidateHistory);
            }
            return true;
        }
        #endregion
    }
}
