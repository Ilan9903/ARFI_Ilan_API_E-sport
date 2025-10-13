using Microsoft.EntityFrameworkCore;
using StacktimApi.Data;
using StacktimApi.Models;
using System;

namespace StacktimApi.Tests.Helpers;

public class TestDbContextFactory : IDisposable
{
    public StacktimDbContext Context { get; }

    public TestDbContextFactory()
    {
        var options = new DbContextOptionsBuilder<StacktimDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new StacktimDbContext(options);

        SeedData();
    }

    private void SeedData()
    {
        var players = new[]
        {
            new Player { Id = 1, Pseudo = "TestPlayer1", Email = "test1@email.com", Rank = "Gold", TotalScore = 1500 },
            new Player { Id = 2, Pseudo = "TestPlayer2", Email = "test2@email.com", Rank = "Silver", TotalScore = 900 },
            new Player { Id = 3, Pseudo = "TestPlayer3", Email = "test3@email.com", Rank = "Platinum", TotalScore = 2100 }
        };

        Context.Players.AddRange(players);
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}