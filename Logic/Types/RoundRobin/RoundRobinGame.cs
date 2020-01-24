using System.Collections.Generic;
using System.Linq;

namespace Logic.Types.RoundRobin
{
    public class RoundRobinGame
    {
        public IEnumerable<int> ParticipantNrs { get; set; } = Enumerable.Empty<int>();
    }
}
