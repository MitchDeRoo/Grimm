namespace Grimm;

public record Room(string RoomCode, int MaxPlayers, List<Player> Players);