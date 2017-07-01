using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PowerDiaryTestWebApp.Domain.Concrete;

namespace PowerDiaryTestWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<PowerDiaryDbContext>());

            using (var db = new PowerDiaryDbContext())
            {
                if (!db.Database.CompatibleWithModel(false))
                {
                    throw new Exception("Database is not compatible with a model");
                }
                var routes = db.CompressedRoutes.ToList();
                foreach (var r in routes)
                {
                    Console.WriteLine($"Existing route: {r.CompressedUri} => {r.Uri}");
                }
            }


            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
