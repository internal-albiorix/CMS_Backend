using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagementSystem.Model.Model;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystem.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region DbSet Section
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<TechnologyModel> TechnologyModels { get; set; }
        public DbSet<StatusModel> StatusModels { get; set; }
        public DbSet<ReferEmployeeModel> ReferEmployeeModels { get; set; }
        public DbSet<InterviewScheduleModel> InterviewScheduleModels { get; set; }
        public DbSet<InterviewRoundModel> InterviewRoundModels { get; set; }
        public DbSet<FeedBackModel> FeedBackModels { get; set; }
        public DbSet<DesignationModel> DesignationModels { get; set; }
        public DbSet<CurrentOpeningModel> CurrentOpeningModels { get; set; }
        public DbSet<CandidateModel> CandidateModels { get; set; }
        public DbSet<CandidateHistoryModel> CandidateHistoryModels { get; set; }
        public DbSet<CandidateChartModel> CandidateChartModels { get; set; }
        public DbSet<TechnologyAssociation> TechnologyAssociation { get; set; }
        public DbSet<TimeFrameModel> TimeFrameModels { get; set; }
        public DbSet<ForgotPasswordModel> ForgotPasswordModels { get; set; }
        public DbSet<EmailTemplateModel> EmailTemplateModels { get; set; }

        public DbSet<InquiriesModel> InquiriesModels { get; set; }
        public DbSet<EmailLogModel> emailLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatusModel>().HasData(
        new StatusModel {Id=1, StatusName = "New", StatusDescription = "New Candidate register" },
        new StatusModel {Id=2, StatusName = "In Progress", StatusDescription = "Candidate in progress" },
         new StatusModel {Id=3, StatusName = "Selected", StatusDescription = "Candidate is Selected" },
          new StatusModel {Id=4, StatusName = "Rejected", StatusDescription = "Candidate Rejected" },
           new StatusModel {Id=5, StatusName = "Not Fit", StatusDescription = "Candidate expectation not match" }
         );

            modelBuilder.Entity<TimeFrameModel>().HasData(
        new TimeFrameModel { Id = 1, Name="Last 30 days", value=30},
        new TimeFrameModel { Id = 2, Name = "Last 60 days", value = 60 },
         new TimeFrameModel { Id = 3, Name="Last 180 days",value=180 },
          new TimeFrameModel { Id = 4, Name="Last 365 days", value=365}
         );

            modelBuilder.Entity<UserModel>()
               .HasMany(u => u.UserTechnologies)
               .WithOne(ta => ta.User)
               .HasForeignKey(ta => ta.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReferEmployeeModel>()
               .HasMany(u => u.ReferEmployeeTechnologies)
               .WithOne(ta => ta.ReferEmployee)
               .HasForeignKey(ta => ta.ReferEmployeeId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CandidateModel>()
             .HasMany(u => u.CandidateTechnologies)
             .WithOne(ta => ta.Candidate)
             .HasForeignKey(ta => ta.CandidateId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FeedBackModel>()
             .HasMany(u => u.FeedbackTechnologies)
             .WithOne(ta => ta.Feedback)
             .HasForeignKey(ta => ta.FeedbackId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InquiriesModel>()
            .HasMany(u => u.InquiriesTechnologies)
            .WithOne(ta => ta.Inquiries)
            .HasForeignKey(ta => ta.InquiriesId)
            .OnDelete(DeleteBehavior.Cascade);


            #endregion
        }
    }
}
