using System.Collections.Generic;

namespace Logic.RoundRobin
{
    public record RoundRobinSchedule(IReadOnlyCollection<RoundRobinRound> Rounds);
}
