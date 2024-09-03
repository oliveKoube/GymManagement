using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Users;
using GymManagement.Domain.Users.Events;
using MediatR;

namespace GymManagement.Application.Authentication.Events;

internal sealed class UserCreatedDomainEventHandler(IEmailService emailService,
    TimeProvider timeProvider,
    IEmailVerificationTokensRepository emailVerificationTokensRepository,
    IUnitOfWork unitOfWork) : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly IEmailService _emailService = emailService;
    private readonly IEmailVerificationTokensRepository _emailVerificationTokensRepository = emailVerificationTokensRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var verificationEmail = new EmailVerificationToken
        {
            Id = Guid.NewGuid(),
            UserId = notification.UserId,
            CreatedOnUtc = timeProvider.GetUtcNow().DateTime,
            ExpireOnUtc = timeProvider.GetUtcNow().AddDays(1).DateTime
        };
        await _emailVerificationTokensRepository.AddAsync(verificationEmail);
        await _unitOfWork.CommitChangesAsync();

        await _emailService.SendVerificationEmailAsync(verificationEmail,notification.Email);
    }
}