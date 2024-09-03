using GymManagement.Domain.Users;

namespace GymManagement.Application.Common.Interfaces;

public interface IEmailVerificationTokensRepository
{
    Task AddAsync(EmailVerificationToken emailVerificationToken);

    Task<EmailVerificationToken?> VerifyEmail(Guid tokenId);

    Task Remove(EmailVerificationToken emailVerificationToken);
}