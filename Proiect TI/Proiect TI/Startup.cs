using Microsoft.Owin;
using Owin;
using System.Globalization;


[assembly: OwinStartupAttribute(typeof(Proiect_TI.Startup))]
namespace Proiect_TI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
