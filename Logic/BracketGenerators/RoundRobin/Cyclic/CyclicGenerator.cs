using System.Collections.Generic;
using System.Linq;
using Logic.Types.Bracket;

namespace Logic.BracketGenerators.RoundRobin.Cyclic
{
    public class CyclicGenerator : ICyclicGenerator
    {
        private readonly ICyclicSeedGenerator _cyclicSeedGenerator;

        private const string NotSupportedRoundCountMessage = "Number of rounds must be one of 4, 5, 8, 9, 12, or 13.";
        private const string NotSupportedParticipantCountMessage = "Number of participants should be a multiple of 4.";

        public CyclicGenerator(ICyclicSeedGenerator cyclicSeedGenerator)
        {
            _cyclicSeedGenerator = cyclicSeedGenerator;
        }

        /// <exception cref="InvalidParameterException"/>
        /// <exception cref="SeedNotFoundException"/>
        public Bracket GenerateBracket(int roundCount, int participantCount)
        {
            ValidateRoundCount(roundCount);
            ValidateParticipantCount(participantCount);

            IEnumerable<(int, int)> seed = _cyclicSeedGenerator.GenerateSeed(roundCount / 4, participantCount / 4);

            IEnumerable<BracketRound> rounds = seed.SelectMany((p, i) => FourRoundsFromSeed(p, participantCount / 4, 4 * i));
            if (roundCount % 4 == 1)
            {
                rounds = rounds.Prepend(ZeroRound(participantCount));
            }

            return new Bracket
            {
                Rounds = rounds.ToList()
            };
        }

        private static BracketRound ZeroRound(int participantCount) =>
            new BracketRound
            {
                Games = Enumerable.Range(0, participantCount / 4).Select(i =>
                    new BracketGame
                    {
                        ParticipantNrs = Enumerable.Repeat(i, 4).Zip(Enumerable.Range(1, 4)).Select(CoordinatesToNumber)
                    })
            };

        private static IEnumerable<BracketRound> FourRoundsFromSeed((int, int) seedPair, int modulus, int shift)
        {
            List<int> diff = new List<int> { seedPair.Item1, seedPair.Item2, -seedPair.Item1, -seedPair.Item2 };

            return Enumerable.Range(0, 4).Select(i => new BracketRound
            {
                Games = Enumerable.Range(1, modulus).Select(j => new BracketGame
                {
                    ParticipantNrs = Enumerable.Range(0, 4)
                        .Select(x => Enumerable.Range(i, x).Sum(y => diff[y % 4]))
                        .Select(x => (x + j + i + shift) % modulus) // As `i` also contributes to shift
                        .Zip(Enumerable.Range(1, 4))
                        .Select(CoordinatesToNumber)
                        .OrderBy(x => x)
                        .ToList()
                }).ToList()
            });
        }

        private static int CoordinatesToNumber((int, int) coordinates) => coordinates.Item1 * 4 + coordinates.Item2;

        /// <exception cref="InvalidParameterException"/>
        private void ValidateRoundCount(int roundCount)
        {
            if (roundCount <= 0 || roundCount % 4 == 2 || roundCount % 4 == 3)
            {
                throw new InvalidParameterException(NotSupportedRoundCountMessage);
            }
        }

        /// <exception cref="InvalidParameterException"/>
        private void ValidateParticipantCount(int participantCount)
        {
            if (participantCount % 4 != 0)
            {
                throw new InvalidParameterException(NotSupportedParticipantCountMessage);
            }
        }
    }
}
