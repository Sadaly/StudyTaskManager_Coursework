using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Commands.GroupTaskUpdateCreate
{
    class GroupTaskUpdateCreateCommandHandler : ICommandHandler<GroupTaskUpdateCreateCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IGroupTaskRepository _groupTaskRepository;
        private readonly IGroupTaskUpdateRepository _groupTaskUpdateRepository;

        public GroupTaskUpdateCreateCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IGroupTaskRepository groupTaskRepository, IGroupTaskUpdateRepository groupTaskUpdateRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _groupTaskRepository = groupTaskRepository;
            _groupTaskUpdateRepository = groupTaskUpdateRepository;
        }

        public async Task<Result<Guid>> Handle(GroupTaskUpdateCreateCommand request, CancellationToken cancellationToken)
        {
            var content = Content.Create(request.Content);
            if (content.IsFailure) return Result.Failure<Guid>(content);

            var creator = await _userRepository.GetByIdAsync(request.CreatorId, cancellationToken);
            if (creator.IsFailure) return Result.Failure<Guid>(creator);

            var groupTask = await _groupTaskRepository.GetByIdAsync(request.TaskId, cancellationToken);
            if (groupTask.IsFailure) return Result.Failure<Guid>(groupTask);

            var groupTaskUpdate = GroupTaskUpdate.Create(creator.Value, groupTask.Value, content.Value);
            if (groupTaskUpdate.IsFailure) return Result.Failure<Guid>(groupTaskUpdate);

            var add = await _groupTaskUpdateRepository.AddAsync(groupTaskUpdate.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<Guid>(add);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(groupTaskUpdate.Value.Id);
        }
    }
}
