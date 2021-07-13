using AppliedAlgebra.GfPolynoms.GaloisFields;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.RoundRobin.Generators
{
    internal class FiniteFieldGenerator : IScheduleGenerator
    {
        private (GaloisField Field, int ParticipantCount)? _generated;

        public Specificity Specificity => Specificity.Special;

        public int? MaxRoundCount(int participantCount)
        {
            if (participantCount <= 4 || participantCount % 12 != 4) { return null; }
            try
            {
                EnsureFieldGenerated(participantCount);
                return _generated.Value.Field.Order;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public async Task<RoundRobinSchedule> GenerateSchedule(int participantCount, int roundCount)
        {
            return new RoundRobinSchedule(await GenerateRounds(participantCount).Take(roundCount).ToListAsync());
        }

        private async IAsyncEnumerable<RoundRobinRound> GenerateRounds(int participantCount)
        {
            EnsureFieldGenerated(participantCount);
            int order = _generated.Value.Field.Order;
            int quarter = (order - 1) / 4;

            for (int element = 0; element < order; element++)
            {
                IEnumerable<RoundRobinGame> games = Enumerable.Range(0, 3)
                    .SelectMany(rootIndex => Enumerable.Range(0, quarter).Select(power => (rootIndex, power)))
                    .Select(pair => new RoundRobinGame(new List<int>
                    {
                        _generated.Value.Field.Add(element,_generated.Value.Field.PowGeneratingElement(pair.power)) + pair.rootIndex * order,
                        _generated.Value.Field.Add(element,_generated.Value.Field.PowGeneratingElement(pair.power + 2 * quarter)) + pair.rootIndex * order,
                        _generated.Value.Field.Add(element,_generated.Value.Field.PowGeneratingElement(pair.power + quarter)) + ((pair.rootIndex + 1) % 3) * order,
                        _generated.Value.Field.Add(element,_generated.Value.Field.PowGeneratingElement(pair.power + 3 * quarter)) + ((pair.rootIndex + 1) % 3) * order,
                    }.OrderBy(value => value).ToList()))
                    .Concat(Enumerable.Repeat(new RoundRobinGame(new List<int> { element, element + order, element + 2 * order, 3 * order }), 1));
                yield return new RoundRobinRound(games.ToList());
            }
        }

        [MemberNotNull(nameof(_generated))]
        private void EnsureFieldGenerated(int participantCount)
        {
            int order = (participantCount - 1) / 3;
            if (!_generated.HasValue || _generated.Value.ParticipantCount != participantCount)
            {
                _generated = (GaloisField.Create(order), participantCount);
            }
        }
    }
}
