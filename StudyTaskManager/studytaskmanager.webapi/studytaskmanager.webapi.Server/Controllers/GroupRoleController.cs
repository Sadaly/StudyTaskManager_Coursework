using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.GroupRoles.Commands.GroupRoleDelete;
using StudyTaskManager.Application.Entity.GroupRoles.Queries.GroupRoleGetAll;
using StudyTaskManager.Application.Entity.GroupRoles.Queries.GroupRoleGetById;
using StudyTaskManager.Application.Entity.Groups.Commands.GroupCreateRole;
using StudyTaskManager.Domain.Shared;
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
        [FromBody] GroupCreateRoleCommand request, // это команда группы, а не ролиГруппы (у них одинаковое имя).
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(request, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }


        //[Authorize]
        [HttpDelete("{roleId:guid}")]
        public async Task<IActionResult> Delete(
            Guid roleId,
            CancellationToken cancellationToken)
        {
            var command = new GroupRoleDeleteCommand(roleId);

            Result response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpGet("{groupRoleId:guid}")]
        public async Task<IActionResult> Get(
            Guid groupRoleId,
            CancellationToken cancellationToken)
        {
            var query = new GroupRoleGetByIdQuery(groupRoleId);
            var response = await Sender.Send(query, cancellationToken);

            if (response.IsFailure) return BadRequest(response.Error);
            if (response.Value.GroupId == null) return BadRequest(); //TODO Добавил ошибку что роль общая

            return Ok(response.Value);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromBody] Guid groupId,
            CancellationToken cancellationToken)
        {
            var query = new GroupRoleGetAllQuery(gr => gr.GroupId == groupId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }
    }
}
