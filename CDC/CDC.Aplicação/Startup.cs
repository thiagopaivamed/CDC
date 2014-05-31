using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CDC.Aplicação.Startup))]
namespace CDC.Aplicação
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
