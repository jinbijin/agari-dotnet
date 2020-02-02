using Autofac;
using GraphQL.Types;
using Schema.Query;
using Schema.Types;

namespace Schema
{
    public class SchemaModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ScheduleGameType>().AsSelf();
            builder.RegisterType<ScheduleRoundType>().AsSelf();
            builder.RegisterType<ScheduleType>().AsSelf();

            builder.RegisterType<GenerateScheduleQuery>().AsSelf();

            builder.RegisterType<AgariQuery>().AsSelf();

            builder.RegisterType<AgariSchema>().As<ISchema>();
        }
    }
}
