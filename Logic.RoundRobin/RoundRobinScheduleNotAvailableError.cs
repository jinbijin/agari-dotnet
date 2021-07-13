using Logic.Common;

namespace Logic.RoundRobin
{
    public sealed class RoundRobinScheduleNotAvailableError : Error
    {
        public RoundRobinScheduleNotAvailableError() : base("Round-robin schedule doesn't exist.")
        {
        }
    }
}
