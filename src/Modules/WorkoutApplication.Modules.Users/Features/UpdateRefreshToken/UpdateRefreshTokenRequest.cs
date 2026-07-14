using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutApplication.Modules.Users.Features.UpdateRefreshToken
{
    public record UpdateRefreshTokenRequest(int UserId, string RefreshToken);

}
