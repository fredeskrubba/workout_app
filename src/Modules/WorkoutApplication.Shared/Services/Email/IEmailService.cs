using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutApplication.Shared.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
