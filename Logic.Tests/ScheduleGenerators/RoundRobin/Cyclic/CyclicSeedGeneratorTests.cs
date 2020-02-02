using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Logic.ScheduleGenerators.RoundRobin.Cyclic;
using Logic.Types.Exceptions;
using Xunit;

namespace Logic.Tests.ScheduleGenerators.RoundRobin.Cyclic
{
    public class CyclicSeedGeneratorTests
    {
        public CyclicSeedGeneratorTests()
        {
            _cyclicSeedGenerator = new CyclicSeedGenerator();
        }

        private readonly ICyclicSeedGenerator _cyclicSeedGenerator;

        public static IEnumerable<object[]> SuccessData =>
            new List<object[]>
            {
                new object[] { 4, 5 },
                new object[] { 5, 5 },
                new object[] { 6, 7 }
            };

        public static IEnumerable<object[]> FailureData =>
            new List<object[]>
            {
                new object[] { 4, 4 },
                new object[] { 5, 4 },
                new object[] { 6, 6 }
            };

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateSeedShouldGenerateFourTuples(int count, int modulus)
        {
            IEnumerable<List<int>> results = await _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => r.Count == 4);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateSeedShouldGenerateTuplesWithSumMultipleOfModulus(int count, int modulus)
        {
            IEnumerable<List<int>> results = await _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => r.Sum() % modulus == 0);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateSeedShouldHaveCountTuples(int count, int modulus)
        {
            IEnumerable<List<int>> results = await _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().HaveCount(count);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateSeedShouldHaveNonnegativeEntries(int count, int modulus)
        {
            IEnumerable<List<int>> results = await _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => r.All(x => x >= 0));
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateSeedShouldHaveBoundedEntries(int count, int modulus)
        {
            IEnumerable<List<int>> results = await _cyclicSeedGenerator.GenerateSeed(count, modulus);

            results.Should().OnlyContain(r => r.All(x => x < modulus));
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateSeedShouldHaveDistinctAdjacentDifferences(int count, int modulus)
        {
            IEnumerable<List<int>> results = await _cyclicSeedGenerator.GenerateSeed(count, modulus);

            using AssertionScope assertionScope = new AssertionScope();
            foreach (int i in Enumerable.Range(0, 4))
            {
                results.Should().OnlyHaveUniqueItems(r => r[i]);
            }
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateSeedShouldHaveDistinctOppositeDifferences(int count, int modulus)
        {
            IEnumerable<List<int>> results = await _cyclicSeedGenerator.GenerateSeed(count, modulus);

            using AssertionScope assertionScope = new AssertionScope();
            foreach (int i in Enumerable.Range(0, 4))
            {
                results.Should().OnlyHaveUniqueItems(r => (r[i] + r[(i + 1) % 4]) % modulus);
            }
        }

        [Theory]
        [MemberData(nameof(FailureData))]
        public async Task GenerateSeedShouldThrowSeedNotFoundException(int count, int modulus)
        {
            Func<Task<IEnumerable<List<int>>>> func = async () => await _cyclicSeedGenerator.GenerateSeed(count, modulus);

            await func.Should().ThrowExactlyAsync<SeedNotFoundException>()
                .WithMessage("Could not find valid seed for the cyclic schedule generator.");
        }
    }
}
