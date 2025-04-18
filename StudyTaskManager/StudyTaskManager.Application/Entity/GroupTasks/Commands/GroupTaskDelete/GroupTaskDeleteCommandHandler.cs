using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTasks.Commands.GroupTaskDelete
{
    class GroupTaskDeleteCommandHandler : ICommandHandler<GroupTaskDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupTaskRepository _groupTaskRepository;

        public GroupTaskDeleteCommandHandler(IUnitOfWork unitOfWork, IGroupTaskRepository groupTaskRepository)
        {
            _unitOfWork = unitOfWork;
            _groupTaskRepository = groupTaskRepository;
        }

        public async Task<Result> Handle(GroupTaskDeleteCommand request, CancellationToken cancellationToken)
        {
            var remove = await _groupTaskRepository.RemoveAsync(request.Id, cancellationToken);
            if (remove.IsFailure) return remove;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
