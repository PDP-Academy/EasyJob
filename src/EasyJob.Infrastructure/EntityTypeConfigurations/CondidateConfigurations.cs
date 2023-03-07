using EasyJob.Domain.Constants;
using EasyJob.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyJob.Infrastructure.EntityTypeConfigurations;

public class CondidateConfigurations : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable(TableName.Candidates);

        builder
            .Property(candidate => candidate.ResumeUrl)
            .IsRequired();

        builder
            .HasOne(candidate => candidate.User)
            .WithOne()
            .HasForeignKey<Candidate>(candidate => candidate.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(candidate => candidate.Skills)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
