using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Commands.GroupTaskStatusDelete
{
    public class GroupTaskStatusDeleteCommandHandler : ICommandHandler<GroupTaskStatusDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupTaskStatusRepository _groupTaskStatusRepository;

        public GroupTaskStatusDeleteCommandHandler(IUnitOfWork unitOfWork, IGroupTaskStatusRepository groupTaskStatusRepository)
        {
            _unitOfWork = unitOfWork;
            _groupTaskStatusRepository = groupTaskStatusRepository;
        }

        public async Task<Result> Handle(GroupTaskStatusDeleteCommand request, CancellationToken cancellationToken)
        {
            var remove = await _groupTaskStatusRepository.RemoveAsync(request.Id, cancellationToken);
            if (remove.IsFailure) return Result.Failure(remove);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
