using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MySeriesService.Cache;
using MySeriesService.Interfaces;
using MySeriesService.SeriesEvaluator.Fibonacci;

namespace MySeriesService.CastleWindsor.Installers
{
    class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().Pick().If(t => t.Name.EndsWith("ApiController"))
                .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                .LifestylePerWebRequest());

            container.Register(Component.For<ISeriesLogic>()
                .ImplementedBy<SeriesLogic.SeriesLogic>());
            container.Register(Component.For<ISeriesEvaluator>()
                .ImplementedBy<FibonacciSeriesEvaluator>());
            container.Register(Component.For<ISeriesCache>()
                .ImplementedBy<SeriesCache>());

            container.Kernel.Resolver.AddSubResolver(
                new CollectionResolver(container.Kernel, true));
        }
    }
}
