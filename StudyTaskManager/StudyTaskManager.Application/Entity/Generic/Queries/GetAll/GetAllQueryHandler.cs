using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories.Generic;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Generic.Queries.GetAll
{
    class GetAllQueryHandler<TEntity> : IQueryHandler<GetAllQuery<TEntity>, List<TEntity>> where TEntity : BaseEntity
    {
        private readonly IRepository<TEntity> _repository;

        public GetAllQueryHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<TEntity>>> Handle(GetAllQuery<TEntity> request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
