using FluentResults;

namespace Grimm;

public class GameService(IRoomRepository repository, IRoomCodeGenerator codeGenerator)
{
    public string CreateRoom()
    {
        var roomCode = codeGenerator.Generate();

        repository.Add(new Room(roomCode, 4, []));

        return roomCode;
    }

    public Result<string> JoinRoom(string playerName, string roomCode)
    {
        var option = repository.GetByRoomCode(roomCode);
        if (!option.HasValue)
        {
            return Result.Fail($"Failed to find room with code {roomCode}");
        }

        return Result.Ok();
    }
}