using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Repository.Interface
{
    public interface IInquiriesRepository
    {
        Task<List<InquiriesModel>> GetAllInquiries();
        Task<InquiriesModel> GetInquiriesByIdAsync(int InquiriesId);
        Task<bool> RemoveInquiriesTechnologies(int inquiriesId);
    }
}
