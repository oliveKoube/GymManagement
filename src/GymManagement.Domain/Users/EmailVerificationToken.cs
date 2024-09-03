namespace GymManagement.Domain.Users;

public sealed class EmailVerificationToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime ExpireOnUtc { get; set; }
    public User User { get; set; } = null!;
}