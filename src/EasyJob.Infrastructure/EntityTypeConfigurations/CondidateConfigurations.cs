using EasyJob.Domain.Constants;
using EasyJob.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyJob.Infrastructure.EntityTypeConfigurations;

public class CondidateConfigurations : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable(TableName.Condidate);

        builder.Property(condidate => condidate.ResumeUrl)
            .IsRequired();

        builder
            .HasOne(condidate => condidate.User)
            .WithOne()
            .HasForeignKey<Candidate>(condidate => condidate.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
