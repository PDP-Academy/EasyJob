using EasyJob.Domain.Constants;
using EasyJob.Domain.Entities.Interviews;
using EasyJob.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyJob.Infrastructure.EntityTypeConfigurations;

public class InterviewerConfiguration : IEntityTypeConfiguration<Interviewer>
{
    public void Configure(EntityTypeBuilder<Interviewer> builder)
    {
        builder.ToTable(TableName.Interviewer);

        builder
            .Property(interviewer => interviewer.Description)
            .IsRequired(true);

        builder
            .Property(interviewer => interviewer.Position)
            .IsRequired(true);

        builder
            .Property(interviewer => interviewer.Speciality)
            .IsRequired(true);

        builder
            .Property(interviewer => interviewer.Company)
            .IsRequired(false);

        builder
            .HasOne(interviewer => interviewer.User)
            .WithOne()
            .HasForeignKey<User>(user => user.Id);

        builder
            .HasMany(interviewer => interviewer.Skills)
            .WithOne()
            .HasForeignKey(skill => skill.Id);

    }
}
