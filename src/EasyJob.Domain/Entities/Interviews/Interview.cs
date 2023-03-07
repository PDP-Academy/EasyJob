using EasyJob.Domain.Entities.Common;
using EasyJob.Domain.Entities.Users;

namespace EasyJob.Domain.Entities.Interviews;

public class Interview : Auditable
{
    public string Title { get; set; }
    public string? RecordingUrl { get; set; }
    public string? Feedback { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public double? Score { get; set; }
    public Guid CandidateId { get; }
    public Candidate Candidate { get; }
    public Guid InterviewerId { get; }
    public Interviewer Interviewer { get; }
    public string InterviewUrl { get; set; }
}