using GymManagement.Domain.Common;

namespace GymManagement.Domain.Admins.Events;

public record SubscriptionDeletedDomainEvent(Guid SubscriptionId) : IDomainEvent;