using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Commands.GroupTaskStatusCreate
{
    class GroupTaskStatusCreateCommandHandler : ICommandHandler<GroupTaskStatusCreateCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTaskStatusRepository _groupTaskStatusRepository;

        public GroupTaskStatusCreateCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository, IGroupTaskStatusRepository groupTaskStatusRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _groupTaskStatusRepository = groupTaskStatusRepository;
        }

        public async Task<Result<Guid>> Handle(GroupTaskStatusCreateCommand request, CancellationToken cancellationToken)
        {
            var title = Title.Create(request.Title);
            if (title.IsFailure) return Result.Failure<Guid>(title);

            Group? group = null;
            if (request.GroupId is not null)
            {
                var groupRes = await _groupRepository.GetByIdAsync(request.GroupId.Value, cancellationToken);
                if (groupRes.IsFailure) return Result.Failure<Guid>(groupRes);
                group = groupRes.Value;
            }

            Content? description = null;
            if (request.Description is not null)
            {
                var descriptionRes = Content.Create(request.Description);
                if (descriptionRes.IsFailure) return Result.Failure<Guid>(descriptionRes);
                description = descriptionRes.Value;
            }

            var groupTaskStatus = GroupTaskStatus.Create(title.Value, request.CanBeUpdated, group, description);
            if (groupTaskStatus.IsFailure) return Result.Failure<Guid>(groupTaskStatus);

            var add = await _groupTaskStatusRepository.AddAsync(groupTaskStatus.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<Guid>(add);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(groupTaskStatus.Value.Id);
        }
    }
}
