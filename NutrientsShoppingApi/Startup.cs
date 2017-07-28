using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NutrientsShoppingApi.DAL;
using Microsoft.EntityFrameworkCore;
using NutrientsShoppingApi.DAL.Models;
using NutrientsShoppingApi.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace NutrientsShoppingApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

			services.AddCors();
			
			// Add framework services.
			//services.AddDbContext<LimanContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<LimanContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LocalSql")));

			services.AddMvc();
			services.AddScoped<IAflProductRepository,AflProductRepository>();


			// Add Swagger API Documentation
			// Register the Swagger generator, defining one or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Nutrients Shopping API", Version = "v1" });
			});

		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, LimanContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

			app.UseCors(builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().AllowCredentials());

            app.UseMvc();
			//DbInitializer.Initialize(context);

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nutrients Shopping API  V1");
			});
		}

		
    }
}
