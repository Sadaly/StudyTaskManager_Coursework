using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageCreate;
using StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageDelete;
using StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageMarkAsRead;
using StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageUpdateContent;
using StudyTaskManager.Application.Entity.PersonalMessages.Queries.PersonalMessageGetById;
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
            var request = new PersonalMessageDeleteCommand(personalMessageId);
            var response = await Sender.Send(request, cancellationToken);

            return response.IsSuccess ? Ok() : BadRequest(response.Error);
        }

        //[Authorize]
        [HttpPut("{personalMessageId:Guid}/MarkAsRead")]
        public async Task<IActionResult> MarkAsRead(
            Guid personalMessageId,
            CancellationToken cancellationToken)
        {
            var request = new PersonalMessageMarkAsReadCommand(personalMessageId);
            var response = await Sender.Send(request, cancellationToken);

            return response.IsSuccess ? Ok() : NotFound(response.Error);
        }

        //[Authorize]
        [HttpPut("{personalMessageId:Guid}/UpdateContent")]
        public async Task<IActionResult> UpdateContent(
            Guid personalMessageId,
            [FromBody] string newContent,
            CancellationToken cancellationToken)
        {
            var request = new PersonalMessageUpdateContentCommand(personalMessageId, newContent);
            var response = await Sender.Send(request, cancellationToken);

            return response.IsSuccess ? Ok() : NotFound(response.Error);
        }
    }
}
