using EasyJob.Domain.Constants;
using EasyJob.Domain.Entities.Interviews;
using EasyJob.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyJob.Infrastructure.EntityTypeConfigurations;

public class InterviewConfiguration : IEntityTypeConfiguration<Interview>
{
    public void Configure(EntityTypeBuilder<Interview> builder)
    {
        builder.ToTable(TableName.Interviews)
            .HasKey(interview => interview.Id);

        builder.Property(interview => interview.InterviewUrl)
            .IsRequired(true);

        builder.Property(interview => interview.Title)
            .HasMaxLength(50)
            .IsRequired(true);

        builder.Property(interview => interview.StartsAt)
            .IsRequired(true);
        
        builder.Property(interview => interview.EndsAt)
            .IsRequired(true);
        
        builder.Property(interview => interview.RecordingUrl)
            .IsRequired(false);
        
        builder.Property(interview => interview.Feedback)
            .IsRequired(false);
        
        builder.Property(interview => interview.Score)
            .IsRequired(false);

        builder.HasOne(interview => interview.Candidate)
            .WithMany()
            .HasForeignKey(interview => interview.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(interview => interview.Interviewer)
            .WithMany()
            .HasForeignKey(interview => interview.InterviewerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
