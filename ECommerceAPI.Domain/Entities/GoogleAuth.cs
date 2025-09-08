using ECommerceAPI.Domain.Entities;

public class GoogleAuth
{
    public int Id { get; set; }

    public Guid UserId { get; set; }  // FK
    public User User { get; set; }    // Navigation property

    public string GoogleIdToken { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
