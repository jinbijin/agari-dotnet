using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Logic.BracketGenerators.RoundRobin.Toroidal
{
    public class ToroidalSeedGenerator : IToroidalSeedGenerator
    {
        private const string NotFoundMessage = "Could not find valid seed for the toroidal bracket generator.";

        /// <exception cref="SeedNotFoundException"/>
        public IEnumerable<(int, int)> GenerateSeed(int count, int modulus)
        {
            return GenerateSeed(count, modulus, ImmutableSortedSet<int>.Empty, ImmutableSortedSet<int>.Empty);
        }

        /// <exception cref="SeedNotFoundException"/>
        private IEnumerable<(int, int)> GenerateSeed(int count, int modulus,
            ImmutableSortedSet<int> usedAdjacent, ImmutableSortedSet<int> usedOpposite)
        {
            if (count == 0)
            {
                return Enumerable.Empty<(int, int)>();
            }

            IEnumerable<int> loopEnumerable = Enumerable.Range(1, (modulus - 1) / 2)
                .Where(x => !usedAdjacent.Contains(x))
                .ToList();
            foreach (var first in loopEnumerable)
            {
                foreach (var second in loopEnumerable.Where(x => x > first))
                {
                    try
                    {
                        var sum = first + second > (modulus / 2) ? modulus - first - second : first + second;
                        if (usedOpposite.Contains(second - first) ||
                            usedOpposite.Contains(sum))
                        {
                            continue;
                        }
                        return GenerateSeed(count - 1, modulus,
                            usedAdjacent.Add(first).Add(second),
                            usedAdjacent.Add(second - first).Add(sum))
                                .Prepend((first, second));
                    }
                    catch (SeedNotFoundException)
                    {
                        continue;
                    }
                }
            }
            throw new SeedNotFoundException(NotFoundMessage);
        }
    }
}
