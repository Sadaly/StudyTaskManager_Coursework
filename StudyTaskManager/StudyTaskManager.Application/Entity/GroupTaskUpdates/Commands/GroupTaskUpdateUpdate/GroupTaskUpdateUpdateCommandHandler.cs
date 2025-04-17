using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Commands.GroupTaskUpdateUpdate
{
    class GroupTaskUpdateUpdateCommandHandler : ICommandHandler<GroupTaskUpdateUpdateCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupTaskUpdateRepository _groupTaskUpdateRepository;

        public GroupTaskUpdateUpdateCommandHandler(IUnitOfWork unitOfWork, IGroupTaskUpdateRepository groupTaskUpdateRepository)
        {
            _unitOfWork = unitOfWork;
            _groupTaskUpdateRepository = groupTaskUpdateRepository;
        }

        public async Task<Result> Handle(GroupTaskUpdateUpdateCommand request, CancellationToken cancellationToken)
        {
            var newContent = Content.Create(request.NewContent);
            if (newContent.IsFailure) return Result.Failure(newContent);

            var groupTaskUpdate = await _groupTaskUpdateRepository.GetByIdAsync(request.GroupTaskUpdateId, cancellationToken);
            if (groupTaskUpdate.IsFailure) return Result.Failure(groupTaskUpdate);

            var updateContent = groupTaskUpdate.Value.UpdateContent(newContent.Value);
            if (updateContent.IsFailure) return Result.Failure(updateContent);

            var updateDB = await _groupTaskUpdateRepository.UpdateAsync(groupTaskUpdate.Value, cancellationToken);
            if (updateDB.IsFailure) return Result.Failure(updateDB);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
