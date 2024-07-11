namespace CandidateManagementSystem.Model.Model
{
    public class Common
    {
       
        public bool IsActive { get; set; } = true;

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string InsertedBy { get; set; }

        public DateTime? InsertedDate { get; set; }
    }
}
