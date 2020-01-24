using System.Collections.Generic;
using System.Linq;

namespace Logic.Types.RoundRobin
{
    public class RoundRobinRound
    {
        public IEnumerable<RoundRobinGame> Games { get; set; } = Enumerable.Empty<RoundRobinGame>();
    }
}
