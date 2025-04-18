using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Queries.BlockedUserInfoGetAll
{
    internal sealed class BlockedUserInfoGetAllQueryHandler : IQueryHandler<BlockedUserInfoGetAllQuery, BlockedUserInfoListResponse>
    {
        private readonly IBlockedUserInfoRepository _blockedUserInfoRepository;

        public BlockedUserInfoGetAllQueryHandler(IBlockedUserInfoRepository blockedUserInfoRepository)
        {
            _blockedUserInfoRepository = blockedUserInfoRepository;
        }

        public async Task<Result<BlockedUserInfoListResponse>> Handle(BlockedUserInfoGetAllQuery request, CancellationToken cancellationToken)
        {
            var list = await _blockedUserInfoRepository.GetAllAsync(cancellationToken);
            if (list.IsFailure) return Result.Failure<BlockedUserInfoListResponse>(list);

            return new BlockedUserInfoListResponse(list.Value);
        }
    }
}
