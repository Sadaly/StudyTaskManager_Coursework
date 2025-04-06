using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Commands.UserInGroupDelete
{
    internal class UserInGroupDeleteCommandHandler : ICommandHandler<UserInGroupDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserInGroupRepository _userInGroupRepository;

        public UserInGroupDeleteCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IGroupRepository groupRepository, IUserInGroupRepository userInGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _userInGroupRepository = userInGroupRepository;
        }

        public async Task<Result> Handle(UserInGroupDeleteCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.Entity.User.User?> user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsFailure) return user;
            if (user.Value == null) return Result.Failure(PersistenceErrors.User.NotFound);

            Result<Domain.Entity.Group.Group?> group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return group;
            if (group.Value == null) return Result.Failure(PersistenceErrors.Group.NotFound);

            Result<Domain.Entity.Group.UserInGroup?> uig = await _userInGroupRepository.GetByUserAndGroupAsync(user.Value, group.Value, cancellationToken);
            if (uig.IsFailure) return uig;
            if (uig.Value == null) return Result.Failure(PersistenceErrors.UserInGroup.NotFound);

            Result delete = uig.Value.Delete();
            if (delete.IsFailure) return delete;

            Result update = await _userInGroupRepository.UpdateAsync(uig.Value, cancellationToken);
            if (update.IsFailure) return update;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
