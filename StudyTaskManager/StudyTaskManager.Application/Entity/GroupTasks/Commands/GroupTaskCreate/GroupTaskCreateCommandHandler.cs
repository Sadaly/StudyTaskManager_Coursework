using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.GroupTasks.Commands.GroupTaskCreate
{
    class GroupTaskCreateCommandHandler : ICommandHandler<GroupTaskCreateCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTaskStatusRepository _groupTaskStatusRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupTaskRepository _groupTaskRepository;

        public GroupTaskCreateCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository, IGroupTaskStatusRepository groupTaskStatusRepository, IUserRepository userRepository, IGroupTaskRepository groupTaskRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _groupTaskStatusRepository = groupTaskStatusRepository;
            _userRepository = userRepository;
            _groupTaskRepository = groupTaskRepository;
        }

        public async Task<Result<Guid>> Handle(GroupTaskCreateCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return Result.Failure<Guid>(group);

            var title = Title.Create(request.HeadLine);
            if (title.IsFailure) return Result.Failure<Guid>(title);

            var status = await _groupTaskStatusRepository.GetByIdAsync(request.StatusId, cancellationToken);
            if (status.IsFailure) return Result.Failure<Guid>(status);

            Content? description = null;
            Domain.Entity.User.User? responsibleUser = null;
            GroupTask? parentTask = null;

            if (request.Description is not null)
            {
                var descriptionRes = Content.Create(request.Description);
                if (descriptionRes.IsFailure) return Result.Failure<Guid>(descriptionRes);
                description = descriptionRes.Value;
            }

            if (request.ResponsibleUserId is not null)
            {
                var responsibleUserRes = await _userRepository.GetByIdAsync(request.ResponsibleUserId.Value, cancellationToken);
                if (responsibleUserRes.IsFailure) return Result.Failure<Guid>(responsibleUserRes);
                responsibleUser = responsibleUserRes.Value;
            }

            if (request.ParentTaskId is not null)
            {
                var parentTaskRes = await _groupTaskRepository.GetByIdAsync(request.ParentTaskId.Value, cancellationToken);
                if (parentTaskRes.IsFailure) return Result.Failure<Guid>(parentTaskRes);
                parentTask = parentTaskRes.Value;
            }

            var task = GroupTask.Create(group.Value, request.Deadline, status.Value, title.Value, description, responsibleUser, parentTask);
            if (task.IsFailure) return Result.Failure<Guid>(task);

            var add = await _groupTaskRepository.AddAsync(task.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<Guid>(add);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(task.Value.Id);
        }
    }
}
