using EntityFrameworkCore.Projectables.FunctionalTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Projectables.FunctionalTests.Generics
{
    public class GenericFunctionTests
    {
        [Fact]
        public Task DefaultIfIdIsNegative()
        {
            using var context = new SampleDbContext<Entity>();
            var query = context.Set<Entity>()
                .Select(x => x.DefaultIfIdIsNegative());

            return Verifier.Verify(query.ToQueryString());
        }
    }
}
