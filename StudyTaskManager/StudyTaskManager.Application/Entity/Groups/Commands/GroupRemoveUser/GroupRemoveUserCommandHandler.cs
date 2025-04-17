using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupRemoveUser
{
    class GroupRemoveUserCommandHandler : ICommandHandler<GroupRemoveUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInGroupRepository _userInGroupRepository;

        public GroupRemoveUserCommandHandler(IUnitOfWork unitOfWork, IUserInGroupRepository userInGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _userInGroupRepository = userInGroupRepository;
        }

        public async Task<Result> Handle(GroupRemoveUserCommand request, CancellationToken cancellationToken)
        {
            var userInGroup = await _userInGroupRepository.GetByUserAndGroupAsync(request.UserId, request.GroupId, cancellationToken);
            if (userInGroup.IsFailure) return userInGroup;

            var remove = await _userInGroupRepository.RemoveAsync(userInGroup.Value, cancellationToken);
            if (remove.IsFailure) return remove;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}