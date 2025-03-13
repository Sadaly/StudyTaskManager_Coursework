using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Persistence
{
    public class ApplicationDBContext : DbContext, IUnitOfWork
	{

	}
}
