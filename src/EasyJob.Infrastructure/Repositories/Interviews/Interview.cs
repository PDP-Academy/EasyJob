using EasyJob.Infrastructure.Contexts;

namespace EasyJob.Infrastructure.Repositories.Interviews;

public class Interview : GenericRepository<Interview, Guid>, IInterview
{
	public Interview(AppDbContext appDbContext)
		: base(appDbContext)
	{
	}
}
