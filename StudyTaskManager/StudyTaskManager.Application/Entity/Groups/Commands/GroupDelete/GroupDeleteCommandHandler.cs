using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupDelete
{
    class GroupDeleteCommandHandler : ICommandHandler<GroupDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;

        public GroupDeleteCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
        }

        public async Task<Result> Handle(GroupDeleteCommand request, CancellationToken cancellationToken)
        {
            var remove = await _groupRepository.RemoveAsync(request.Id, cancellationToken);
            if (remove.IsFailure) return Result.Failure(remove);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
