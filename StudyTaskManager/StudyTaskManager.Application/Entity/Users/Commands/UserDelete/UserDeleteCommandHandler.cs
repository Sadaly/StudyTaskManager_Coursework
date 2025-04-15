using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserDelete
{
    public sealed class UserDeleteCommandHandler : ICommandHandler<UserDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserDeleteCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var delete = await _userRepository.RemoveAsync(request.UserId, cancellationToken);
            if (delete.IsFailure) return delete;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
