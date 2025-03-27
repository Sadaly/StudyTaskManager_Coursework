using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserChangePhoneNumber
{
	internal sealed class UserChangePhoneNumberCommandHandler : ICommandHandler<UserChangePhoneNumberCommand, Guid>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepository _userRepository;

		public UserChangePhoneNumberCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
		{
			_unitOfWork = unitOfWork;
			_userRepository = userRepository;
		}

		public async Task<Result<Guid>> Handle(UserChangePhoneNumberCommand request, CancellationToken cancellationToken)
		{
			Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(request.NewPhoneNumber);

			if (!(await _userRepository.IsPhoneNumberUniqueAsync(phoneNumberResult.Value, cancellationToken)).Value)
			{
				return Result.Failure<Guid>(PersistenceErrors.User.PhoneNumberAlreadyInUse);
			}

			var foundUserResult = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
			if (foundUserResult.IsFailure)
			{
				return Result.Failure<Guid>(foundUserResult.Error);
			}

			var user = foundUserResult.Value;
			user.ChangePhoneNumber(phoneNumberResult.Value);

			await _userRepository.UpdateAsync(user, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return user.Id;
		}
	}
}
