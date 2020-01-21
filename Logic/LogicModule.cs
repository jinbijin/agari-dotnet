using Autofac;
using Logic.BracketGenerators.RoundRobin.Toroidal;

namespace Logic
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ToroidalSeedGenerator>().As<IToroidalSeedGenerator>();
            builder.RegisterType<ToroidalGenerator>().As<IToroidalGenerator>();
        }
    }
}
