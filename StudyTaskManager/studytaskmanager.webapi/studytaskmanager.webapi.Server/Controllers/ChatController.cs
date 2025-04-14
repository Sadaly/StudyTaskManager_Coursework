using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTaskManager.WebAPI.Abstractions;

namespace StudyTaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : ApiController
    {
        public ChatController(ISender sender) : base(sender) { }
    }
}
