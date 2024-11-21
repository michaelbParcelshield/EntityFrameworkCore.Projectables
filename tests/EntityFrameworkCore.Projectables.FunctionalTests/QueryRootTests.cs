using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.Projectables.FunctionalTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Projectables.FunctionalTests;

public class QueryRootTests
{
    public record Entity
    {
        public int Id { get; set; }

        [Projectable(UseMemberBody = nameof(Computed2))]
        public int Computed1 => Id;

        private int Computed2 => Id * 2;

        [Projectable(UseMemberBody = nameof(_ComputedWithBaking))]
        [NotMapped]
        public int ComputedWithBacking { get; set; }

        private int _ComputedWithBaking => Id * 5;
    }

    [Fact]
    public Task UseMemberPropertyQueryRootExpression()
    {
        using var dbContext = new SampleDbContext<Entity>();

        var query = dbContext.Set<Entity>();

        return Verifier.Verify(query.ToQueryString());
    }
}
