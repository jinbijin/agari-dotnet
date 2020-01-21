﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Logic.BracketGenerators.RoundRobin.Cyclic;
using Logic.Types.Bracket;
using Logic.Types.Exceptions;
using Moq;
using Xunit;

namespace Logic.Tests.BracketGenerators.RoundRobin.Cyclic
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
                new object[] { 4, 16 },
                new object[] { 5, 16 }
            };

        public static IEnumerable<object[]> InvalidData =>
            new List<object[]>
            {
                new object[] { 3, 20, "Number of rounds must be at least 4." },
                new object[] { 4, 21, "Number of participants should be a multiple of 4." }
            };

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateBracketShouldCallCyclicSeedGenerator(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            Bracket result = await _cyclicGenerator.GenerateBracket(roundCount, participantCount);

            _cyclicSeedGeneratorMock.Verify(generator => generator.GenerateSeed(roundCount, participantCount / 4), Times.Once);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateBracketShouldHaveRoundCountRounds(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            Bracket result = await _cyclicGenerator.GenerateBracket(roundCount, participantCount);

            result.Rounds.Should().HaveCount(roundCount);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateBracketShouldHaveParticipantCountGamesPerRound(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            Bracket result = await _cyclicGenerator.GenerateBracket(roundCount, participantCount);

            result.Rounds.Should().OnlyContain(r => r.Games.Count() == participantCount / 4);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateBracketShouldHaveFourParticipantsPerGame(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            Bracket result = await _cyclicGenerator.GenerateBracket(roundCount, participantCount);

            result.Rounds.SelectMany(r => r.Games).Should().OnlyContain(g => g.ParticipantNrs.Count() == 4);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateBracketShouldHaveIncreasingParticipantsPerGame(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            Bracket result = await _cyclicGenerator.GenerateBracket(roundCount, participantCount);

            result.Rounds.SelectMany(r => r.Games).Should()
                .OnlyContain(g => g.ParticipantNrs.SequenceEqual(g.ParticipantNrs.OrderBy(x => x)));
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateBracketShouldHaveAllParticipantsPerRound(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            Bracket result = await _cyclicGenerator.GenerateBracket(roundCount, participantCount);

            using AssertionScope assertionScope = new AssertionScope();
            foreach (var round in result.Rounds)
            {
                round.Games.SelectMany(g => g.ParticipantNrs).Should()
                    .OnlyHaveUniqueItems()
                    .And.BeEquivalentTo(Enumerable.Range(1, participantCount));
            }
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public async Task GenerateBracketShouldHaveUniquePairs(int roundCount, int participantCount,
            IEnumerable<List<int>> mockedSeed)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(mockedSeed);

            Bracket result = await _cyclicGenerator.GenerateBracket(roundCount, participantCount);

            result.Rounds.SelectMany(r => r.Games.SelectMany(g => Pairs(g.ParticipantNrs))).Should()
                .OnlyHaveUniqueItems();
        }

        [Theory]
        [MemberData(nameof(FailureData))]
        public async Task GenerateBracketShouldThrowSeedNotFoundException(int roundCount, int participantCount)
        {
            _cyclicSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new SeedNotFoundException());

            Func<Task<Bracket>> func = async () => await _cyclicGenerator.GenerateBracket(roundCount, participantCount);

            await func.Should().ThrowExactlyAsync<SeedNotFoundException>();
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task GenerateBracketShouldThrowInvalidParameterException(int roundCount, int participantCount, string expectedMessage)
        {
            Func<Task<Bracket>> func = async () => await _cyclicGenerator.GenerateBracket(roundCount, participantCount);

            await func.Should().ThrowExactlyAsync<InvalidParameterException>().WithMessage(expectedMessage);
        }

        private static IEnumerable<(int, int)> Pairs(IEnumerable<int> list) =>
            list.SelectMany(x => list.Select(y => (x, y)).Where(p => p.x < p.y));
    }
}
