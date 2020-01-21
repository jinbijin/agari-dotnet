using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Logic.BracketGenerators.RoundRobin.Cyclic;
using Logic.Types.Exceptions;
using Xunit;

namespace Logic.Tests.BracketGenerators.RoundRobin.Cyclic
{
    public class CyclicSeedGeneratorTests
    {
        public CyclicSeedGeneratorTests(ICyclicSeedGenerator cyclicSeedGenerator)
        {
            _cyclicSeedGenerator = cyclicSeedGenerator;
        }

        private readonly ICyclicSeedGenerator _cyclicSeedGenerator;

        public static IEnumerable<object[]> SuccessData =>
            new List<object[]>
            {
                new object[] { 5, 5 },
                new object[] { 9, 12 },
                new object[] { 13, 13 }
            };

        public static IEnumerable<object[]> FailureData =>
            new List<object[]>
            {
                new object[] { 5, 4 },
                new object[] { 9, 11 },
                new object[] { 13, 12 }
            };

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldGenerateFourTuples(int count, int modulus)
        {
            IEnumerable<List<int>> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => r.Count == 4);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldGenerateTuplesWithSumMultipleOfModulus(int count, int modulus)
        {
            IEnumerable<List<int>> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => r.Sum() % modulus == 0);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldHaveCountTuples(int count, int modulus)
        {
            IEnumerable<List<int>> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().HaveCount(count);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldHaveNonnegativeEntries(int count, int modulus)
        {
            IEnumerable<List<int>> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => r.All(x => x >= 0));
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldHaveBoundedEntries(int count, int modulus)
        {
            IEnumerable<List<int>> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => r.All(x => x < modulus));
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldHaveDistinctAdjacentDifferences(int count, int modulus)
        {
            IEnumerable<List<int>> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            using AssertionScope assertionScope = new AssertionScope();
            foreach (int i in Enumerable.Range(0, 4))
            {
                results.Should().OnlyHaveUniqueItems(r => r[i]);
            }
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateSeedShouldHaveDistinctOppositeDifferences(int count, int modulus)
        {
            IEnumerable<List<int>> results = _cyclicSeedGenerator.GenerateSeed(count, modulus);

            using AssertionScope assertionScope = new AssertionScope();
            foreach (int i in Enumerable.Range(0, 4))
            {
                results.Should().OnlyHaveUniqueItems(r => (r[i] + r[(i + 1) / 4]) % modulus);
            }
        }

        [Theory]
        [MemberData(nameof(FailureData))]
        public void GenerateSeedShouldThrowSeedNotFoundException(int count, int modulus)
        {
            Func<IEnumerable<List<int>>> func = () => _cyclicSeedGenerator.GenerateSeed(count, modulus);

            func.Should().ThrowExactly<SeedNotFoundException>()
                .WithMessage("Could not find valid seed for the toroidal bracket generator.");
        }
    }
}
