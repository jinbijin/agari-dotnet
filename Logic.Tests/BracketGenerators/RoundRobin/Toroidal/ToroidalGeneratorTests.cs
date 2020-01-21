using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Logic.BracketGenerators.RoundRobin.Toroidal;
using Logic.Types.Bracket;
using Logic.Types.Exceptions;
using Moq;
using Xunit;

namespace Logic.Tests.BracketGenerators.RoundRobin.Toroidal
{
    public class ToroidalGeneratorTests
    {
        private readonly Mock<IToroidalSeedGenerator> _toroidalSeedGeneratorMock;
        private readonly IToroidalGenerator _toroidalGenerator;

        public static IEnumerable<object[]> SuccessData =>
            new List<object[]>
            {
                new object[] { 4, 20, new List<(int, int)> { (1, 2) } },
                new object[] { 5, 20, new List<(int, int)> { (1, 2) } }
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
                new object[] { 3, 20, "Number of rounds must be one of 4, 5, 8, 9, 12, or 13." },
                new object[] { 4, 21, "Number of participants should be a multiple of 4." }
            };

        public ToroidalGeneratorTests()
        {
            _toroidalSeedGeneratorMock = new Mock<IToroidalSeedGenerator>();
            _toroidalGenerator = new ToroidalGenerator(_toroidalSeedGeneratorMock.Object);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateBracketShouldCallToroidalSeedGenerator(int roundCount, int participantCount,
            IEnumerable<(int, int)> mockedSeed)
        {
            _toroidalSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(mockedSeed);

            Bracket result = _toroidalGenerator.GenerateBracket(roundCount, participantCount);

            _toroidalSeedGeneratorMock.Verify(generator => generator.GenerateSeed(roundCount / 4, participantCount / 4), Times.Once);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateBracketShouldHaveRoundCountRounds(int roundCount, int participantCount,
            IEnumerable<(int, int)> mockedSeed)
        {
            _toroidalSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(mockedSeed);

            Bracket result = _toroidalGenerator.GenerateBracket(roundCount, participantCount);

            result.Rounds.Should().HaveCount(roundCount);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateBracketShouldHaveParticipantCountGamesPerRound(int roundCount, int participantCount,
            IEnumerable<(int, int)> mockedSeed)
        {
            _toroidalSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(mockedSeed);

            Bracket result = _toroidalGenerator.GenerateBracket(roundCount, participantCount);

            result.Rounds.Should().OnlyContain(r => r.Games.Count() == participantCount / 4);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateBracketShouldHaveFourParticipantsPerGame(int roundCount, int participantCount,
            IEnumerable<(int, int)> mockedSeed)
        {
            _toroidalSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(mockedSeed);

            Bracket result = _toroidalGenerator.GenerateBracket(roundCount, participantCount);

            result.Rounds.SelectMany(r => r.Games).Should().OnlyContain(g => g.ParticipantNrs.Count() == 4);
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateBracketShouldHaveIncreasingParticipantsPerGame(int roundCount, int participantCount,
            IEnumerable<(int, int)> mockedSeed)
        {
            _toroidalSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(mockedSeed);

            Bracket result = _toroidalGenerator.GenerateBracket(roundCount, participantCount);

            result.Rounds.SelectMany(r => r.Games).Should()
                .OnlyContain(g => g.ParticipantNrs.SequenceEqual(g.ParticipantNrs.OrderBy(x => x)));
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void GenerateBracketShouldHaveAllParticipantsPerRound(int roundCount, int participantCount,
            IEnumerable<(int, int)> mockedSeed)
        {
            _toroidalSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(mockedSeed);

            Bracket result = _toroidalGenerator.GenerateBracket(roundCount, participantCount);

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
        public void GenerateBracketShouldHaveUniquePairs(int roundCount, int participantCount,
            IEnumerable<(int, int)> mockedSeed)
        {
            _toroidalSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(mockedSeed);

            Bracket result = _toroidalGenerator.GenerateBracket(roundCount, participantCount);

            result.Rounds.SelectMany(r => r.Games.SelectMany(g => Pairs(g.ParticipantNrs))).Should()
                .OnlyHaveUniqueItems();
        }

        [Theory]
        [MemberData(nameof(FailureData))]
        public void GenerateBracketShouldThrowSeedNotFoundException(int roundCount, int participantCount)
        {
            _toroidalSeedGeneratorMock.Setup(generator => generator.GenerateSeed(It.IsAny<int>(), It.IsAny<int>()))
                .Throws<SeedNotFoundException>();

            Func<Bracket> func = () => _toroidalGenerator.GenerateBracket(roundCount, participantCount);

            func.Should().ThrowExactly<SeedNotFoundException>();
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public void GenerateBracketShouldThrowInvalidParameterException(int roundCount, int participantCount, string expectedMessage)
        {
            Func<Bracket> func = () => _toroidalGenerator.GenerateBracket(roundCount, participantCount);

            func.Should().ThrowExactly<InvalidParameterException>().WithMessage(expectedMessage);
        }

        private static IEnumerable<(int, int)> Pairs(IEnumerable<int> list) =>
            list.SelectMany(x => list.Select(y => (x, y)).Where(p => p.x < p.y));
    }
}
