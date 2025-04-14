
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Application.Entity.PersonalChats.Commands.PersonalChatCreate
{
    class PersonalChatCreateCommandHandler : ICommandHandler<PersonalChatCreateCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IPersonalChatRepository _personalChatRepository;

        public PersonalChatCreateCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IPersonalChatRepository personalChatRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _personalChatRepository = personalChatRepository;
        }

        public async Task<Result<Guid>> Handle(PersonalChatCreateCommand request, CancellationToken cancellationToken)
        {
            var user1 = await _userRepository.GetByIdAsync(request.User1, cancellationToken);
            if (user1.IsFailure) return Result.Failure<Guid>(user1);

            var user2 = await _userRepository.GetByIdAsync(request.User2, cancellationToken);
            if (user2.IsFailure) return Result.Failure<Guid>(user2);

            var personalChat = PersonalChat.Create(user1.Value, user2.Value);
            if (personalChat.IsFailure) return Result.Failure<Guid>(personalChat);

            var add = await _personalChatRepository.AddAsync(personalChat.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<Guid>(add);

            return Result.Success(personalChat.Value.Id);
        }
    }
}
