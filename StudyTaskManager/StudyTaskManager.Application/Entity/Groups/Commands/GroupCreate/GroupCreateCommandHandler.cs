using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupCreate
{
    public class GroupCreateCommandHandler : ICommandHandler<GroupCreateCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupRoleRepository _groupRoleRepository;
		private readonly IUserInGroupRepository _userInGroupRepository;
        private IUserRepository _userRepository;

        public GroupCreateCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository, IGroupRoleRepository groupRoleRepository, IUserInGroupRepository userInGroupRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _groupRoleRepository = groupRoleRepository;
            _userInGroupRepository = userInGroupRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(GroupCreateCommand request, CancellationToken cancellationToken)
        {
            var foundGroup = await _groupRepository.GetAllAsync(g=> g.Title.Value == request.Title,cancellationToken);
            if (foundGroup.Value.Count != 0 ) return Result.Failure<Guid>(PersistenceErrors.Group.AlreadyExists);

            var title = Title.Create(request.Title);
            if (title.IsFailure) return Result.Failure<Guid>(title);

            Content? description = null;
            if (request.Description is not null)
            {
                var descriptionRes = Content.Create(request.Description);
                if (descriptionRes.IsFailure) return Result.Failure<Guid>(descriptionRes);
                description = descriptionRes.Value;
            }
            var baseRoles = await _groupRoleRepository.GetBaseAsync(cancellationToken);
            if (baseRoles.IsFailure) return Result.Failure<Guid>(baseRoles);

            var memberRole = baseRoles.Value.FirstOrDefault(r => r.Title.Value == "Участник");
            var creatorRole = baseRoles.Value.FirstOrDefault(r => r.Title.Value == "Создатель");

            if(memberRole == null || creatorRole == null) return Result.Failure<Guid>(PersistenceErrors.SystemRole.NotFound);

            var group = Group.Create(title.Value, description, memberRole);
            if (group.IsFailure) return Result.Failure<Guid>(group);

            var add = await _groupRepository.AddAsync(group.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<Guid>(add);

            var user = await _userRepository.GetByIdAsync(request.CreatorId);
            if (user.IsFailure) return Result.Failure<Guid>(user);

            var uig = UserInGroup.Create(group.Value, user.Value, creatorRole);
            if (uig.IsFailure) return Result.Failure<Guid>(uig);

            var addUIG = await _userInGroupRepository.AddAsync(uig.Value, cancellationToken);
            if (addUIG.IsFailure) return Result.Failure<Guid>(addUIG);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(group.Value.Id);
        }
    }
}
