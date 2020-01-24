using System.Collections.Generic;
using System.Linq;

namespace Logic.Types.RoundRobin
{
    public class RoundRobinBracket
    {
        public IEnumerable<RoundRobinRound> Rounds { get; set; } = Enumerable.Empty<RoundRobinRound>();
    }
}
