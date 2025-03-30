using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Abstractions
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
