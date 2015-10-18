using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Domain.DataAccess.Repositories;
using Domain.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using WebLibrary.Hubs;

namespace WebLibrary
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            var container = BuildContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            var hubActivator = new SimpleInjectorHubActivator(container);
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => hubActivator);
        }

        private static Container BuildContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.Register<IColorRepository, ColorRepository>(Lifestyle.Singleton);
            container.Register<IColorService, ColorService>();

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            return container;
        }
    }
}