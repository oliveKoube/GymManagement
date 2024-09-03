using MediatR;
using ErrorOr;

namespace GymManagement.Application.Authentication.Commands.VerifyEmail;

public record EmailVerificationCommand(Guid tokenId) : IRequest<ErrorOr<bool>>;