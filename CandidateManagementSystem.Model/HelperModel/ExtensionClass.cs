using System.Globalization;

namespace CandidateManagementSystem.Model.HelperModel
{
    public static class ExtensionClass
    {
        public static string ToDate(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                DateTime inputDate = DateTime.Parse(value, null, DateTimeStyles.RoundtripKind);
                TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                // Ensure inputDate is in UTC
                if (inputDate.Kind != DateTimeKind.Utc)
                {
                    inputDate = inputDate.ToUniversalTime();
                }

                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(inputDate, indianTimeZone);
                string indianDateTimeFormat = indianTime.ToString("dd-MM-yyyy hh:mm tt");
                return indianDateTimeFormat;
            }
            return value;
        }
    }
}
