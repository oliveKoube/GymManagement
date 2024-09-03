using GymManagement.Domain.Users;

namespace GymManagement.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message);
    Task SendVerificationEmailAsync(EmailVerificationToken emailVerificationToken, string email);
}