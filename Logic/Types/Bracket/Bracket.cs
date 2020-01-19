using System.Collections.Generic;

namespace Logic.Types.Bracket
{
    public class Bracket
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public IEnumerable<BracketRound> Rounds { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    }
}
