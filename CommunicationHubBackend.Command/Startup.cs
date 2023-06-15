using CommunicationHubBackend.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CommunicationHubBackend.Command
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = (IConfigurationRoot)configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            
            // API
            services.AddRouting();

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                .AllowAnyMethod()
                                                                .AllowAnyHeader()));

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddMediatR((x) => x.RegisterServicesFromAssemblyContaining<AssemblyMarker>());

            services.AddSwaggerDocument();

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseCors("AllowAll");

            //// Enable middleware to serve generated Swagger as a JSON endpoint.
            //app.UseSwagger(c => c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            //{
            //    swaggerDoc.Servers = new List<OpenApiServer> {
            //        new OpenApiServer { Url = $"", Description = "Development Server" }
            //    };
            //}));

            //// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            //// specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Communication Hub Command Local");
            //    c.SwaggerEndpoint($"", "Communication Hub Command Development");
            //    c.RoutePrefix = string.Empty;
            //});

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
