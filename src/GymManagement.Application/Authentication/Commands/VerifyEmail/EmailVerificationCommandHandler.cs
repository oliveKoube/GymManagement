using MediatR;
using ErrorOr;
using GymManagement.Application.Common.Interfaces;

namespace GymManagement.Application.Authentication.Commands.VerifyEmail;

public class EmailVerificationCommandHandler(TimeProvider timeProvider,
    IEmailVerificationTokensRepository emailVerificationTokensRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<EmailVerificationCommand, ErrorOr<bool>>
{
    private readonly TimeProvider _timeProvider = timeProvider;
    private readonly IEmailVerificationTokensRepository _emailVerificationTokensRepository = emailVerificationTokensRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<bool>> Handle(EmailVerificationCommand request, CancellationToken cancellationToken)
    {
        var result = await _emailVerificationTokensRepository.VerifyEmail(request.tokenId);

        if (result is null || result.ExpireOnUtc < _timeProvider.GetUtcNow().DateTime
                           || result.User.EmailVerified)
        {
            return false;
        }
        result.User.EmailVerified = true;

        await _emailVerificationTokensRepository.Remove(result);

        await _unitOfWork.CommitChangesAsync();

        return true;
    }
}