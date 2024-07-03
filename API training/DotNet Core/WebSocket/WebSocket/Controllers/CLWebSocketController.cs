using Microsoft.AspNetCore.Mvc;
using WS.Middleware;

namespace WS.Controllers
{
    [Route("api/websocket")]
    [ApiController]
    public class CLWebSocketController : ControllerBase
    {
        private readonly ChatWebSocketHandler _webSocketHandler;

        public CLWebSocketController(ChatWebSocketHandler webSocketHandler)
        {
            _webSocketHandler = webSocketHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if(HttpContext.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await _webSocketHandler.HandleWebSocket(HttpContext,webSocket);
                return Ok("done");
            }
            else
            {
                return BadRequest("web socket is not supported");
            }
        }
    }
}
