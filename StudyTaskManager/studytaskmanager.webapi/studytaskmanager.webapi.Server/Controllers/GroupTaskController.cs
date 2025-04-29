using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.GroupTasks.Commands.GroupTaskCreate;
using StudyTaskManager.Application.Entity.GroupTasks.Commands.GroupTaskDelete;
using StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetByGroup;
using StudyTaskManager.WebAPI.Abstractions;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GroupTaskController : ApiController
    {
        public GroupTaskController(ISender sender) : base(sender) { }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] GroupTaskCreateCommand command,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpGet("{taskId:guid}")]
        public async Task<IActionResult> Delete(
            Guid taskId,
            CancellationToken cancellationToken)
        {
            var request = new GroupTaskDeleteCommand(taskId);
            var response = await Sender.Send(request, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpGet("Group/{groupId:guid}")]
        public async Task<IActionResult> GetByGroup(
            Guid groupId,
            CancellationToken cancellationToken)
        {
            var request = new GroupTaskGetAllQuery(gt => gt.GroupId == groupId);
            var response = await Sender.Send(request, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }
    }
}
