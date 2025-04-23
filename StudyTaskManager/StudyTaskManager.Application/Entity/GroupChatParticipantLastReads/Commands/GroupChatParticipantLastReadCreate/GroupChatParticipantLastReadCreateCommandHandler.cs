using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Commands.GroupChatParticipantLastReadCreate
{
    internal class GroupChatParticipantLastReadCreateCommandHandler : ICommandHandler<GroupChatParticipantLastReadCreateCommand, (Guid, Guid, ulong)>
    {
        IUserRepository _userRepository;
        IGroupChatMessageRepository _groupChatMessageRepository;
        IGroupChatRepository _groupChatRepository;
        IGroupChatParticipantLastReadRepository _groupChatParticipantLastReadRepository;
        IUnitOfWork _unitOfWork;

        public GroupChatParticipantLastReadCreateCommandHandler(IUserRepository userRepository, IGroupChatMessageRepository groupChatMessageRepository, IGroupChatRepository groupChatRepository, IGroupChatParticipantLastReadRepository groupChatParticipantLastReadRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _groupChatMessageRepository = groupChatMessageRepository;
            _groupChatRepository = groupChatRepository;
            _groupChatParticipantLastReadRepository = groupChatParticipantLastReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<(Guid, Guid, ulong)>> Handle(GroupChatParticipantLastReadCreateCommand request, CancellationToken cancellationToken)
        {
            var userRes = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (userRes.IsFailure) return Result.Failure<(Guid, Guid, ulong)>(userRes);

            var groupChatRes = await _groupChatRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (groupChatRes.IsFailure) return Result.Failure<(Guid, Guid, ulong)>(groupChatRes);

            var gcmRes = await _groupChatMessageRepository.GetMessageAsync(request.GroupChatId, request.LastReadId, cancellationToken);
            if (gcmRes.IsFailure) return Result.Failure<(Guid, Guid, ulong)>(userRes);

            var gcpRes = await _groupChatParticipantLastReadRepository.GetParticipantLastReadAsync(request.UserId, request.GroupChatId, request.LastReadId, cancellationToken);
            if (userRes.IsSuccess) return Result.Failure<(Guid, Guid, ulong)>(PersistenceErrors.GroupChatParticipantLastRead.AlreadyExist);

            var result = GroupChatParticipantLastRead.Create(userRes.Value, groupChatRes.Value, gcmRes.Value);
            if (result.IsFailure) return Result.Failure<(Guid, Guid, ulong)>(result);

            await _groupChatParticipantLastReadRepository.AddAsync(result.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return (result.Value.UserId, result.Value.GroupChatId, result.Value.LastReadMessageId);
        }
    }
}
