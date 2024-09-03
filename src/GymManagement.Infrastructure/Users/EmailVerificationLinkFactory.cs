using GymManagement.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace GymManagement.Infrastructure.Users;

internal sealed class EmailVerificationLinkFactory(IHttpContextAccessor httpContextAccessor,
    LinkGenerator linkGenerator)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly LinkGenerator _linkGenerator = linkGenerator;
    private const string Authentication = "Authentication";
    private const string VerifyEmail = "verifyEmail";

    public string Create(EmailVerificationToken emailVerificationToken)
    {
        string? verficationLink = _linkGenerator.GetPathByAction(_httpContextAccessor.HttpContext!,
            controller:Authentication,action:VerifyEmail,values: new { token = emailVerificationToken.Id } );

        return verficationLink ?? throw new NullReferenceException("verificationLink is null");
    }
}