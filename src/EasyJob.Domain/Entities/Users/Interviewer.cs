using EasyJob.Domain.Entities.Skills;

namespace EasyJob.Domain.Entities.Users;

public class Interviewer
{
    public string Description { get; set; }
    public string Position { get; set; }
    public string Speciality { get; set; }
    public string? Company { get; set; }
    public Guid UserId { get; }
    public User User { get; }

    public ICollection<Skill> Skills { get; set; }
}