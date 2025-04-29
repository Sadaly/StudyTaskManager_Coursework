using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.WebAPI.Abstractions;
using StudyTaskManager.Application.Entity.GroupRoles.Commands.GroupRoleCreate;
using StudyTaskManager.Application.Entity.GroupRoles.Commands.GroupRoleDelete;
using StudyTaskManager.Application.Entity.GroupRoles.Queries.GroupRoleGetAll;
using StudyTaskManager.Application.Entity.GroupRoles.Queries.GroupRoleGetById;

namespace StudyTaskManager.WebAPI.Controllers.Common
{
    [Route("api/Common/" + CommonGroupRolesController.CONTROLLER_NAME)]
    public class CommonGroupRolesController : ApiController
    {
        public const string CONTROLLER_NAME = "GroupRoles";
        public CommonGroupRolesController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
        [FromBody] GroupRoleCreateCommand request,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(request, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpDelete("{groupRoleId:guid}")]
        public async Task<IActionResult> Delete(
            Guid groupRoleId,
            CancellationToken cancellationToken)
        {
            var queryGetById = new GroupRoleGetByIdQuery(groupRoleId);
            var responseGetById = await Sender.Send(queryGetById, cancellationToken);

            if (responseGetById.IsFailure) return BadRequest(responseGetById.Error);
            if (responseGetById.Value.GroupId != null) return BadRequest(); //TODO Добавил ошибку что роль не общая

            var command = new GroupRoleDeleteCommand(groupRoleId);
            var response = await Sender.Send(command, cancellationToken);

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
            if (response.Value.GroupId != null) return BadRequest(); //TODO Добавил ошибку что роль не общая

            return Ok(response.Value);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var query = new GroupRoleGetAllQuery(gr => gr.GroupId == null);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }
    }
}