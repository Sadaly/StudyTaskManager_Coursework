using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChats.Commands.GroupChatAddParticipant
{
    public sealed class GroupChatAddParticipantCommandHandler : ICommandHandler<GroupChatAddParticipantCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IGroupChatRepository _groupChatRepository;
        private readonly IGroupChatParticipantRepository _groupChatParticipantRepository;

        public GroupChatAddParticipantCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IGroupChatRepository groupChatRepository, IGroupChatParticipantRepository groupChatParticipantRepository)
        {
            _groupChatParticipantRepository = groupChatParticipantRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _groupChatRepository = groupChatRepository;
        }

        public async Task<Result> Handle(GroupChatAddParticipantCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsFailure) return Result.Failure(user);

            var chat = await _groupChatRepository.GetByIdAsync(request.GroupChatId, cancellationToken);
            if (chat.IsFailure) return Result.Failure(chat);

            var participant = GroupChatParticipant.Create(user.Value, chat.Value);
            if (participant.IsFailure) return Result.Failure(participant);

            var add = await _groupChatParticipantRepository.AddAsync(participant.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure(add);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
