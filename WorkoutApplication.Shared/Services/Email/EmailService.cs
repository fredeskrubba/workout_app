using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace WorkoutApplication.Shared.Services.Email
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "frederikskrubbeltrangbs@gmail.com";
            var password = "";

            using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(mail, "Workout App"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            client.Send(mailMessage);

            return Task.CompletedTask;
        }
    }
}
