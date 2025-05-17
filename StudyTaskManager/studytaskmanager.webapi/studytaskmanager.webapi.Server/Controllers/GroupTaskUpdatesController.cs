using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.GroupTaskUpdates.Commands.GroupTaskUpdateCreate;
using StudyTaskManager.Application.Entity.GroupTaskUpdates.Commands.GroupTaskUpdateDelete;
using StudyTaskManager.Application.Entity.GroupTaskUpdates.Commands.GroupTaskUpdateUpdate;
using StudyTaskManager.Application.Entity.GroupTaskUpdates.Queries.GroupTaskUpdateGetAll;
using StudyTaskManager.Application.Entity.GroupTaskUpdates.Queries.GroupTaskUpdateGetById;
using StudyTaskManager.WebAPI.Abstractions;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GroupTaskUpdatesController : ApiController
    {
        public GroupTaskUpdatesController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] GroupTaskUpdateCreateCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        //[Authorize]
        [HttpDelete("{updateId:guid}")]
        public async Task<IActionResult> Delete(
            Guid updateId,
            CancellationToken cancellationToken)
        {
            var command = new GroupTaskUpdateDeleteCommand(updateId);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response) : HandleFailure(response);
        }

        //[Authorize]
        [HttpPut("{updateId:guid}")]
        public async Task<IActionResult> Update(
            Guid updateId,
           [FromBody] string newContent,
            CancellationToken cancellationToken)
        {
            var command = new GroupTaskUpdateUpdateCommand(updateId, newContent);
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response) : HandleFailure(response);
        }

        //[Authorize]
        [HttpGet("{updateId:guid}")]
        public async Task<IActionResult> GetById(
            Guid updateId,
            CancellationToken cancellationToken)
        {
            var query = new GroupTaskUpdateGetByIdQuery(updateId);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var query = new GroupTaskUpdateGetAllQuery(null);
            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
        }
    }
}
