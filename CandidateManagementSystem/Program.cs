
using CandidateManagementSystem.Helper;
using CandidateManagementSystem.Model.HelperModel;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CandidateManagementSystem.Repository.Interface;
using CandidateManagementSystem.Repository;
using CandidateManagementSystem.Repository.Helper.Interface;
using CandidateManagementSystem.Repository.Helper;
using CandidateManagementSystem.Services.Interface;
using CandidateManagementSystem.Services;
using CandidateManagementSystem.Middleware;
using Microsoft.EntityFrameworkCore;
using CandidateManagementSystem.Repository.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddEndpointsApiExplorer().AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Candidate management system-APIs", Version = "v1" });
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });
    c.OperationFilter<AuthOperationFilter>();
});

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var JwtSettings = builder.Configuration.GetSection("JwtTokenSetting").Get<JwtTokenSetting>();
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = JwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = JwtSettings.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SigningKey))
    };
});
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AutomaticAuthentication = false;
});
#region register repository
builder.Services.AddScoped(typeof(ICandidateManagementRepository<>), typeof(CandidateManagementRepository<>));
builder.Services.AddScoped(typeof(ICurrentOpeningRepository), typeof(CurrentOpeningRepository));
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IEmailHelper, EmailHelper>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<IReferEmployeeRepository, ReferEmployeeRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<ICandidateHistoryRepository, CandidateHistoryRepository>();
builder.Services.AddScoped<IInterviewScheduleRepository, InterviewScheduleRepository>();
builder.Services.AddScoped<IFeedBackRepository, FeedBackRepository>();
builder.Services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
builder.Services.AddScoped<IInquiriesRepository, InquiriesRepository>();
#endregion

#region register service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITechnologyService, TechnologyService>();
builder.Services.AddScoped<IInterviewRoundService, InterviewRoundService>();
builder.Services.AddScoped<IDesignationService, DesignationService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<ICandidateHistoryService, CandidateHistoryService>();
builder.Services.AddScoped<ICurrentOpeningService, CurrentOpeningService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<IReferemployeeService, ReferemployeeService>();
builder.Services.AddScoped<IInterviewScheduleService, InterviewScheduleService>();
builder.Services.AddScoped<IFeedBackService, FeedBackService>();
builder.Services.AddScoped<ITimeFrameService, TimeFrameService>();
builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
builder.Services.AddScoped<IInquiriesService, InquiriesService>();
builder.Services.AddScoped<IGoogleCalanderService, GoogleCalanderService>();
#endregion



//Add Db service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<JwtMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder =>
{
    builder
    .WithOrigins("http://localhost:3000", "https://cmsalbiorix.web.app")
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.Run();
