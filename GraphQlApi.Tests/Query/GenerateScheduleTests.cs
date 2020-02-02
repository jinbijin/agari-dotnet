using System.Threading.Tasks;
using GraphQlApi.Tests.Fixture;
using GraphQlApi.Tests.Variables;
using Snapshooter.Xunit;
using Xunit;

namespace GraphQlApi.Tests.Query
{
    public class GenerateScheduleTests : GraphQlApiTest<GenerateScheduleVariables>, IClassFixture<WebApiFixture>
    {
        private const string Query = "query GenerateSchedule($roundCount: Int!, $participantCount: Int!) { generateSchedule(roundCount: $roundCount, participantCount: $participantCount) { rounds { games { participantNrs } } } }";

        public GenerateScheduleTests(WebApiFixture webApiFixture) : base(webApiFixture, Query)
        {
        }

        [Fact(DisplayName = "generateSchedule OK")]
        public Task OkTest() =>
            MatchResponseForRequest(new GenerateScheduleVariables
            {
                roundCount = 4,
                participantCount = 20
            }, Snapshot.FullName());

        [Fact(DisplayName = "generateSchedule Error: not enough participants")]
        public Task NotEnoughParticipantsTest() =>
            MatchResponseForRequest(new GenerateScheduleVariables
            {
                roundCount = 4,
                participantCount = 16
            }, Snapshot.FullName());

        [Fact(DisplayName = "generateSchedule Error: not enough rounds")]
        public Task NotEnoughRoundsTest() =>
            MatchResponseForRequest(new GenerateScheduleVariables
            {
                roundCount = 3,
                participantCount = 20
            }, Snapshot.FullName());

        [Fact(DisplayName = "generateSchedule Error: invalid participant count")]
        public Task InvalidParticipantCountTest() =>
            MatchResponseForRequest(new GenerateScheduleVariables
            {
                roundCount = 4,
                participantCount = 21
            }, Snapshot.FullName());
    }
}
