using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;

namespace CandidateManagementSystem.Services.Interface
{
    public interface IGoogleCalanderService
    {
        Task<Event> CreateEventAsync(DateTime startDate, DateTime endDate, string summary);
    }
}
