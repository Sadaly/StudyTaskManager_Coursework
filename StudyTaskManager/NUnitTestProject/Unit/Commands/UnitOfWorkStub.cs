using StudyTaskManager.Domain.Abstractions;

namespace NUnitTestProject.Unit.Commands.SystemRole
{
    public class UnitOfWorkStub : IUnitOfWork
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}