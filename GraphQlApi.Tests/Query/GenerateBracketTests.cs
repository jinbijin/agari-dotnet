using System.Threading.Tasks;
using GraphQlApi.Tests.Fixture;
using GraphQlApi.Tests.Variables;
using Snapshooter.Xunit;
using Xunit;

namespace GraphQlApi.Tests.Query
{
    public class GenerateBracketTests : GraphQlApiTest<GenerateBracketVariables>, IClassFixture<WebApiFixture>
    {
        private const string Query = "query GenerateBracket($roundCount: Int!, $participantCount: Int!) { generateBracket(roundCount: $roundCount, participantCount: $participantCount) { rounds { games { participantNrs } } } }";

        public GenerateBracketTests(WebApiFixture webApiFixture) : base(webApiFixture, Query)
        {
        }

        [Fact(DisplayName = "generateBracket OK")]
        public Task OkTest() =>
            MatchResponseForRequest(new GenerateBracketVariables
            {
                roundCount = 4,
                participantCount = 20
            }, Snapshot.FullName());

        [Fact(DisplayName = "generateBracket Error: not enough participants")]
        public Task NotEnoughParticipantsTest() =>
            MatchResponseForRequest(new GenerateBracketVariables
            {
                roundCount = 4,
                participantCount = 16
            }, Snapshot.FullName());

        [Fact(DisplayName = "generateBracket Error: not enough rounds")]
        public Task NotEnoughRoundsTest() =>
            MatchResponseForRequest(new GenerateBracketVariables
            {
                roundCount = 3,
                participantCount = 20
            }, Snapshot.FullName());

        [Fact(DisplayName = "generateBracket Error: invalid participant count")]
        public Task InvalidParticipantCountTest() =>
            MatchResponseForRequest(new GenerateBracketVariables
            {
                roundCount = 4,
                participantCount = 21
            }, Snapshot.FullName());
    }
}
