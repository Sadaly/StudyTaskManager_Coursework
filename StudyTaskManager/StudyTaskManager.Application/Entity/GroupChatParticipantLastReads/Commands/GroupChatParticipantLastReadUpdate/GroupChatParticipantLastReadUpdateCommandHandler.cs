using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Commands.GroupChatParticipantLastReadUpdate
{
    internal class GroupChatParticipantLastReadUpdateCommandHandler : ICommandHandler<GroupChatParticipantLastReadUpdateCommand>
    {
        IUserRepository _userRepository;
        IGroupChatMessageRepository _groupChatMessageRepository;
        IGroupChatRepository _groupChatRepository;
        IGroupChatParticipantLastReadRepository _groupChatParticipantLastReadRepository;
        IUnitOfWork _unitOfWork;

        public GroupChatParticipantLastReadUpdateCommandHandler(IUserRepository userRepository, IGroupChatMessageRepository groupChatMessageRepository, IGroupChatRepository groupChatRepository, IGroupChatParticipantLastReadRepository groupChatParticipantLastReadRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _groupChatMessageRepository = groupChatMessageRepository;
            _groupChatRepository = groupChatRepository;
            _groupChatParticipantLastReadRepository = groupChatParticipantLastReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(GroupChatParticipantLastReadUpdateCommand request, CancellationToken cancellationToken)
        {
            var userRes = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (userRes.IsFailure) return Result.Failure<(Guid, Guid, ulong)>(userRes);

            var groupChatRes = await _groupChatRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (groupChatRes.IsFailure) return Result.Failure(groupChatRes);

            var gcmRes = await _groupChatMessageRepository.GetMessageAsync(request.GroupChatId, request.LastReadId, cancellationToken);
            if (gcmRes.IsFailure) return Result.Failure(userRes);

            var result = GroupChatParticipantLastRead.Create(userRes.Value, groupChatRes.Value, gcmRes.Value);
            if (result.IsFailure) return Result.Failure(result);

            await _groupChatParticipantLastReadRepository.UpdateAsync(result.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
