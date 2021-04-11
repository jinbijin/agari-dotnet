using Autofac;
using Logic.RoundRobin.Generators;
using Logic.RoundRobin.Implementations;

namespace Logic.RoundRobin
{
    public class RoundRobinModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AffineF4Generator>().As<IScheduleGenerator>();
            builder.RegisterType<RoundRobinScheduleGenerator>().As<IRoundRobinScheduleGenerator>();
        }
    }
}
