using Autofac;

namespace StudyReporter.DependencyInjection
{
    public static class ContainerFactory
    { 
        public static IContainer GetContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new AutofacMainModule());

            containerBuilder.RegisterModule(new AutofacMediatRModule());

            var container = containerBuilder.Build();

            return container;
        }
    }
}
