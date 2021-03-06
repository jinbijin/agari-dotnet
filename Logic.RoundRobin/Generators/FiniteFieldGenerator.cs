﻿using AppliedAlgebra.GfPolynoms.GaloisFields;
using Logic.Common.Random;
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

        public IReadOnlyCollection<string> RandomnessSources { get; } = new List<string>
        {
            "Based on a fixed schedule.",
            "Participant numbers are randomized, except for the highest one.",
            "Table numbers are randomized, except for the last one.",
            "Round selection from the schedule is randomized."
        };

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
            return new RoundRobinSchedule(await GenerateRounds(participantCount).Take(roundCount).ToListAsync(), RandomnessSources);
        }

        private async IAsyncEnumerable<RoundRobinRound> GenerateRounds(int participantCount)
        {
            EnsureFieldGenerated(participantCount);
            int order = _generated.Value.Field.Order;
            int quarter = (order - 1) / 4;

            IList<int> shuffle = Enumerable.Range(0, 3 * order).Shuffle();
            shuffle.Add(3 * order);

            foreach (int element in Enumerable.Range(0, order).Shuffle())
            {
                IEnumerable<RoundRobinGame> games = Enumerable.Range(0, 3)
                    .SelectMany(rootIndex => Enumerable.Range(0, quarter).Select(power => (rootIndex, power)))
                    .Select(pair => new RoundRobinGame(new List<int>
                    {
                        shuffle[_generated.Value.Field.Add(element,_generated.Value.Field.PowGeneratingElement(pair.power)) + pair.rootIndex * order],
                        shuffle[_generated.Value.Field.Add(element,_generated.Value.Field.PowGeneratingElement(pair.power + 2 * quarter)) + pair.rootIndex * order],
                        shuffle[_generated.Value.Field.Add(element,_generated.Value.Field.PowGeneratingElement(pair.power + quarter)) + ((pair.rootIndex + 1) % 3) * order],
                        shuffle[_generated.Value.Field.Add(element,_generated.Value.Field.PowGeneratingElement(pair.power + 3 * quarter)) + ((pair.rootIndex + 1) % 3) * order],
                    }.OrderBy(value => value).ToList())).Shuffle()
                    .Concat(Enumerable.Repeat(new RoundRobinGame(new List<int> { shuffle[element], shuffle[element + order], shuffle[element + 2 * order], shuffle[3 * order] }.OrderBy(value => value).ToList()), 1));
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
