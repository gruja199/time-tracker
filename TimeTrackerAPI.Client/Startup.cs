using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using TimeTrackerAPI.Client.Security;

namespace TimeTrackerAPI.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthorizationCore();
            services.AddTokenAuthenticationStateProvider();

        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
