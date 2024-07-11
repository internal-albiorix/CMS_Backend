using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Repository.Interface
{
    public interface IInterviewScheduleRepository
    {
        Task<List<InterviewScheduleModel>> GetAllInterviewSchedule();

        Task<int> CandidateAssignAnyInterview(int candidateId);

        Task<List<InterviewScheduleModel>> GetUpcomingInterviews();
    }
}
