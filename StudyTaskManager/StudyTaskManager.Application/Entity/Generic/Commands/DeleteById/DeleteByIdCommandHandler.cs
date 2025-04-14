using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions.Repositories.Generic;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Generic.Commands.DeleteById
{
    class DeleteByIdCommandHandler<TEntity> : ICommandHandler<DeleteByIdCommand> where TEntity : BaseEntityWithID
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryWithID<TEntity> _repository;

        public DeleteByIdCommandHandler(IUnitOfWork unitOfWork, IRepositoryWithID<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteByIdCommand request, CancellationToken cancellationToken)
        {
            Result<TEntity> entity = await _repository.GetByIdAsync(request.IdEntity, cancellationToken);
            if (entity.IsFailure) return entity;

            Result delete = await _repository.RemoveAsync(entity.Value, cancellationToken);
            if (delete.IsFailure) return delete;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
