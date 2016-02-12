using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using MySeriesService.CastleWindsor;
using MySeriesService.CastleWindsor.Installers;
using System.Web.Http;

namespace MySeriesService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new WindsorContainer().Install(
                new ControllerInstaller(),
                new DefaultInstaller());
            var httpDependencyResolver = new WindsorDependencyResolver(container);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/series/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.DependencyResolver = httpDependencyResolver;
        }
    }
}
