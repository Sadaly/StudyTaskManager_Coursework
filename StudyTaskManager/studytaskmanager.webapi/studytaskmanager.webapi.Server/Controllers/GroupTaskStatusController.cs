using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.GroupTaskStatuses.Commands.GroupTaskStatusCreate;
using StudyTaskManager.Application.Entity.GroupTaskStatuses.Commands.GroupTaskStatusDelete;
using StudyTaskManager.Application.Entity.GroupTaskStatuses.Commands.GroupTaskStatusUpdate;
using StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetAll;
using StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetById;
using StudyTaskManager.WebAPI.Abstractions;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GroupTaskStatusController : ApiController
    {
        public GroupTaskStatusController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] GroupTaskStatusCreateCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        //[Authorize]
        [HttpGet("{groupTaskStatusId:guid}")]
        public async Task<IActionResult> Get(
            Guid groupTaskStatusId,
            CancellationToken cancellationToken)
        {
            var query = new GroupTaskStatusGetByIdQuery(groupTaskStatusId);
            var response = await Sender.Send(query, cancellationToken);

            if (response.IsFailure) return HandleFailure(response);
            if (response.Value.GroupId == null) return BadRequest(); //TODO написать ошибку что статус общий

            return Ok(response.Value);
        }

        //[Authorize]
        [HttpGet("Group/{groupId:guid}")]
        public async Task<IActionResult> GetByGroup(
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var query = new GroupTaskStatusGetAllQuery(gts => gts.GroupId == groupId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok() : HandleFailure(response);
        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] GroupTaskStatusUpdateCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : HandleFailure(response);
        }

        //[Authorize]
        [HttpDelete("{groupTaskStatusId:guid}")]
        public async Task<IActionResult> Delete(
            Guid groupTaskStatusId,
            CancellationToken cancellationToken)
        {
            // Сначало получить GroupTaskStatus, чтобу удостовериться что он не общий, после удалить
            var queryGetById = new GroupTaskStatusGetByIdQuery(groupTaskStatusId);
            var responseGetById = await Sender.Send(queryGetById, cancellationToken);

            if (responseGetById.IsFailure) return BadRequest(responseGetById.Error);
            if (responseGetById.Value.GroupId == null) return BadRequest(); //TODO написать ошибку что статус общий

            var command = new GroupTaskStatusDeleteCommand(groupTaskStatusId);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : HandleFailure(response);
        }
    }
}
