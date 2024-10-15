using NSubstitute;
using Optional;
using Shouldly;

namespace Grimm.Tests;

[TestFixture]
public class GameServiceShould
{
    private IRoomRepository _repository;
    private IRoomCodeGenerator _codeGenerator;
    private GameService _service;

    [SetUp]
    public void Setup()
    {
        _repository = Substitute.For<IRoomRepository>();
        _codeGenerator = Substitute.For<IRoomCodeGenerator>();
        _service = new GameService(_repository, _codeGenerator);
    }

    [Test]
    public void Return_RoomCode_When_Success()
    {
        _codeGenerator.Generate().Returns("ABCD");

        var roomCode = _service.CreateRoom();

        roomCode.ShouldBe("ABCD");
    }

    [Test]
    public void Add_Game_To_Repository_When_Success()
    {
        _codeGenerator.Generate().Returns("ABCD");

        _service.CreateRoom();

        _repository.Received().Add(new Room("ABCD", 4, []));
    }

    [Test]
    public void Return_Error_When_Joining_Game_With_NonExistent_Code()
    {
        _repository.GetByRoomCode("ABCD").Returns(Option.None<Room>());

        var result = _service.JoinRoom("Red Riding Hood", "ABCD");

        result.IsFailed.ShouldBeTrue();
        result.HasError(error => error.Message == "Failed to find room with code ABCD").ShouldBeTrue();
    }

    [Test]
    public void Return_Error_When_Joining_Full_Game()
    {
        Room room = new Room("ABCD", 4, [ new Player(), new Player(), new Player(), new Player() ]);
        _repository.GetByRoomCode("ABCD").Returns(Option.Some(room));

        var result = _service.JoinRoom("Red Riding Hood", "ABCD");

        result.IsFailed.ShouldBeTrue();
        result.HasError(error => error.Message == "Room is full");
    }
}