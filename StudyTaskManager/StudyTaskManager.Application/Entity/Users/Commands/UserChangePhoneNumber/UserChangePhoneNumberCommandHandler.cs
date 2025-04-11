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
            Result<PhoneNumber> phoneNumber = PhoneNumber.Create(request.NewPhoneNumber);
            if (phoneNumber.IsFailure) return Result.Failure<Guid>(phoneNumber.Error);

            Result<bool> isPhoneNumberUnique = await _userRepository.IsPhoneNumberUniqueAsync(phoneNumber.Value, cancellationToken);
            if (isPhoneNumberUnique.IsFailure) return Result.Failure<Guid>(isPhoneNumberUnique.Error);
            if (!isPhoneNumberUnique.Value) return Result.Failure<Guid>(PersistenceErrors.User.PhoneNumberAlreadyInUse);

            Result<Domain.Entity.User.User> user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsFailure) return Result.Failure<Guid>(user.Error);

            user.Value.ChangePhoneNumber(phoneNumber.Value);

            await _userRepository.UpdateAsync(user.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Value.Id;
        }
    }
}
