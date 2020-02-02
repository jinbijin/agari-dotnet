using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.Types.Exceptions;

namespace Logic.ScheduleGenerators.RoundRobin.Cyclic
{
    public interface ICyclicSeedGenerator
    {
        /// <exception cref="SeedNotFoundException"/>
        Task<IEnumerable<List<int>>> GenerateSeed(int count, int modulus);
    }
}
