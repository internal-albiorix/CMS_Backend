using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Model.Dto
{
    public class TechnologyDto  : Common
    {
        public int Id { get; set; }
        public string TechnologyName { get; set; }
        public string TechnologyDescription { get; set; }
    }
}
