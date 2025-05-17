using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.GroupRoles.Commands.GroupRoleDelete;
using StudyTaskManager.Application.Entity.GroupRoles.Queries.GroupRoleGetAll;
using StudyTaskManager.Application.Entity.GroupRoles.Queries.GroupRoleGetById;
using StudyTaskManager.Application.Entity.Groups.Commands.GroupCreateRole;
using StudyTaskManager.WebAPI.Abstractions;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GroupRoleController : ApiController
    {
        public GroupRoleController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
        [FromBody] GroupCreateRoleCommand command, // это команда группы, а не ролиГруппы (у них одинаковое имя).
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        //[Authorize]
        [HttpDelete("{roleId:guid}")]
        public async Task<IActionResult> Delete(
            Guid roleId,
            CancellationToken cancellationToken)
        {
            var query = new GroupRoleGetByIdQuery(roleId);
            var responseGetById = await Sender.Send(query, cancellationToken);

            if (responseGetById.IsFailure) return BadRequest(responseGetById.Error);
            if (responseGetById.Value.GroupId == null) return BadRequest(); //TODO Добавил ошибку что роль общая

            var command = new GroupRoleDeleteCommand(roleId);
            var responseDelete = await Sender.Send(command, cancellationToken);

            return responseDelete.IsSuccess ? Ok() : BadRequest(responseDelete.Error);
        }

        //[Authorize]
        [HttpGet("{roleId:guid}")]
        public async Task<IActionResult> Get(
            Guid roleId,
            CancellationToken cancellationToken)
        {
            var query = new GroupRoleGetByIdQuery(roleId);
            var response = await Sender.Send(query, cancellationToken);

            if (response.IsFailure) return HandleFailure(response);
            if (response.Value.GroupId == null) return BadRequest(); //TODO Добавил ошибку что роль общая

            return Ok(response.Value);
        }

        //[Authorize]
        [HttpGet("Group/{groupId:guid}")]
        public async Task<IActionResult> GetAll(
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var query = new GroupRoleGetAllQuery(gr => gr.GroupId == groupId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
    }
}
