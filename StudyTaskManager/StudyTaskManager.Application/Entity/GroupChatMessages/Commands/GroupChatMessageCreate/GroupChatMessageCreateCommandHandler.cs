using MediatR;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageCreate
{
    //internal class GroupChatMessageCreateCommandHandler : ICommandHandler<GroupChatMessageCreateCommand, (Guid, ulong)>
    //{
    //    private readonly IUserRepository _userRepository;
    //    private readonly IGroupChatRepository _groupChatRepository;

    //    Task<Result<(Guid, ulong)>> IRequestHandler<GroupChatMessageCreateCommand, Result<(Guid, ulong)>>.Handle(GroupChatMessageCreateCommand request, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
