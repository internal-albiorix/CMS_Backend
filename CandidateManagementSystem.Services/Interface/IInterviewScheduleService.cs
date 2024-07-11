using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Services.Interface
{
    public interface IInterviewScheduleService
    {
        Task<InterviewScheduleDto> InsertInterviewSchedule(InterviewScheduleDto interviewScheduleDto);
        Task<bool> UpdateInterviewSchedule(InterviewScheduleDto interviewScheduleDto, int id);
        Task<bool> DeleteInterviewSchedule(int id);
        Task<IEnumerable<InterviewScheduleModel>> GetInterviewSchedule();

        Task<IEnumerable<InterviewScheduleModel>> GetUpcomingInterviews();
        
    }
}
