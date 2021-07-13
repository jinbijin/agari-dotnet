using Logic.Common;
using System.Threading.Tasks;

namespace Logic.RoundRobin
{
    public interface IRoundRobinScheduleGenerator
    {
        Task<Response<RoundRobinSchedule, Error>> GenerateSchedule(int participantCount, int roundCount);
        Task<Response<bool, Error>> ValidateGenerateScheduleRequest(int participantCount, int roundCount);
        Task<Response<int, Error>> GetMaxRounds(int participantCount);
    }
}
