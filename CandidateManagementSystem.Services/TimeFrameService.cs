using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository.Mapper;
using CandidateManagementSystem.Services.Interface;

namespace CandidateManagementSystem.Services
{
    public class TimeFrameService:ITimeFrameService
    {
        private readonly ICandidateManagementRepository<TimeFrameModel> _repo;

        public TimeFrameService(ICandidateManagementRepository<TimeFrameModel> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TimeFrameDto>> GetTimeFrame()
        {
            try
            {
                var resultModel = await _repo.GetAll();
                var resultDto = CMSAutoMapper.Mapper.Map<List<TimeFrameDto>>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
