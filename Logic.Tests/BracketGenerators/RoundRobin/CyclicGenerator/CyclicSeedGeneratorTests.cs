using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Logic.BracketGenerators.RoundRobin.CyclicGenerator;
using Xunit;

namespace Logic.Tests.BracketGenerators.RoundRobin.CyclicGenerator
{
    public class CyclicSeedGeneratorTests
    {
        private readonly ICyclicSeedGenerator _cyclicSeedGenerator;

        public CyclicSeedGeneratorTests()
        {
            _cyclicSeedGenerator = new CyclicSeedGenerator();
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 12)]
        [InlineData(3, 13)]
        public void GenerateSeedShouldHaveCountPairs(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().HaveCount(count);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 12)]
        [InlineData(3, 13)]
        public void GenerateSeedShouldHavePositiveEntries(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => r.Item1 > 0 && r.Item2 > 0);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 12)]
        [InlineData(3, 13)]
        public void GenerateSeedShouldHaveBoundedEntries(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => 2 * r.Item1 < modulus && 2 * r.Item2 < modulus);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 12)]
        [InlineData(3, 13)]
        public void GenerateSeedShouldHaveIncreasingPairs(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => r.Item1 < r.Item2);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 12)]
        [InlineData(3, 13)]
        public void GenerateSeedShouldBeIncreasing(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().BeInAscendingOrder(r => r.Item1);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 12)]
        [InlineData(3, 13)]
        public void GenerateSeedShouldHaveDistinctEntries(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Select(r => r.Item1).Concat(results.Select(r => r.Item2))
                .Should().OnlyHaveUniqueItems();
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 12)]
        [InlineData(3, 13)]
        public void GenerateSeedShouldHaveDistinctSums(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Select(r => r.Item2 - r.Item1).Concat(results.Select(r => r.Item1 + r.Item2))
                .Should().OnlyHaveUniqueItems();
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(2, 11)]
        [InlineData(3, 12)]
        public void GenerateSeedShouldThrowSeedNotFoundException(int count, int modulus)
        {
            Func<IEnumerable<(int, int)>> func = () => _cyclicSeedGenerator.GenerateSeed(count, modulus);

            func.Should().ThrowExactly<SeedNotFoundException>()
                .WithMessage("Could not find valid seed for the cyclic bracket generator.");
        }
    }
}
