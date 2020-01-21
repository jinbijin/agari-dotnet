using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Logic.Types.Exceptions;

namespace Logic.BracketGenerators.RoundRobin.Cyclic
{
    public class CyclicSeedGenerator : ICyclicSeedGenerator
    {
        private const string NotFoundMessage = "Could not find valid seed for the cyclic bracket generator.";

        /// <exception cref="SeedNotFoundException"/>
        public async Task<IEnumerable<List<int>>> GenerateSeed(int count, int modulus)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            try
            {
                cts.CancelAfter(2000);
                return await Task.Run(() => GenerateSeed(count, modulus,
                    Enumerable.Repeat(ImmutableSortedSet<int>.Empty, 4).ToList(),
                    Enumerable.Repeat(ImmutableSortedSet<int>.Empty, 4).ToList(), cts.Token), cts.Token);
            }
            catch (OperationCanceledException)
            {
                throw new SeedNotFoundException(NotFoundMessage);
            }
        }

        /// <exception cref="SeedNotFoundException"/>
        public IEnumerable<List<int>> GenerateSeed(int count, int modulus,
            List<ImmutableSortedSet<int>> usedAdjacentDifferences,
            List<ImmutableSortedSet<int>> usedOppositeDifferences,
            CancellationToken cancellationToken)
        {
            if (count == 0)
            {
                return Enumerable.Empty<List<int>>();
            }
            cancellationToken.ThrowIfCancellationRequested();

            foreach (List<int> tuple in LoopEnumerable(modulus, usedAdjacentDifferences))
            {
                List<int> oppositeDifferences = Enumerable.Range(0, 4)
                    .Select(i => (tuple[i] + tuple[(i + 1) % 4]) % modulus)
                    .ToList();
                if (Enumerable.Range(0, 4).Any(i => usedOppositeDifferences[i].Contains(oppositeDifferences[i])))
                {
                    continue;
                }

                List<ImmutableSortedSet<int>> recAdjacentDifferences = tuple.Zip(usedAdjacentDifferences)
                    .Select(p => p.Second.Add(p.First))
                    .ToList();
                List<ImmutableSortedSet<int>> recOppositeDifferences = oppositeDifferences.Zip(usedOppositeDifferences)
                    .Select(p => p.Second.Add(p.First))
                    .ToList();
                try
                {
                    return GenerateSeed(count - 1, modulus, recAdjacentDifferences, recOppositeDifferences, cancellationToken).Prepend(tuple);
                }
                catch (SeedNotFoundException)
                {
                    continue;
                }
            }
            throw new SeedNotFoundException(NotFoundMessage);
        }

        private IEnumerable<List<int>> LoopEnumerable(int modulus, List<ImmutableSortedSet<int>> usedAdjacentDifferences)
        {
            List<IEnumerable<int>> loopEnumerables = Enumerable.Range(0, 4)
                .Select(i => Enumerable.Range(0, modulus)
                    .Where(x => !usedAdjacentDifferences[i].Contains(x)))
                .ToList();

            foreach (int p in loopEnumerables[0])
            {
                foreach (int q in loopEnumerables[1])
                {
                    foreach (int r in loopEnumerables[2])
                    {
                        foreach (int s in loopEnumerables[3]
                            .Where(x => (p + q + r + x) % modulus == 0))
                        {
                            yield return new List<int> { p, q, r, s };
                        }
                    }
                }
            }
        }
    }
}
