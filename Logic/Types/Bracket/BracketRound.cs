using System.Collections.Generic;
using System.Linq;

namespace Logic.Types.Bracket
{
    public class BracketRound
    {
        public IEnumerable<BracketGame> Games { get; set; } = Enumerable.Empty<BracketGame>();
    }
}
