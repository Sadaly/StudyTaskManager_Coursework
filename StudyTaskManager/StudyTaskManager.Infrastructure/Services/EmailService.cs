using StudyTaskManager.Application.Abstractions;
using StudyTaskManager.Domain.Entity.User;

namespace Gatherly.Infrastructure.Services;

internal sealed class EmailService : IEmailService
{
    public Task SendWelcomeEmailAsync(User user, CancellationToken cancellationToken = default) =>
        Task.CompletedTask;
}
