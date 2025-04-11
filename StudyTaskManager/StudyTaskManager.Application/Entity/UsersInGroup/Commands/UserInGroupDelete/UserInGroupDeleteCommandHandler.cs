using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Commands.UserInGroupDelete
{
    internal class UserInGroupDeleteCommandHandler : ICommandHandler<UserInGroupDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInGroupRepository _userInGroupRepository;

        public UserInGroupDeleteCommandHandler(IUnitOfWork unitOfWork, IUserInGroupRepository userInGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _userInGroupRepository = userInGroupRepository;
        }

        public async Task<Result> Handle(UserInGroupDeleteCommand request, CancellationToken cancellationToken)
        {
            Result<UserInGroup> userInGroup = await _userInGroupRepository.GetByUserAndGroupAsync(request.UserId, request.GroupId, cancellationToken);
            if (userInGroup.IsFailure) return userInGroup;

            Result delete = await _userInGroupRepository.RemoveAsync(userInGroup.Value, cancellationToken);
            if (delete.IsFailure) return delete;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
