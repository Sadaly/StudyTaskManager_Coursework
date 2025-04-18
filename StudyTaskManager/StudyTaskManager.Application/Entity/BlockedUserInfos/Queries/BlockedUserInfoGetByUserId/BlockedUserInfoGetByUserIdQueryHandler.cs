using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Queries.BlockedUserInfoGetByUserId
{
    internal sealed class BlockedUserInfoGetByUserIdQueryHandler : IQueryHandler<BlockedUserInfoGetByUserIdQuery, BlockedUserInfoResponse>
    {
        private readonly IBlockedUserInfoRepository _blockedUserInfoRepository;

        public BlockedUserInfoGetByUserIdQueryHandler(IBlockedUserInfoRepository blockedUserInfoRepository)
        {
            _blockedUserInfoRepository = blockedUserInfoRepository;
        }

        public async Task<Result<BlockedUserInfoResponse>> Handle(BlockedUserInfoGetByUserIdQuery request, CancellationToken cancellationToken)
        {
            var buiResult = await _blockedUserInfoRepository.GetByUser(request.UserId);
            if (buiResult.IsFailure) return Result.Failure<BlockedUserInfoResponse>(buiResult);

            return new BlockedUserInfoResponse(buiResult.Value);
        }
    }
}