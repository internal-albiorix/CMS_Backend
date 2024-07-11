using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Services.Interface
{
    public interface ITimeFrameService
    {
        Task<IEnumerable<TimeFrameDto>> GetTimeFrame();
    }
}
