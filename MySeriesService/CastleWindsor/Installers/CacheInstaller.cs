using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MySeriesService.Cache;
using MySeriesService.Interfaces;

namespace MySeriesService.CastleWindsor.Installers
{
    public class CacheInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISeriesCache>()
                .ImplementedBy<SeriesCache>());
        }
    }
}
