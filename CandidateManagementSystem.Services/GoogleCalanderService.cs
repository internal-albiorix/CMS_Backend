using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CandidateManagementSystem.Services.Interface;
using Google.Apis.Auth.OAuth2.Flows;

namespace CandidateManagementSystem.Services
{
    public class GoogleCalanderService:IGoogleCalanderService
    {
        string[] Scopes = {
            CalendarService.Scope.Calendar,
        };
        static string ApplicationName = "Calander Api"; // Your application name

        private async Task<UserCredential> GetUserCredentialAsync()
        {
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                var googleClientSecrets = GoogleClientSecrets.Load(stream);
                var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = googleClientSecrets.Secrets,
                    Scopes = Scopes,
                    DataStore = new FileDataStore("token.json", true)
                });

                var codeReceiver = new LocalServerCodeReceiver();
                var credential = await new AuthorizationCodeInstalledApp(flow, codeReceiver).AuthorizeAsync("user", CancellationToken.None);
                return credential;
            }
        }
        private async Task<CalendarService> GetCalendarServiceAsync()
        {
            var credential = await GetUserCredentialAsync();
            return new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });
        }

        public async Task<Event> CreateEventAsync(DateTime startDate, DateTime endDate, string summary)
        {
            var ServiceAccountKeyPath = Path.Combine(Directory.GetCurrentDirectory(), "credentials.json");
            var credential = GoogleCredential.FromFile(ServiceAccountKeyPath)
                                             .CreateScoped(Scopes);
           
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            try {
                Event newEvent = new Event()
                {
                    Summary = summary,
                    Start = new EventDateTime()
                    {
                        DateTime = startDate, // Convert to string in the required format
                        TimeZone = "Asia/Kolkata",
                    },
                    End = new EventDateTime()
                    {
                        DateTime = endDate, // Convert to string in the required format
                        TimeZone = "Asia/Kolkata",
                    },
                };

                EventsResource.InsertRequest request = service.Events.Insert(newEvent, "primary");
            Event createdEvent = await request.ExecuteAsync();
                return createdEvent;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
