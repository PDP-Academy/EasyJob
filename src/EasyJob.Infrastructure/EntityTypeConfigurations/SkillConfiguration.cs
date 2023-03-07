using EasyJob.Domain.Constants;
using EasyJob.Domain.Entities.Skills;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyJob.Infrastructure.EntityTypeConfigurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable(TableName.Skills);

        builder.HasKey(skill => skill.Id);

        builder.Property(skill => skill.Name)
            .IsRequired(true)
            .HasMaxLength(75);
    }
}
