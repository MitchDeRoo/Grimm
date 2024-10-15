using Microsoft.AspNetCore.Mvc;

namespace Grimm;

[Route("api/games")]
[ApiController]
public class GameController(GameService service) : ControllerBase
{
    [HttpPost("create")]
    public IActionResult CreateRoom()
    {
        var roomCode = service.CreateRoom();
        return Ok(roomCode);
    }
}