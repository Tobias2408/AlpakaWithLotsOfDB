using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Neo4j.Driver;
using Neoflix.Services;

namespace Neoflix
{
    public class Startup
    {
      

        public void ConfigureServices(IServiceCollection services)
        {
            // Add API controllers
            services.AddControllers();

            // Add JWT authorization/authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtHelper.ConfigureJwt);

            // Use the Config class to retrieve Neo4j configuration values
            var neo4jConfig = Config.UnpackNeo4jConfig();
            var uri = neo4jConfig.Uri;
            var user = neo4jConfig.Username;
            var password = neo4jConfig.Password;

            // Initialize and return the Neo4j driver
            services.AddSingleton<IDriver>(provider =>
            {
                // Use the retrieved configuration values
                var driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
                return driver;
            });
            services.AddSingleton<UserService>();
            services.AddSingleton<CustomerService>();
            services.AddSingleton<LocationService>();


            services.AddSingleton<AlpakaService>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Neoflix API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Use Developer Exception Page in Development environment
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Neoflix API V1");
                c.RoutePrefix = "Swagger"; // Sets the route to serve Swagger UI
            });

            // Add static file serving for Vue app
            app.UseFileServer();

            // Add routing
            app.UseRouting();

            // Add authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Register controllers
            app.UseEndpoints(builder => builder.MapControllers());
        }
    }
}