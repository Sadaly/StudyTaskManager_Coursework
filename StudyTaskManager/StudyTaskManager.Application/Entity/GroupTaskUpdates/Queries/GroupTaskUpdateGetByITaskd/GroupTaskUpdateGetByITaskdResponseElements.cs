using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Queries.GroupTaskUpdateGetByITaskd;

public sealed record GroupTaskUpdateGetByITaskdResponseElements(
    Guid Id,
    DateTime DateCreated,
    string Content)
{
    internal GroupTaskUpdateGetByITaskdResponseElements(GroupTaskUpdate update)
        : this(
             update.Id,
             update.DateCreated,
             update.Content.Value)
    { }
}