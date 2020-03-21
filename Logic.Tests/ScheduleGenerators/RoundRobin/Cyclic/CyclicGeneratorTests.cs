using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Logic.ScheduleGenerators.RoundRobin.Cyclic;
using Logic.Types.RoundRobin;
using Logic.Types.Exceptions;
using Moq;
using Xunit;

namespace Logic.Tests.ScheduleGenerators.RoundRobin.Cyclic
{
    public class CyclicGeneratorTests
    {
        public CyclicGeneratorTests()
        {
            _cyclicSeedGeneratorMock = new Mock<ICyclicSeedGenerator>();
            _cyclicGenerator = new CyclicGenerator(_cyclicSeedGeneratorMock.Object);
        }

        private readonly Mock<ICyclicSeedGenerator> _cyclicSeedGeneratorMock;
        private readonly ICyclicGenerator _cyclicGenerator;

        public static IEnumerable<object[]> SuccessData =>
            new List<object[]>
            {
                new object[] { 5, 20, new List<List<int>> {
                    new List<int>{ 1, 2, 4, 3 },
                    new List<int>{ 2, 4, 3, 1 },
                    new List<int>{ 4, 3, 1, 2 },
                    new List<int>{ 3, 1, 2, 4 },
                    new List<int>{ 0, 0, 0, 0 }
                } }
            };

        public static IEnumerable<object[]> FailureData =>
            new List<object[]>
            {
                new object[] { 4, 20 }
            };

        public static IEnumerable<object[]> InvalidData =>
            new List<object[]>
            {
                new object[] { 3, 20, "Number of rounds must be at least 4." },
                new object[] { 4, 21, "Number of participants should be a multiple of 4." },
                new object[] { 5, 16, "There are not enough participants." }
            };

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateScheduleShouldCallCyclicSeedGenerator(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            RoundRobinSchedule result = await _cyclicGenerator.GenerateSchedule(roundCount, participantCount);

            _cyclicSeedGeneratorMock.Verify(generator => generator.GenerateSeed(roundCount, participantCount / 4), Times.Once);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateScheduleShouldHaveRoundCountRounds(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            RoundRobinSchedule result = await _cyclicGenerator.GenerateSchedule(roundCount, participantCount);

            result.Rounds.Should().HaveCount(roundCount);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateScheduleShouldHaveParticipantCountGamesPerRound(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            RoundRobinSchedule result = await _cyclicGenerator.GenerateSchedule(roundCount, participantCount);

            result.Rounds.Should().OnlyContain(r => r.Games.Count() == participantCount / 4);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateScheduleShouldHaveFourParticipantsPerGame(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            RoundRobinSchedule result = await _cyclicGenerator.GenerateSchedule(roundCount, participantCount);

            result.Rounds.SelectMany(r => r.Games).Should().OnlyContain(g => g.ParticipantNrs.Count() == 4);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateScheduleShouldHaveIncreasingParticipantsPerGame(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            RoundRobinSchedule result = await _cyclicGenerator.GenerateSchedule(roundCount, participantCount);

            result.Rounds.SelectMany(r => r.Games).Should()
                .OnlyContain(g => g.ParticipantNrs.SequenceEqual(g.ParticipantNrs.OrderBy(x => x)));
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateScheduleShouldHaveAllParticipantsPerRound(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            RoundRobinSchedule result = await _cyclicGenerator.GenerateSchedule(roundCount, participantCount);

            using AssertionScope assertionScope = new AssertionScope();
            foreach (var round in result.Rounds)
            {
                round.Games.SelectMany(g => g.ParticipantNrs).Should()
                    .OnlyHaveUniqueItems()
                    .And.BeEquivalentTo(Enumerable.Range(0, participantCount));
            }
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateScheduleShouldHaveUniquePairs(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            RoundRobinSchedule result = await _cyclicGenerator.GenerateSchedule(roundCount, participantCount);

            result.Rounds.SelectMany(r => r.Games.SelectMany(g => Pairs(g.ParticipantNrs))).Should()
                .OnlyHaveUniqueItems();
        }

        [Theory]
        [MemberData(nameof(FailureData))]
        public async Task GenerateScheduleShouldThrowSeedNotFoundException(int roundCount, int participantCount)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new SeedNotFoundException());

            Func<Task<RoundRobinSchedule>> func = async () => await _cyclicGenerator.GenerateSchedule(roundCount, participantCount);

            await func.Should().ThrowExactlyAsync<SeedNotFoundException>();
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task GenerateScheduleShouldThrowInvalidParameterException(int roundCount, int participantCount, string expectedMessage)
        {
            Func<Task<RoundRobinSchedule>> func = async () => await _cyclicGenerator.GenerateSchedule(roundCount, participantCount);

            await func.Should().ThrowExactlyAsync<InvalidParameterException>().WithMessage(expectedMessage);
        }

        private static IEnumerable<(int, int)> Pairs(IEnumerable<int> list) =>
            list.SelectMany(x => list.Select(y => (x, y)).Where(p => p.x < p.y));
    }
}
