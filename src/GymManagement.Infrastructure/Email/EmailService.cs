using FluentEmail.Core;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Users;
using GymManagement.Infrastructure.Users;

namespace GymManagement.Infrastructure.Email;

internal sealed class EmailService(IFluentEmail fluentEmail,
    EmailVerificationLinkFactory emailVerificationLinkFactory) : IEmailService
{
    private readonly IFluentEmail _fluentEmail = fluentEmail;
    private readonly EmailVerificationLinkFactory _emailVerificationLinkFactory = emailVerificationLinkFactory;

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        await _fluentEmail
            .To(email)
            .Subject(subject)
            .Body(message)
            .SendAsync();
    }

    public async Task SendVerificationEmailAsync(EmailVerificationToken emailVerificationToken,string email)
    {
        string verificationLink = _emailVerificationLinkFactory.Create(emailVerificationToken);
        await _fluentEmail
            .To(email)
            .Subject("User created")
            .Body($"Your account has been created <a href='{verificationLink}'>here</a>",isHtml:true)
            .SendAsync();
    }
}