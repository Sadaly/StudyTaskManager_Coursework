using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupCreate
{
    public class GroupCreateCommandHandler : ICommandHandler<GroupCreateCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupRoleRepository _groupRoleRepository;

        public GroupCreateCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository, IGroupRoleRepository groupRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _groupRoleRepository = groupRoleRepository;
        }

        public async Task<Result<Guid>> Handle(GroupCreateCommand request, CancellationToken cancellationToken)
        {
            var title = Title.Create(request.Title);
            if (title.IsFailure) return Result.Failure<Guid>(title);

            Content? description = null;
            if (request.Description is not null)
            {
                var descriptionRes = Content.Create(request.Description);
                if (descriptionRes.IsFailure) return Result.Failure<Guid>(descriptionRes);
                description = descriptionRes.Value;
            }

            var defaultRole = await _groupRoleRepository.GetByIdAsync(request.DefaultRoleId, cancellationToken);
            if (defaultRole.IsFailure) return Result.Failure<Guid>(defaultRole);

            var group = Group.Create(title.Value, description, defaultRole.Value);
            if (group.IsFailure) return Result.Failure<Guid>(group);

            var add = await _groupRepository.AddAsync(group.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<Guid>(add);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(group.Value.Id);
        }
    }
}
