using Microsoft.Owin;
using Owin;
using WebLibrary;

[assembly: OwinStartup(typeof(Startup))]

namespace WebLibrary
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
