using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Abstractions;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(User user, CancellationToken cancellationToken = default);
}
