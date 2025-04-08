using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleDelete
{
    internal class SystemRoleDeleteCommandHandler : ICommandHandler<SystemRoleDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISystemRoleRepository _systemRoleRepository;

        public SystemRoleDeleteCommandHandler(IUnitOfWork unitOfWork, ISystemRoleRepository systemRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _systemRoleRepository = systemRoleRepository;
        }

        public async Task<Result> Handle(SystemRoleDeleteCommand request, CancellationToken cancellationToken)
        {
            //var sr = await _systemRoleRepository.GetByIdAsync(request.SystemRoleId, cancellationToken);
            //if (sr.IsFailure) return sr;
            //if (sr.Value == null) return Result.Failure(PersistenceErrors.SystemRole.NotFound);

            var delete = await _systemRoleRepository.RemoveAsync(sr.Value, cancellationToken);
            if (delete.IsFailure) return delete;

            throw new NotImplementedException();
        }
    }
}
