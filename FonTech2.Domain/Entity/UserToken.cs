using FonTech2.Domain.Interfaces;

namespace FonTech2.Domain.Entity;

#nullable disable

public class UserToken : IEntityId<long>
{
    public long Id { get; set; }
    
    public string RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpireTime { get; set; }
    
    public User User { get; set; }
    
    public long UserId { get; set; }
}