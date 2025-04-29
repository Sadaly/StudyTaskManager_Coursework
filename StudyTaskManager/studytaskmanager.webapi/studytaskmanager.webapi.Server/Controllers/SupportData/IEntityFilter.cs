using StudyTaskManager.Domain.Common;
using System.Linq.Expressions;

namespace StudyTaskManager.WebAPI.Controllers.SupportData
{
    public interface IEntityFilter<TEntity> where TEntity : BaseEntity
    {
        Expression<Func<TEntity, bool>> ToPredicate();
    }
}