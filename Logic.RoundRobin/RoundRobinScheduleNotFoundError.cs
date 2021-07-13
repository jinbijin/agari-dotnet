using Logic.Common;

namespace Logic.RoundRobin
{
    public sealed class RoundRobinScheduleNotFoundError : Error
    {
        public RoundRobinScheduleNotFoundError() : base("Round robin schedule not found.")
        {
        }
    }
}
