using PowerDiaryTestWebApp.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;
using PowerDiaryTestWebApp.Domain.Abstract;

namespace PowerDiaryTestWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.Add("compressed", new CompressedRoute(DependencyResolver.Current.GetService<ICompressedRoutesRepository>()));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }


    }
}
