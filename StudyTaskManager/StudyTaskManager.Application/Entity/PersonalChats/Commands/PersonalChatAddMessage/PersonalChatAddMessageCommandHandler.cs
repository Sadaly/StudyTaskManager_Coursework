using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.PersonalChats.Commands.PersonalChatAddMessage
{
    public class PersonalChatAddMessageCommandHandler : ICommandHandler<PersonalChatAddMessageCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IPersonalChatRepository _personalChatRepository;
        private readonly IPresonalMessageRepository _presonalMessageRepository;

        public PersonalChatAddMessageCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IPersonalChatRepository personalChatRepository, IPresonalMessageRepository presonalMessageRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _personalChatRepository = personalChatRepository;
            _presonalMessageRepository = presonalMessageRepository;
        }

        public async Task<Result<Guid>> Handle(PersonalChatAddMessageCommand request, CancellationToken cancellationToken)
        {
            var content = Content.Create(request.Message);
            if (content.IsFailure) return Result.Failure<Guid>(content);

            var sender = await _userRepository.GetByIdAsync(request.SenderId, cancellationToken);
            if (sender.IsFailure) return Result.Failure<Guid>(sender);

            var receiver = await _userRepository.GetByIdAsync(request.ReceiverId, cancellationToken);
            if (receiver.IsFailure) return Result.Failure<Guid>(receiver);

            var personalChat = await _personalChatRepository.GetChatByUsersAsync(sender.Value, receiver.Value, cancellationToken);
            if (personalChat.IsFailure) return Result.Failure<Guid>(personalChat);

            var personalMessage = PersonalMessage.Create(sender.Value, personalChat.Value, content.Value);
            if (personalMessage.IsFailure) return Result.Failure<Guid>(personalMessage);

            var add = await _presonalMessageRepository.AddAsync(personalMessage.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<Guid>(add);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(personalMessage.Value.Id);
        }
    }
}
