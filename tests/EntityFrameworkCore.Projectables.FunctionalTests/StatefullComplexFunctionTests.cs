﻿using EntityFrameworkCore.Projectables.FunctionalTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Projectables.FunctionalTests;

public class StatefullComplexFunctionTests
{
    public record Entity
    {
        public int Id { get; set; }
        
        [Projectable]
        public int Computed(int argument) => Id + argument; 
    }


    [Fact]
    public Task FilterOnProjectableProperty()
    {
        using var dbContext = new SampleDbContext<Entity>();

        var query = dbContext.Set<Entity>()
            .Where(x => x.Computed(1) == 2);

        return Verifier.Verify(query.ToQueryString());
    }

    [Fact]
    public Task SelectProjectableProperty()
    {
        using var dbContext = new SampleDbContext<Entity>();

        var query = dbContext.Set<Entity>()
            .Select(x => x.Computed(1));

        return Verifier.Verify(query.ToQueryString());
    }


    [Fact]
    public Task PassInVariableArguments()
    {
        using var dbContext = new SampleDbContext<Entity>();

        var argument = 1;
        var query = dbContext.Set<Entity>()
            .Select(x => x.Computed(argument));

        return Verifier.Verify(query.ToQueryString());
    }

    [Fact]
    public Task PassInReferenceArguments()
    {
        using var dbContext = new SampleDbContext<Entity>();

        var query = dbContext.Set<Entity>()
            .Select(x => x.Computed(x.Id));

        return Verifier.Verify(query.ToQueryString());
    }
}
