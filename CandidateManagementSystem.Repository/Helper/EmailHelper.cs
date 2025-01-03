using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using CandidateManagementSystem.Repository.Helper.Interface;
using CandidateManagementSystem.Model.Model;
using CandidateManagementSystem.Repository.Data;


namespace CandidateManagementSystem.Helper
{
    public  class EmailHelper:IEmailHelper
    {
        private readonly SmtpClient _smtpClient;
        private  IConfiguration _configuration;
        private ApplicationDbContext _context;

        public  EmailHelper(IConfiguration configuration,ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;

            _smtpClient = new SmtpClient(_configuration["SmtpSettings:Host"])
            {
                Port = int.Parse(_configuration["SmtpSettings:Port"]),
                Credentials = new NetworkCredential(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]),
                EnableSsl = true,
            };
        }
        public bool SentEmail(List<string> toEmails, string subject, string body)
        {
            var fromAddress = new MailAddress(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Name"]);
            var emailLog = new EmailLogModel
            {
                SentDate = DateTime.Now,
                Status = "Success",
                Recipient = string.Join(",", toEmails)
            };
            try
            {
                var message = new MailMessage
                {
                    From = fromAddress,
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                foreach (var email in toEmails)
                {
                    message.To.Add(new MailAddress(email));
                }
                _smtpClient.Send(message);
                return true;
            }
            catch (SmtpException ex)
            {
                emailLog.Status = "Failed";
                emailLog.ErrorMessage = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                emailLog.Status = "Failed";
                emailLog.ErrorMessage = ex.Message;
                return false;
            }
            finally
            {
                _context.emailLogs.Add(emailLog);
                _context.SaveChanges();
            }
        }

    }

}



