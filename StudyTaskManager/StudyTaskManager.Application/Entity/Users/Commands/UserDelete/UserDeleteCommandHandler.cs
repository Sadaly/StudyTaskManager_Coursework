using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Generic.Commands.DeleteById;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserDelete
{
    public sealed class UserDeleteCommandHandler : DeleteByIdCommandHandler<StudyTaskManager.Domain.Entity.User.User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserDeleteCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository) : base(unitOfWork, userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

		//public async Task<Result> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
		//{
		//	var delete = await _userRepository.RemoveAsync(request.UserId, cancellationToken);
		//	if (delete.IsFailure) return delete;

		//	await _unitOfWork.SaveChangesAsync(cancellationToken);

		//	return Result.Success();
		//}
	}
}
