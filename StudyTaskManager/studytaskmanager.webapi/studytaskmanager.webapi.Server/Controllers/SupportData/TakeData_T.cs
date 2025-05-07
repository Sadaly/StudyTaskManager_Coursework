using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.WebAPI.Controllers.SupportData
{
    public sealed record TakeData<TFilter, TEntity>(
        int StartIndex,
        int Count,
        TFilter? Filter)
        where TFilter : IEntityFilter<TEntity>
        where TEntity : BaseEntity;
}
