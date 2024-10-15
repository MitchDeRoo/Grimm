using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace Grimm.Tests;

[TestFixture]
public class GameControllerShould
{
    private IRoomRepository _repository;
    private IRoomCodeGenerator _generator;
    private GameService _service;
    private GameController _controller;

    [SetUp]
    public void Setup()
    {
        _repository = Substitute.For<IRoomRepository>();
        _generator = Substitute.For<IRoomCodeGenerator>();
        _service = new GameService(_repository, _generator);
        _controller = new GameController(_service);
    }

    [Test]
    public void Return_OK_Result_When_Create()
    {
        _generator.Generate().Returns("ABCD");

        var result = _controller.CreateRoom() as ObjectResult;

        result.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldBe("ABCD");
    }
}