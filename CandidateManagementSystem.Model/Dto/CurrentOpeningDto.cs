using System.ComponentModel.DataAnnotations.Schema;
using CandidateManagementSystem.Model.Model;


namespace CandidateManagementSystem.Model.Dto
{
    public class CurrentOpeningDto : Common
    {
        public int Id { get; set; }

        public int DesignationId { get; set; }
        
        public int TechnologyId { get; set; }

        public string Experience { get; set; }

        public int Noofopening { get; set; }
        public string TechnologyName { get; set; }
       
        public string DesignationName { get; set; }
    }
}
