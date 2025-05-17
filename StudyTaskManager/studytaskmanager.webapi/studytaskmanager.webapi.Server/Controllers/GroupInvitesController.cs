using StudyTaskManager.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.GroupInvites.Commands.GroupInviteDelete;
using StudyTaskManager.Application.Entity.GroupInvites.Commands.GroupInviteAcceptInvite;
using StudyTaskManager.Application.Entity.GroupInvites.Commands.GroupInviteDeclineInvite;
using StudyTaskManager.Application.Entity.UsersInGroup.Commands.UserInGroupCreate;
using StudyTaskManager.Application.Entity.GroupInvites.Queries.GroupInviteGetByGroupAndUser;
using StudyTaskManager.Application.Entity.Groups.Queries.GroupGetById;
using StudyTaskManager.Application.Entity.GroupInvites.Queries.GroupInviteGetAll;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GroupInvitesController : ApiController
    {
        public GroupInvitesController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpDelete("{receiverId:guid}_{groupId:guid}")]
        public async Task<IActionResult> Delete(
            Guid receiverId,
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var command = new GroupInviteDeleteCommand(receiverId, groupId);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : HandleFailure(response);
        }

        //[Authorize]
        [HttpGet("{receiverId:guid}_{groupId:guid}")]
        public async Task<IActionResult> Get(
            Guid receiverId,
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var query = new GroupInviteGetByGroupAndUserQuery(receiverId, groupId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        //[Authorize]
        [HttpPut("{receiverId:guid}_{groupId:guid}/Accept")]
        public async Task<IActionResult> Accept(
            Guid receiverId,
            Guid groupId,
            CancellationToken cancellationToken)
        {
            // принимаем приглашение
            var commandAccept = new GroupInviteAcceptInviteCommand(receiverId, groupId);
            var responseAccept = await Sender.Send(commandAccept, cancellationToken);
            if (responseAccept.IsFailure) return NotFound(responseAccept.Error);

            // добавляем пользователя в группе, для этого нужно знать роль по умолчанию в ней
            var groupResponse = await Sender.Send(new GroupGetByIdQuery(groupId), cancellationToken);
            if (groupResponse.IsFailure) return BadRequest(groupResponse.Error);

            var commandCreateUIG = new UserInGroupCreateWithRoleCommand(
                receiverId, groupId, groupResponse.Value.DefaultRoleId);
            var responseCreateUIG = await Sender.Send(commandCreateUIG, cancellationToken);
            if (responseCreateUIG.IsFailure) return BadRequest(responseCreateUIG.Error);

            // в конце удаляем приглашение (возможно можно сделать это и после того как мы ответим приложению-клиенту)
            return await Delete(receiverId, groupId, cancellationToken);
        }
        //[Authorize]
        [HttpPut("{receiverId:guid}_{groupId:guid}/Decline")]
        public async Task<IActionResult> Decline(
            Guid receiverId,
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var commandDecline = new GroupInviteDeclineInviteCommand(receiverId, groupId);
            var responseDecline = await Sender.Send(commandDecline, cancellationToken);

            return responseDecline.IsSuccess ? Ok() : BadRequest(responseDecline.Error);
        }

        //[Authorize]
        [HttpGet("Receiver/{receiverId:guid}")]
        public async Task<IActionResult> GetByReceiver(
            Guid receiverId,
            CancellationToken cancellationToken)
        {
            var query = new GroupInviteGetAllQuery(gi => gi.ReceiverId == receiverId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
        //[Authorize]
        [HttpGet("Group/{groupId:guid}")]
        public async Task<IActionResult> GetByGroup(
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var query = new GroupInviteGetAllQuery(gi => gi.GroupId == groupId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
        //[Authorize]
        [HttpGet("Sender/{senderId:guid}")]
        public async Task<IActionResult> GetBySenderId(
            Guid senderId,
            CancellationToken cancellationToken)
        {
            var query = new GroupInviteGetAllQuery(gi => gi.SenderId == senderId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
    }
}
