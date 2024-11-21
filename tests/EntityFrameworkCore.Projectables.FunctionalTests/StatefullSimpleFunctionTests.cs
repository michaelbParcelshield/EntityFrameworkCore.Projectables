﻿using EntityFrameworkCore.Projectables.FunctionalTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Projectables.FunctionalTests;

public class StatefullSimpleFunctionTests
{
    public record Entity
    {
        public int Id { get; set; }
        
        [Projectable]
        public int Computed1() => Id;

        [Projectable]
        public int Computed2() => Id * 2;

        public int Test(int i) => i;
    }

    [Fact]
    public Task FilterOnProjectableProperty()
    {
        using var dbContext = new SampleDbContext<Entity>();

        var query = dbContext.Set<Entity>()
            .Where(x => x.Computed1() == 1);

        return Verifier.Verify(query.ToQueryString());
    }

    [Fact]
    public Task SelectProjectableProperty()
    {
        using var dbContext = new SampleDbContext<Entity>();

        var query = dbContext.Set<Entity>()
            .Select(x => x.Computed1());

        return Verifier.Verify(query.ToQueryString());
    }

    [Fact]
    public Task FilterOnComplexProjectableProperty()
    {
        using var dbContext = new SampleDbContext<Entity>();

        var query = dbContext.Set<Entity>()
            .Where(x => x.Computed2() == 2);

        return Verifier.Verify(query.ToQueryString());
    }

    [Fact]
    public Task SelectComplexProjectableProperty()
    {
        using var dbContext = new SampleDbContext<Entity>();

        var query = dbContext.Set<Entity>()
            .Select(x => x.Computed2());

        return Verifier.Verify(query.ToQueryString());
    }

    [Fact]
    public Task CombineSelectProjectableProperties()
    {
        using var dbContext = new SampleDbContext<Entity>();

        var query = dbContext.Set<Entity>()
            .Select(x => x.Computed1() + x.Computed2());

        return Verifier.Verify(query.ToQueryString());
    }
}
