using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagementSystem.Model.Enum;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Data;
using CandidateManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystem.Repository
{
    public class InquiriesRepository:IInquiriesRepository
    {
        private readonly ApplicationDbContext _context;
        public InquiriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<InquiriesModel>> GetAllInquiries()
        {

            try
            {
                var inquiriesListModel = await _context.InquiriesModels
                         .Where(e => e.IsActive)
                         .Include(it => it.InquiriesTechnologies)
                             .ThenInclude(ct => ct.Technology)
                         .OrderByDescending(e => e.InsertedDate)
                         .ToListAsync();
               foreach (var inquiriesModel in inquiriesListModel)
                {
                    inquiriesModel.TechnologyIds = inquiriesModel.InquiriesTechnologies.Select(ut => ut.TechnologyId).ToList();
                }
                return inquiriesListModel;

            }
            catch (Exception ex)
            {
                return new List<InquiriesModel>();
            }
        }

        public async Task<InquiriesModel> GetInquiriesByIdAsync(int InquiriesId)
        {
            try
            {
                // Fetch the user by UserId
                var inquiriesModel = await _context.InquiriesModels
                    .Where(e => e.IsActive && e.Id == InquiriesId)
                    .Include(it => it.InquiriesTechnologies)
                        .ThenInclude(ut => ut.Technology)
                    .FirstOrDefaultAsync();
                if (inquiriesModel != null)
                    return inquiriesModel;
                else
                    throw new InvalidOperationException($"Inquiries with ID {InquiriesId} not found.");
                
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Inquiries with ID {InquiriesId} not found.");
            }
        }
        public async Task<bool> RemoveInquiriesTechnologies(int inquiriesId)
        {
            try
            {
                var technologies = _context.TechnologyAssociation.Where(tech => tech.InquiriesId == inquiriesId);
                _context.TechnologyAssociation.RemoveRange(technologies);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
