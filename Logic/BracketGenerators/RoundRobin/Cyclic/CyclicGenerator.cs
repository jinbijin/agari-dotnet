using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Types.RoundRobin;
using Logic.Types.Exceptions;

namespace Logic.BracketGenerators.RoundRobin.Cyclic
{
    public class CyclicGenerator : ICyclicGenerator
    {
        private readonly ICyclicSeedGenerator _cyclicSeedGenerator;
        public CyclicGenerator(ICyclicSeedGenerator cyclicSeedGenerator)
        {
            _cyclicSeedGenerator = cyclicSeedGenerator;
        }

        private const string NotSupportedRoundCountMessage = "Number of rounds must be at least 4.";
        private const string NotSupportedParticipantCountMessage = "Number of participants should be a multiple of 4.";
        private const string NotEnoughParticipantsMessage = "There are not enough participants.";

        /// <exception cref="InvalidParameterException"/>
        /// <exception cref="SeedNotFoundException"/>
        public async Task<RoundRobinBracket> GenerateBracket(int roundCount, int participantCount)
        {
            ValidateRoundCount(roundCount);
            ValidateParticipantCount(participantCount);
            ValidateRelativeParticipantCount(roundCount, participantCount);

            IEnumerable<List<int>> seed = await _cyclicSeedGenerator.GenerateSeed(roundCount, participantCount / 4);

            IEnumerable<RoundRobinRound> rounds = seed.Select((p, i) => RoundFromSeed(p, participantCount / 4, i));

            return new RoundRobinBracket
            {
                Rounds = rounds.ToList()
            };
        }

        private static RoundRobinRound RoundFromSeed(List<int> seed, int modulus, int shift) =>
            new RoundRobinRound
            {
                Games = Enumerable.Range(1, modulus).Select(j => new RoundRobinGame
                {
                    ParticipantNrs = Enumerable.Range(0, 4)
                        .Select(x => Enumerable.Range(0, x).Sum(y => seed[y % 4]))
                        .Select(x => (x + j + shift - 1) % modulus)
                        .Zip(Enumerable.Range(1, 4))
                        .Select(CoordinatesToNumber)
                        .OrderBy(x => x)
                        .ToList()
                }).ToList()
            };

        private static int CoordinatesToNumber((int, int) coordinates) => coordinates.Item1 * 4 + coordinates.Item2;

        /// <exception cref="InvalidParameterException"/>
        private void ValidateRoundCount(int roundCount)
        {
            if (roundCount < 4)
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

        /// <exception cref="InvalidParameterException"/>
        private void ValidateRelativeParticipantCount(int roundCount, int participantCount)
        {
            if (participantCount < 4 * (roundCount + ((roundCount + 1) % 2)))
            {
                throw new InvalidParameterException(NotEnoughParticipantsMessage);
            }
        }
    }
}
