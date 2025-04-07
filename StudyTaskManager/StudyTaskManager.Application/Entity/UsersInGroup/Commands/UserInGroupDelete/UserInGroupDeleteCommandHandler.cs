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
        private readonly IUserInGroupRepository _userInGroupRepository;

        public UserInGroupDeleteCommandHandler(IUnitOfWork unitOfWork, IUserInGroupRepository userInGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _userInGroupRepository = userInGroupRepository;
        }

        public async Task<Result> Handle(UserInGroupDeleteCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.Entity.Group.UserInGroup?> uig = await _userInGroupRepository.GetByUserAndGroupAsync(request.UserId, request.GroupId, cancellationToken);
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
