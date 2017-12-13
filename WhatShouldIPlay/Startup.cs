using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WhatShouldIPlay.Startup))]
namespace WhatShouldIPlay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
