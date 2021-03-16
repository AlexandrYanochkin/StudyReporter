using Autofac;
using MediatR;

namespace StudyReporter.DependencyInjection
{
    internal class AutofacMediatRModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
               .RegisterType<Mediator>()
               .As<IMediator>()
               .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(t =>
            {
                var componentContext = t.Resolve<IComponentContext>();

                return type => componentContext.Resolve(type);
            });

            builder
                .RegisterAssemblyTypes(typeof(AutofacMediatRModule).Assembly)
                .AsImplementedInterfaces();
        }
    }
}
