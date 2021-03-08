using System.Linq;
using Autofac;

namespace StudyReporter.DependencyInjection
{
    internal class AutofacMainModule : Module
    {
        private const string _dtoPostfix = "Dto";

        private const string _parserPostfix = "Parser";

        private const string _validatorPostfix = "Validator";

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith(_dtoPostfix))
                .AsSelf();

            builder.RegisterAssemblyTypes(ThisAssembly)
                 .Where(t => t.Name.EndsWith(_parserPostfix))
                 .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith(_validatorPostfix))
                .AsImplementedInterfaces();
        }
    }
}
