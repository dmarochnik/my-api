using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    p => string.Format("{0} cannot be null or empty.", p));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "myAPI", Version = "v1" });
            });

            //adding JWT to dependency injection container
            services.Add(new ServiceDescriptor(typeof(IJWTAuthentication), new JWTAuthentication("2e6a98abb5b23339ad14601d3bedc1d23847498cb18daf8cfc98c2a2095ec8f47d80053f6d4e22b8f6419407ac3083dc")));
            services.Add(new ServiceDescriptor(
                typeof(DbContextOptions<ApplicationDbContext>),
                new DbContextOptionsBuilder<ApplicationDbContext>().UseMySQL(Configuration.GetConnectionString("DefaultConnection"))
                .Options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "myAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
