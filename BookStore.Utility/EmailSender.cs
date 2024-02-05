using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
namespace BookStore.Utility
{
    public class EmailSender(IConfiguration _config) : IEmailSender
    {

        public string Gmail { get; set; } = _config.GetValue<string>("GmailAccount:Gmail");
        public string Password { get; set; } = _config.GetValue<string>("GmailAccount:Password");

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(Gmail, Password);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(Gmail);
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = htmlMessage;
            smtpClient.Send(mailMessage);
            return Task.CompletedTask;

        }
    }
}
