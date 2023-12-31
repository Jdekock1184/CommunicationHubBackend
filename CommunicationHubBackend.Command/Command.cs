using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Data;

namespace CommunicationHubBackend.Command
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance.
    /// </summary>
    internal sealed class Command : StatelessService
    {
        public Command(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                        //var builder = WebApplication.CreateBuilder();

                        //builder.Services.AddSingleton<StatelessServiceContext>(serviceContext);
                        //builder.WebHost
                        //            .UseKestrel()
                        //            .UseStartup<Startup>()
                        //            .UseContentRoot(Directory.GetCurrentDirectory())
                        //            .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                        //            .UseUrls(url);
                        //var app = builder.Build();
                        
                        //app.MapGet("/", () => "Hello World!");
                        
                        
                        //return app;
                        return new WebHostBuilder()
                                    .UseKestrel()
                                    .ConfigureServices(
                                        services => {
                                        services.AddSingleton<StatelessServiceContext>(serviceContext);
                                    })
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseStartup<Startup>()
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                    .UseUrls(url)
                                    .Build();
                    }))
            };
        }
    }
}
