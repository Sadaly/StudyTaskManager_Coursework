using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Queries;

public sealed record BlockedUserInfoResponse(
    Guid UserId,
    string Username,
    string Email,
    DateTime BlockedDate,
    string Reason,
    Guid? PrevRoleId
    )
{
    internal BlockedUserInfoResponse(BlockedUserInfo bui)
        : this(bui.UserId, bui.User.Username.Value, bui.User.Email.Value, bui.BlockedDate, bui.Reason, bui.PrevRoleId)
    { }
}