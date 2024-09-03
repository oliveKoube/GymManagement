using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Users;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Users;

public class EmailVerificationTokensRepository(GymManagementDbContext dbContext) : IEmailVerificationTokensRepository
{
    private readonly GymManagementDbContext _dbContext = dbContext;

    public async Task AddAsync(EmailVerificationToken user)
    {
        await _dbContext.AddAsync(user);
    }

    public async Task<EmailVerificationToken?> VerifyEmail(Guid tokenId)
    {
        return await _dbContext.EmailVerificationTokens
            .Include(o => o.User)
            .FirstOrDefaultAsync(e => e.Id == tokenId);
    }

    public async Task Remove(EmailVerificationToken emailVerificationToken)
    {
        _dbContext.EmailVerificationTokens.Remove(emailVerificationToken);
        await Task.CompletedTask;
    }
}