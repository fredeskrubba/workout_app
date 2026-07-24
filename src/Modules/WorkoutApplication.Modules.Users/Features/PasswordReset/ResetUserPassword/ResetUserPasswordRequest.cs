namespace WorkoutApplication.Modules.Users.Features.PasswordReset.ResetUserPassword
{
    public record ResetUserPasswordRequest(string Email, string NewPassword);
}
