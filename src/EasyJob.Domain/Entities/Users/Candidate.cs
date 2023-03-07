using EasyJob.Domain.Entities.Skills;

namespace EasyJob.Domain.Entities.Users;

public class Candidate
{
    public string ResumeUrl { get; set; }
    public Guid UserId { get; }
    public User User { get; }
    public double? LastScore { get; set; }

    public ICollection<Skill> Skills { get; set; }
}