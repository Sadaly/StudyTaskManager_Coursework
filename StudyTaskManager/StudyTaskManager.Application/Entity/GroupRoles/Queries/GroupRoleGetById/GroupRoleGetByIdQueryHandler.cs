using MediatR;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupRoles.Queries.GroupRoleGetById
{
    public class GroupRoleGetByIdQueryHandler : IQueryHandler<GroupRoleGetByIdQuery, GroupRoleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRoleRepository _groupRoleRepository;

        public GroupRoleGetByIdQueryHandler(IUnitOfWork unitOfWork, IGroupRoleRepository groupRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRoleRepository = groupRoleRepository;
        }

        public async Task<Result<GroupRoleResponse>> Handle(GroupRoleGetByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _groupRoleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (role.IsFailure) return Result.Failure<GroupRoleResponse>(role);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(new GroupRoleResponse(role.Value));
        }
    }
}
