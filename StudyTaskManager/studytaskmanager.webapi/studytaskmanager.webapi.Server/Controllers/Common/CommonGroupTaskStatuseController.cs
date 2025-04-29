using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.GroupTaskStatuses.Commands.GroupTaskStatusCreate;
using StudyTaskManager.Application.Entity.GroupTaskStatuses.Commands.GroupTaskStatusDelete;
using StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetAll;
using StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetById;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.WebAPI.Abstractions;

namespace StudyTaskManager.WebAPI.Controllers.Common
{
    [Route("api/Common/" + CommonGroupTaskStatuseController.CONTROLLER_NAME)]
    public class CommonGroupTaskStatuseController : ApiController
    {
        public const string CONTROLLER_NAME = "GroupTaskStatus";
        public sealed record GroupTaskStatusCreateCommandData(
            string Title,
            bool CanBeUpdated,
            string? Description);
        public CommonGroupTaskStatuseController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] GroupTaskStatusCreateCommandData data,
            CancellationToken cancellationToken)
        {
            var command = new GroupTaskStatusCreateCommand(data.Title, data.CanBeUpdated, null, data.Description);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpDelete("{groupTaskStatusId:guid}")]
        public async Task<IActionResult> Delete(
            Guid groupTaskStatusId,
            CancellationToken cancellationToken)
        {
            var queryGetById = new GroupTaskStatusGetByIdQuery(groupTaskStatusId);
            var responseGetById = await Sender.Send(queryGetById, cancellationToken);

            if (responseGetById.IsFailure) return BadRequest(responseGetById.Error);
            if (responseGetById.Value.GroupId != null) return BadRequest(); //TODO написать ошибку то статус не общий

            var command = new GroupTaskStatusDeleteCommand(groupTaskStatusId);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }


        //[Authorize]
        [HttpGet("{groupTaskStatusId:guid}")]
        public async Task<IActionResult> Get(
            Guid groupTaskStatusId,
            CancellationToken cancellationToken)
        {
            var query = new GroupTaskStatusGetByIdQuery(groupTaskStatusId);
            var response = await Sender.Send(query, cancellationToken);

            if (response.IsFailure) return BadRequest(response.Error);
            if (response.Value.GroupId != null) return BadRequest(); //TODO написать ошибку то статус не общий

            return Ok(response.Value);
        }
        //[Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var query = new GroupTaskStatusGetAllQuery(gts => gts.GroupId == null);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }
    }
}
