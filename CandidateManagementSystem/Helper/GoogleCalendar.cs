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

namespace CandidateManagementSystem.Helper
{
    public  class GoogleCalendar
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Calander Api"; // Your application name

        public CalendarService GetCalendarService()
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName, // Your application name
            });
        }

        public async Task<Event> CreateEventAsync(DateTime startDate, DateTime endDate, string summary)
        {
            var service = GetCalendarService();

            Event newEvent = new Event()
            {
                Summary = summary,
                Start = new EventDateTime()
                {
                    DateTime = startDate,
                    TimeZone = "America/Los_Angeles",
                },
                End = new EventDateTime()
                {
                    DateTime = endDate,
                    TimeZone = "America/Los_Angeles",
                },
            };

            EventsResource.InsertRequest request = service.Events.Insert(newEvent, "primary");
            Event createdEvent = await request.ExecuteAsync();
            return createdEvent;
        }
    }
}



