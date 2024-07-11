using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Model.Dto
{
    public class StatusDto : Common
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public string? StatusDescription { get; set; }
        public bool DisplayInAppointSchedule { get; set; }
    }
}
