using System.Collections.Generic;
using System.Linq;

namespace Logic.Types.Bracket
{
    public class Bracket
    {
        public IEnumerable<BracketRound> Rounds { get; set; } = Enumerable.Empty<BracketRound>();
    }
}
