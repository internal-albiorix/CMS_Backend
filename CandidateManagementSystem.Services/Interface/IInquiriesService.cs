using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagementSystem.Model.Dto;

namespace CandidateManagementSystem.Services.Interface
{
    public interface IInquiriesService
    {
        Task<IEnumerable<InquiriesDto>> GetInquiries();
        Task<InquiriesDto> GetInquiriesById(int id);
        Task<InquiriesDto> InsertInquiries(InquiriesDto inquiriesDto);
        Task<bool> UpdateInquiries(InquiriesDto inquiriesDto, int id);
        Task<bool> DeleteInquiries(int id);
    }
}
