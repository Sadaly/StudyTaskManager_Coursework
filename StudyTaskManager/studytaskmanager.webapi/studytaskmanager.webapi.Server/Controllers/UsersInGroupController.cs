using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageUpdateContent;
using StudyTaskManager.Application.Entity.UsersInGroup.Commands.UserInGroupDelete;
using StudyTaskManager.Application.Entity.UsersInGroup.Commands.UserInGroupUpdateRole;
using StudyTaskManager.Application.Entity.UsersInGroup.Queries.GetAllUserInGroups;
using StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupGetByUserAndGroupIds;
using StudyTaskManager.WebAPI.Abstractions;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersInGroupController : ApiController
    {
        public UsersInGroupController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpDelete("{userId:guid}_{groupId:guid}")]
        public async Task<IActionResult> Delete(
            Guid userId,
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var command = new UserInGroupDeleteCommand(userId, groupId);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpPut("{userId:guid}_{groupId:guid}/UpdateRole")]
        public async Task<IActionResult> UpdateRole(
            Guid userId,
            Guid groupId,
            [FromBody] Guid roleId,
            CancellationToken cancellationToken)
        {
            var request = new UserInGroupUpdateRoleCommand(userId, groupId, roleId);
            var response = await Sender.Send(request, cancellationToken);

            return response.IsSuccess ? Ok() : NotFound(response.Error);
        }

        //[Authorize]
        [HttpGet("{userId:guid}_{groupId:guid}")]
        public async Task<IActionResult> Get(
            Guid userId,
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var query = new UserInGroupGetByUserAndGroupIdsQuery(userId, groupId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        //[Authorize]
        [HttpGet("User/{userId:guid}")]
        public async Task<IActionResult> GetByUser(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var query = new UserInGroupsGetAllQuery(uig => uig.UserId == userId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        //[Authorize]
        [HttpGet("Group/{groupId:guid}")]
        public async Task<IActionResult> GetByGroup(
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var query = new UserInGroupsGetAllQuery(uig => uig.GroupId == groupId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }
    }
}
