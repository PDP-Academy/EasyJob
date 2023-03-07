using EasyJob.Domain.Entities.Common;

namespace EasyJob.Domain.Entities.Skills;

public class Skill : Auditable
{
    public string Name { get; set; }
}