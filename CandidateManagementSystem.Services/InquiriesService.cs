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
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace CandidateManagementSystem.Services
{
    public class InquiriesService:IInquiriesService
    {
        private readonly ICandidateManagementRepository<InquiriesModel> _repo;
        private readonly IInquiriesRepository _inquiriesRepo;
        public InquiriesService(ICandidateManagementRepository<InquiriesModel> repo,IInquiriesRepository inquiriesRepo)
        {
            _repo = repo;
            _inquiriesRepo = inquiriesRepo;
        }

        public Task<bool> DeleteInquiries(int id)
        {
            try
            {
                return _repo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<InquiriesDto>> GetInquiries()
        {
            try
            {
                var resultModel = await _inquiriesRepo.GetAllInquiries();
                var resultDto = CMSAutoMapper.Mapper.Map<IEnumerable<InquiriesDto>>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<InquiriesDto> GetInquiriesById(int id)
        {
            try
            {
                var resultModel = await _inquiriesRepo.GetInquiriesByIdAsync(id);
                var resultDto = CMSAutoMapper.Mapper.Map<InquiriesDto>(resultModel);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<InquiriesDto> InsertInquiries(InquiriesDto inquiriesDto)
        {
            try
            {
                var inquiriesModel = CMSAutoMapper.Mapper.Map<InquiriesModel>(inquiriesDto);
                inquiriesModel.InsertedDate = DateTime.Now;
                inquiriesModel.InsertedBy = CurrentUser.User.FullName;
                inquiriesModel.InquiriesTechnologies = inquiriesDto.TechnologyIds.Select(techId => new TechnologyAssociation
                { TechnologyId = techId }
                ).ToList();
                var resultModel = await _repo.PostAsync(inquiriesModel);
                var resultDto = CMSAutoMapper.Mapper.Map<InquiriesDto>(resultModel);
                return resultDto;
            }
            catch(Exception ex) 
            { 
                throw ex; 
            }
        }

        public async Task<bool> UpdateInquiries(InquiriesDto inquiriesDto,int id)
        {
            var inquiriesModel = await _repo.GetByIdAsync(id);
            try
            {
                await _inquiriesRepo.RemoveInquiriesTechnologies(id);
                var exisitngInquiries = await _repo.GetByIdAsync(id);
                exisitngInquiries.FullName = inquiriesDto.FullName;
                exisitngInquiries.MobileNumber = inquiriesDto.MobileNumber;
                exisitngInquiries.Email = inquiriesDto.Email;
                exisitngInquiries.InquiriesTechnologies = inquiriesDto.TechnologyIds.Select(techId => new TechnologyAssociation
                {
                    TechnologyId = techId
                }).ToList();
                exisitngInquiries.Experience = inquiriesDto.Experience;
                exisitngInquiries.UpdatedBy = CurrentUser.User.FullName;
                exisitngInquiries.UpdatedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(inquiriesDto.Resume) && inquiriesDto.Resume != "undefined")
                {
                    exisitngInquiries.Resume = inquiriesDto.Resume;
                }
                var data = await _repo.PutAsync(exisitngInquiries, id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
