using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageCreate
{
    internal class PersonalMessageCreateCommandHandler : ICommandHandler<PersonalMessageCreateCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IPersonalChatRepository _personalChatRepository;
        private readonly IPresonalMessageRepository _presonalMessageRepository;

        public PersonalMessageCreateCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IPersonalChatRepository personalChatRepository, IPresonalMessageRepository presonalMessageRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _personalChatRepository = personalChatRepository;
            _presonalMessageRepository = presonalMessageRepository;
        }

        public async Task<Result<Guid>> Handle(PersonalMessageCreateCommand request, CancellationToken cancellationToken)
        {
            var content = Content.Create(request.Content);
            if (content.IsFailure) return Result.Failure<Guid>(content);

            var sender = await _userRepository.GetByIdAsync(request.SenderId, cancellationToken);
            if (sender.IsFailure) return Result.Failure<Guid>(sender);

            var chat = await _personalChatRepository.GetByIdAsync(request.PersonalChatId, cancellationToken);
            if (chat.IsFailure) return Result.Failure<Guid>(chat);

            var message = PersonalMessage.Create(sender.Value, chat.Value, content.Value);
            if (message.IsFailure) return Result.Failure<Guid>(message);

            var add = await _presonalMessageRepository.AddAsync(message.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<Guid>(add);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(message.Value.Id);
        }
    }
}
