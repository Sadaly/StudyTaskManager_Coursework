using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChats.Commands.GroupChatDelete
{
    public sealed class GroupChatDeleteCommandHandler : ICommandHandler<GroupChatDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupChatRepository _groupChatRepository;

        public GroupChatDeleteCommandHandler(IUnitOfWork unitOfWork, IGroupChatRepository groupChatRepository)
        {
            _unitOfWork = unitOfWork;
            _groupChatRepository = groupChatRepository;
        }

        public async Task<Result> Handle(GroupChatDeleteCommand request, CancellationToken cancellationToken)
        {
            var remove = await _groupChatRepository.RemoveAsync(request.ChatId, cancellationToken);
            if (remove.IsFailure) return Result.Failure(remove);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
