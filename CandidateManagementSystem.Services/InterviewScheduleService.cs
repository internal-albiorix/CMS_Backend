using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagementSystem.Services
{
    public class InterviewScheduleService : IInterviewScheduleService
    {
        private readonly ICandidateManagementRepository<InterviewScheduleModel> _repo;
        private readonly IInterviewScheduleRepository _interviewScheduleRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly ICandidateService _candidateService;
        private readonly ICandidateHistoryService _candidateHistoryService;


        public InterviewScheduleService(ICandidateManagementRepository<InterviewScheduleModel> repo, IInterviewScheduleRepository interviewScheduleRepository, IStatusRepository statusRepository,
            ICandidateService candidateService, ICandidateHistoryService candidateHistoryService)
        {
            _repo = repo;
            _interviewScheduleRepository = interviewScheduleRepository;
            _statusRepository = statusRepository;
            _candidateService = candidateService;
            _candidateHistoryService = candidateHistoryService;

        }
        public async Task<InterviewScheduleDto> InsertInterviewSchedule(InterviewScheduleDto interviewScheduleDto)
        {
            try
            {
                var interviewScheduleModel = CMSAutoMapper.Mapper.Map<InterviewScheduleModel>(interviewScheduleDto);
                interviewScheduleModel.InsertedBy = CurrentUser.User.FullName;
                interviewScheduleModel.InsertedDate = DateTime.Now;

                var resultModel = await _repo.PostAsync(interviewScheduleModel);
                var resultDto = CMSAutoMapper.Mapper.Map<InterviewScheduleDto>(resultModel);

                if (interviewScheduleDto.CandidateId != null)
                {
                    var candidateHistory = new CandidateHistoryDto
                    {
                        CandidateId = interviewScheduleDto.CandidateId,
                        InterviewerId = interviewScheduleDto.InterviewerId,
                        InterviewRoundId = interviewScheduleDto.InterviewRoundId,
                        InterviewStartDate = interviewScheduleDto.StartDate
                    };
                    await _candidateHistoryService.InsertCandidateHistory(candidateHistory);
                    var interviewScheduleCount = await _interviewScheduleRepository.CandidateAssignAnyInterview(interviewScheduleDto.CandidateId);
                    if (interviewScheduleCount == 1)
                    {
                        var defaultStatus = await _statusRepository.GetStatusByName("In Progress");
                        var candidate = await _candidateService.GetCandidateById(interviewScheduleDto.CandidateId);
                        var candidateDto = new CandidateDto
                        {
                            Id = candidate.Id,
                            StatusId = defaultStatus.Id,
                            FullName = candidate.FullName,
                            MobileNumber = candidate.MobileNumber,
                            Email = candidate.Email,
                            TechnologyIds = candidate.TechnologyIds,
                            Experience = candidate.Experience
                        };
                        await _candidateService.UpdateCandidate(candidateDto, interviewScheduleDto.CandidateId);
                    }
                }
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<bool> UpdateInterviewSchedule(InterviewScheduleDto interviewScheduleDto, int id)
        {
            try
            {
                var exisitngInterviewSchedule = await _repo.GetByIdAsync(id);
                exisitngInterviewSchedule.Title = interviewScheduleDto.Title;
                exisitngInterviewSchedule.StartDate = interviewScheduleDto.StartDate;
                exisitngInterviewSchedule.EndDate = interviewScheduleDto.EndDate;
                exisitngInterviewSchedule.CandidateId = Convert.ToInt32(interviewScheduleDto.CandidateId);
                exisitngInterviewSchedule.InterviewerId = Convert.ToInt32(interviewScheduleDto.InterviewerId);
                exisitngInterviewSchedule.InterviewRoundId = Convert.ToInt32(interviewScheduleDto.InterviewRoundId);
                exisitngInterviewSchedule.UpdatedBy = CurrentUser.User.FullName;
                exisitngInterviewSchedule.UpdatedDate = DateTime.Now;
                exisitngInterviewSchedule.EventId = interviewScheduleDto.EventId;
                exisitngInterviewSchedule.GoogleMeetLink = interviewScheduleDto.GoogleMeetLink;
                exisitngInterviewSchedule.IsReject = null;
                if (interviewScheduleDto.CandidateId != null)
                {
                    var candidateHistory = new CandidateHistoryDto
                    {
                        CandidateId = Convert.ToInt32(interviewScheduleDto.CandidateId),
                        InterviewerId = Convert.ToInt32(interviewScheduleDto.InterviewerId),
                        InterviewRoundId = Convert.ToInt32(interviewScheduleDto.InterviewRoundId),
                        InterviewStartDate = interviewScheduleDto.StartDate
                    };
                    await _candidateHistoryService.InsertCandidateHistory(candidateHistory);
                }
              
                return await _repo.PutAsync(exisitngInterviewSchedule, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> DeleteInterviewSchedule(int id)
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

        public async Task<IEnumerable<InterviewScheduleModel>> GetInterviewSchedule()
        {
            try
            {
                return await _interviewScheduleRepository.GetAllInterviewSchedule();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<InterviewScheduleModel>> GetUpcomingInterviews()
        {
            try
            {
                return await _interviewScheduleRepository.GetUpcomingInterviews();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
