using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MySeriesService.Interfaces;
using MySeriesService.SeriesEvaluator.Fibonacci;

namespace MySeriesService.CastleWindsor.Installers
{
    class DefaultInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .Pick()
                .WithServiceDefaultInterfaces()
                .Configure(c => c.LifestyleTransient()));
            
        }
    }
}
