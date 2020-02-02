﻿using Autofac;
using Logic.ScheduleGenerators.RoundRobin.Cyclic;

namespace Logic
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CyclicSeedGenerator>().As<ICyclicSeedGenerator>();
            builder.RegisterType<CyclicGenerator>().As<ICyclicGenerator>();
        }
    }
}
