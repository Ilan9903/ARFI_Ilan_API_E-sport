using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using StacktimApi.Controllers;
using StacktimApi.DTOs;
using StacktimApi.Tests.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StacktimApi.Tests.Controllers;

public class PlayersControllerTests
{
    [Fact]
    public async Task GetPlayers_ReturnsAllPlayers()
    {
        await using var factory = new TestDbContextFactory();
        var context = factory.Context;
        var controller = new PlayersController(context);

        var result = await controller.GetPlayers();

        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var players = okResult.Value.Should().BeAssignableTo<IEnumerable<PlayerDto>>().Subject;
        players.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetPlayer_WithValidId_ReturnsPlayer()
    {
        await using var factory = new TestDbContextFactory();
        var context = factory.Context;
        var controller = new PlayersController(context);
        var existingId = 1;

        var result = await controller.GetPlayer(existingId);

        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var player = okResult.Value.Should().BeOfType<PlayerDto>().Subject;
        player.Id.Should().Be(existingId);
    }

    [Fact]
    public async Task GetPlayer_WithInvalidId_ReturnsNotFound()
    {
        await using var factory = new TestDbContextFactory();
        var context = factory.Context;
        var controller = new PlayersController(context);
        var nonExistingId = 999;

        var result = await controller.GetPlayer(nonExistingId);

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task CreatePlayer_WithValidData_ReturnsCreated()
    {
        await using var factory = new TestDbContextFactory();
        var context = factory.Context;
        var controller = new PlayersController(context);
        var newPlayerDto = new CreatePlayerDto
        {
            Name = "NewPlayer",
            Email = "new@player.com",
            RankPlayer = "Bronze"
        };

        var result = await controller.CreatePlayer(newPlayerDto);

        var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
        var player = createdResult.Value.Should().BeOfType<PlayerDto>().Subject;
        player.Name.Should().Be("NewPlayer");
        player.TotalScore.Should().Be(0);
    }

    [Fact]
    public async Task CreatePlayer_WithDuplicatePseudo_ReturnsBadRequest()
    {
        await using var factory = new TestDbContextFactory();
        var context = factory.Context;
        var controller = new PlayersController(context);
        var duplicatePlayerDto = new CreatePlayerDto
        {
            Name = "TestPlayer1",
            Email = "unique@email.com",
            RankPlayer = "Gold"
        };

        var result = await controller.CreatePlayer(duplicatePlayerDto);

        result.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task DeletePlayer_WithValidId_ReturnsNoContent()
    {
        await using var factory = new TestDbContextFactory();
        var context = factory.Context;
        var controller = new PlayersController(context);
        var idToDelete = 2;

        var result = await controller.DeletePlayer(idToDelete);

        result.Should().BeOfType<NoContentResult>();
        var deletedPlayer = await context.Players.FindAsync(idToDelete);
        deletedPlayer.Should().BeNull();
    }

    [Fact]
    public async Task GetLeaderboard_ReturnsOrderedPlayers()
    {
        await using var factory = new TestDbContextFactory();
        var context = factory.Context;
        var controller = new PlayersController(context);

        var result = await controller.GetLeaderboard();

        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var players = okResult.Value.Should().BeAssignableTo<IEnumerable<PlayerDto>>().Subject;
        players.Should().HaveCount(3);
        players.Should().BeInDescendingOrder(p => p.TotalScore);
    }
}