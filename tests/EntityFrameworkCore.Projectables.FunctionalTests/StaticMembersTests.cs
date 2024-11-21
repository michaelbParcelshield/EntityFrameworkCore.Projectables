﻿using EntityFrameworkCore.Projectables.FunctionalTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Projectables.FunctionalTests;

public class StaticMembersTests
{
    public static class Constants
    {
        public static readonly double ExpensiveThreshold = 1;
    }

    public record Entity
    {
        public int Id { get; set; }

        public double Price { get; set; }

        [Projectable]
        public bool IsExpensive => Price > Constants.ExpensiveThreshold;
    }

    [Fact]
    public Task FilterOnProjectableProperty()
    {
        using var dbContext = new SampleDbContext<Entity>(Infrastructure.CompatibilityMode.Full);

        var query = dbContext.Set<Entity>()
            .Where(x => x.IsExpensive);

        return Verifier.Verify(query.ToQueryString());
    }
}
