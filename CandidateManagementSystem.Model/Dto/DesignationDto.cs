using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Model.Dto
{
    public class DesignationDto :Common
    {
        public int Id { get; set; }
        public string DesignationName { get; set; }
        public string DesignationDescription { get; set; }
    }
}
