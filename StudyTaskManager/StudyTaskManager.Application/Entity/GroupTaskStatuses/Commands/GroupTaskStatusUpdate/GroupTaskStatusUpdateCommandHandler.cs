using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Commands.GroupTaskStatusUpdate
{
    class GroupTaskStatusUpdateCommandHandler : ICommandHandler<GroupTaskStatusUpdateCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupTaskStatusRepository _groupTaskStatusRepository;

        public GroupTaskStatusUpdateCommandHandler(IUnitOfWork unitOfWork, IGroupTaskStatusRepository groupTaskStatusRepository)
        {
            _unitOfWork = unitOfWork;
            _groupTaskStatusRepository = groupTaskStatusRepository;
        }

        public async Task<Result> Handle(GroupTaskStatusUpdateCommand request, CancellationToken cancellationToken)
        {
            var groupTaskStatus = await _groupTaskStatusRepository.GetByIdAsync(request.GroupTaskStatusId, cancellationToken);
            if (groupTaskStatus.IsFailure) return groupTaskStatus;

            Title? newName = null;
            bool? newCanBeUpdated = request.NewCanBeUpdated;
            Content? newDescription = null;

            if (request.NewName is not null)
            {
                var newNameRes = Title.Create(request.NewName);
                if (newNameRes.IsFailure) return newNameRes;
                newName = newNameRes.Value;
            }

            if (request.NewDescription is not null)
            {
                var newDescriptionRes = Content.Create(request.NewDescription);
                if (newDescriptionRes.IsFailure) return newDescriptionRes;
                newDescription = newDescriptionRes.Value;
            }

            var updateGTS = groupTaskStatus.Value.Update(newName, newCanBeUpdated, newDescription);
            if (updateGTS.IsFailure) return updateGTS;

            var updateDB = await _groupTaskStatusRepository.UpdateAsync(groupTaskStatus.Value, cancellationToken);
            if (updateDB.IsFailure) return updateDB;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
