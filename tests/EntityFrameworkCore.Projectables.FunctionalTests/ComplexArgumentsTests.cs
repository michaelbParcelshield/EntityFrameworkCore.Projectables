﻿using EntityFrameworkCore.Projectables.FunctionalTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Projectables.FunctionalTests
{
    public class ComplexArgumentsTests
    {
        public class TestEntity
        {
            public int Id { get; set; }

            [Projectable]
            public bool IsValid1(List<int> validIds) => validIds.Contains(Id);

            [Projectable]
            public bool IsValid2(int[] validIds) => validIds.Contains(Id);

            [Projectable]
            public bool IsValid3(params int[] validIds) => validIds.Contains(Id);
        }

        [Fact]
        public Task ListOfPrimitivesArguments()
        {
            using var dbContext = new SampleDbContext<TestEntity>();

            var validList = new List<int>() { 1, 2, 3 };

            var query = dbContext.Set<TestEntity>()
                .Where(x => x.IsValid1(validList));

            return Verifier.Verify(query.ToQueryString());
        }

        [Fact]
        public Task ArrayOfPrimitivesArguments()
        {
            using var dbContext = new SampleDbContext<TestEntity>();

            var validArray = new[]  { 1, 2, 3 };

            var query = dbContext.Set<TestEntity>()
                .Where(x => x.IsValid2(validArray));

            return Verifier.Verify(query.ToQueryString());
        }


        [Fact]
        public Task ParamsOfPrimitivesArguments()
        {
            using var dbContext = new SampleDbContext<TestEntity>();

            var query = dbContext.Set<TestEntity>()
                .Where(x => x.IsValid3(1, 2, 3));

            return Verifier.Verify(query.ToQueryString());
        }
    }
}
