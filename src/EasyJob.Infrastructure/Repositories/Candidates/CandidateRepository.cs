using EasyJob.Domain.Entities.Users;
using EasyJob.Infrastructure.Contexts;

namespace EasyJob.Infrastructure.Repositories.Candidates;

public sealed class CandidateRepository : GenericRepository<Candidate, Guid>
    , ICandidateRepository
{
    public CandidateRepository(AppDbContext appDbContext)
        : base(appDbContext)
    {
    }
}
