using System.Diagnostics.CodeAnalysis;

namespace GraphQlApi.Tests.Variables
{
    public class GenerateBracketVariables
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "JSON property")]
        public int roundCount { get; set; }
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "JSON property")]
        public int participantCount { get; set; }
    }
}
