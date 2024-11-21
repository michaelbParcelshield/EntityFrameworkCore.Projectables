using System.Linq.Expressions;
using EntityFrameworkCore.Projectables.FunctionalTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Projectables.FunctionalTests;

public class UseMemberBodyPropertyTests
{
    public record Entity
    {
        public int Id { get; set; }
        
        [Projectable(UseMemberBody = nameof(Computed2))]
        public int Computed1 => Id;

        private int Computed2 => Id * 2;

        [Projectable(UseMemberBody = nameof(Computed4))]
        public int Computed3 => Id;

        private static Expression<Func<Entity, int>> Computed4 => x => x.Id * 3;
    }

    [Fact]
    public Task UseMemberPropertyGenerated()
    {
        using var dbContext = new SampleDbContext<Entity>();

        var query = dbContext.Set<Entity>()
            .Select(x => x.Computed1);

        return Verifier.Verify(query.ToQueryString());
    }

    [Fact]
    public Task UseMemberPropertyExpression()
    {
        using var dbContext = new SampleDbContext<Entity>();

        var query = dbContext.Set<Entity>()
            .Select(x => x.Computed3);

        return Verifier.Verify(query.ToQueryString());
    }
}
