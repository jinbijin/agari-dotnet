using System.Collections.Generic;

namespace Logic.RoundRobin
{
    public record RoundRobinRound(IReadOnlyCollection<RoundRobinGame> Games);
}
