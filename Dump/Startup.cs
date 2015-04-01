using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dump.Startup))]
namespace Dump
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // TODO: Startup shenanigans
        }
    }
}
