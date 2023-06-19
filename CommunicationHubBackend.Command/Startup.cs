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

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Sensitive Words API";
                    document.Info.Description = "a Simple API that takes in a message, validates the message for any sensitive words. If any, replaces them wiht **** List of words can be ammended accoring to client's needs.";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Johan de Kock",
                        Email = "jdekock1184@gmail.com",
                        Url = ""
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = ""
                    };
                };
            });

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
