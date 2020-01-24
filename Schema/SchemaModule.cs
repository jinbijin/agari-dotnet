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
            builder.RegisterType<BracketGameType>().AsSelf();
            builder.RegisterType<BracketRoundType>().AsSelf();
            builder.RegisterType<BracketType>().AsSelf();

            builder.RegisterType<GenerateBracketQuery>().AsSelf();

            builder.RegisterType<AgariQuery>().AsSelf();

            builder.RegisterType<AgariSchema>().As<ISchema>();
        }
    }
}
