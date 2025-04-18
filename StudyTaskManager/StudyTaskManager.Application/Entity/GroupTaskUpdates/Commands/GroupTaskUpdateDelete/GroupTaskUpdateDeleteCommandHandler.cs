using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Commands.GroupTaskUpdateDelete
{
    class GroupTaskUpdateDeleteCommandHandler : ICommandHandler<GroupTaskUpdateDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupTaskUpdateRepository _groupTaskUpdateRepository;

        public GroupTaskUpdateDeleteCommandHandler(IUnitOfWork unitOfWork, IGroupTaskUpdateRepository groupTaskUpdateRepository)
        {
            _unitOfWork = unitOfWork;
            _groupTaskUpdateRepository = groupTaskUpdateRepository;
        }

        public async Task<Result> Handle(GroupTaskUpdateDeleteCommand request, CancellationToken cancellationToken)
        {
            var remove = await _groupTaskUpdateRepository.RemoveAsync(request.Id, cancellationToken);
            if (remove.IsFailure) return remove;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
