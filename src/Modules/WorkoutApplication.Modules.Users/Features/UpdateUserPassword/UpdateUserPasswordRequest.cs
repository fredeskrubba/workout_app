using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutApplication.Modules.Users.Features.UpdateUserPassword
{
    public record UpdateUserPasswordRequest(string Email, string NewPassword);
}
