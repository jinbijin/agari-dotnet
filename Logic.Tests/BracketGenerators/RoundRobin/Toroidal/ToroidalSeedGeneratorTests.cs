using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Logic.BracketGenerators.RoundRobin.Toroidal;
using Xunit;

namespace Logic.Tests.BracketGenerators.RoundRobin.Toroidal
{
    public class ToroidalSeedGeneratorTests
    {
        private readonly IToroidalSeedGenerator _toroidalSeedGenerator;

        public static IEnumerable<object[]> SuccessData =>
            new List<object[]>
            {
                new object[] { 1, 5 },
                new object[] { 2, 12 },
                new object[] { 3, 13 }
            };

        public static IEnumerable<object[]> FailureData =>
            new List<object[]>
            {
                new object[] { 1, 4 },
                new object[] { 2, 11 },
                new object[] { 3, 12 }
            };

        public ToroidalSeedGeneratorTests()
        {
            _toroidalSeedGenerator = new ToroidalSeedGenerator();
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldHaveCountPairs(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _toroidalSeedGenerator.GenerateSeed(count, modulus);

            results.Should().HaveCount(count);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldHavePositiveEntries(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _toroidalSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => r.Item1 > 0 && r.Item2 > 0);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldHaveBoundedEntries(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _toroidalSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => 2 * r.Item1 < modulus && 2 * r.Item2 < modulus);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldHaveIncreasingPairs(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _toroidalSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => r.Item1 < r.Item2);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldBeIncreasing(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _toroidalSeedGenerator.GenerateSeed(count, modulus);

            results.Should().BeInAscendingOrder(r => r.Item1);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldHaveDistinctEntries(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _toroidalSeedGenerator.GenerateSeed(count, modulus);

            results.Select(r => r.Item1).Concat(results.Select(r => r.Item2))
                .Should().OnlyHaveUniqueItems();
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldHaveDistinctSums(int count, int modulus)
        {
            IEnumerable<(int, int)> results = _toroidalSeedGenerator.GenerateSeed(count, modulus);

            results.Select(r => r.Item2 - r.Item1).Concat(results.Select(r => r.Item1 + r.Item2))
                .Should().OnlyHaveUniqueItems();
        }

        [Theory]
        [MemberData(nameof(FailureData))]
        public void GenerateSeedShouldThrowSeedNotFoundException(int count, int modulus)
        {
            Func<IEnumerable<(int, int)>> func = () => _toroidalSeedGenerator.GenerateSeed(count, modulus);

            func.Should().ThrowExactly<SeedNotFoundException>()
                .WithMessage("Could not find valid seed for the toroidal bracket generator.");
        }
    }
}
