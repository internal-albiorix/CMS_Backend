
using AutoMapper;
using CandidateManagementSystem.Model.Dto;
using CandidateManagementSystem.Model.Model;

namespace CandidateManagementSystem.Repository.Mapper
{
    public class CMSModelMapper : Profile
    {
        public CMSModelMapper()
        {

            CreateMap<TechnologyDto, TechnologyModel>().ReverseMap();
            CreateMap<InterviewRoundDto, InterviewRoundModel>().ReverseMap();
            CreateMap<DesignationDto, DesignationModel>().ReverseMap();
            CreateMap<StatusDto, StatusModel>().ReverseMap();
            CreateMap<CurrentOpeningDto, CurrentOpeningModel>().ReverseMap();
            CreateMap<UserDto, UserModel>().ReverseMap();
            CreateMap<CandidateDto, CandidateModel>().ReverseMap();
            CreateMap<CandidateHistoryDto, CandidateHistoryModel>().ReverseMap();
            CreateMap<ReferEmployeeDto, ReferEmployeeModel>().ReverseMap();
            CreateMap<InterviewScheduleDto, InterviewScheduleModel>().ReverseMap();
            CreateMap<FeedBackDto, FeedBackModel>().ReverseMap();
            CreateMap<TimeFrameDto, TimeFrameModel>().ReverseMap();
            CreateMap<EmailTemplateDto, EmailTemplateModel>().ReverseMap();
            CreateMap<InquiriesDto, InquiriesModel>().ReverseMap();
            CreateMap<EmailLogDto, EmailLogModel>().ReverseMap();
        }
    }
    public static class CMSAutoMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<CMSModelMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }
}

