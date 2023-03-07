using Microsoft.EntityFrameworkCore;

namespace EasyJob.Domain.Entities.Users;

[Keyless]
public class Admin
{
    public Guid UserId { get; }
    public User User { get; }
}