using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageCreate;
using StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageDelete;
using StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageMarkAsRead;
using StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageUpdateContent;
using StudyTaskManager.Application.Entity.PersonalMessages.Queries.PersonalMessageGetById;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.WebAPI.Abstractions;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class PersonalMessageController : ApiController
    {
        public PersonalMessageController(ISender sender) : base(sender) { }


        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] PersonalMessageCreateCommand request,
            CancellationToken cancellationToken)
        {
            var response = await Sender.Send(request, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpGet("{personalMessageId:guid}")]
        public async Task<IActionResult> Get(
            Guid personalMessageId,
            CancellationToken cancellationToken)
        {
            var request = new PersonalMessageGetByIdQuery(personalMessageId);

            var response = await Sender.Send(request, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpDelete("{personalMessageId:guid}")]
        public async Task<IActionResult> Delete(
            Guid personalMessageId,
            CancellationToken cancellationToken)
        {
            var command = new PersonalMessageDeleteCommand(personalMessageId);

            Result response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpPut("MarkAsRead/{personalMessageId:Guid}")]
        public async Task<IActionResult> MarkAsRead(
            Guid personalMessageId,
            CancellationToken cancellationToken)
        {
            var command = new PersonalMessageMarkAsReadCommand(personalMessageId);

            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : NotFound(response.Error);
        }

        //[Authorize]
        [HttpPut("UpdateContent/{personalMessageId:Guid}")]
        public async Task<IActionResult> UpdateContent(
            Guid personalMessageId,
            [FromBody] string content,
            CancellationToken cancellationToken)
        {
            var command = new PersonalMessageUpdateContentCommand(personalMessageId, content);

            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok() : NotFound(response.Error);
        }
    }
}
