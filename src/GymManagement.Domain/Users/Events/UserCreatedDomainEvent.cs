using GymManagement.Domain.Common;

namespace GymManagement.Domain.Users.Events;

public record UserCreatedDomainEvent(Guid UserId,string Email) : IDomainEvent;