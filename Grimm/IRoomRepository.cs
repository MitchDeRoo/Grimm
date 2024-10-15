using Optional;

namespace Grimm;

public interface IRoomRepository
{
    Option<Room> GetByRoomCode(string roomCode);
    void Add(Room room);
}