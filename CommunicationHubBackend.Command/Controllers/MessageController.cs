using CommunicationHubBackend.Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationHubBackend.Command.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator mediator;

        public MessageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Used to analyse a message and remove any disapproved content.
        /// </summary>
        /// <returns>Will return a 202 Accepted if the message was analysed and approved</returns>
        [HttpPost("sendmessage")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendMessage([FromBody] AnalyseMessageCommand analyseMessageCommand)
        {
            if (analyseMessageCommand != null)
            {
                try
                {
                    var result = await mediator.Send(analyseMessageCommand);
                    return Ok(result);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest("Message is empty");
        }
    }
}
