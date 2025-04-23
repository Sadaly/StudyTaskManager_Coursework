using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Queries.BlockedUserInfoGetAll
{
    internal sealed class BlockedUserInfoGetAllQueryHandler : IQueryHandler<BlockedUserInfoGetAllQuery, List<BlockedUserInfoResponse>>
    {
        private readonly IBlockedUserInfoRepository _blockedUserInfoRepository;

        public BlockedUserInfoGetAllQueryHandler(IBlockedUserInfoRepository blockedUserInfoRepository)
        {
            _blockedUserInfoRepository = blockedUserInfoRepository;
        }

        public async Task<Result<List<BlockedUserInfoResponse>>> Handle(BlockedUserInfoGetAllQuery request, CancellationToken cancellationToken)
        {
            var bui = request.Predicate == null 
                ? await _blockedUserInfoRepository.GetAllAsync(cancellationToken)
                : await _blockedUserInfoRepository.GetAllAsync(request.Predicate, cancellationToken);

            if (bui.IsFailure) return Result.Failure<List<BlockedUserInfoResponse>>(bui.Error);

            var listRes = bui.Value.Select(u => new BlockedUserInfoResponse(u)).ToList();

            return listRes;
        }
    }
}
