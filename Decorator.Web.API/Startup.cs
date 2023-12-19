using Decorator.Business.Service;
using Decorator.Data.EF;
using Decorator.DataAccess.Repository;
using Decorator.DataAccess.Repository.Decorator;
using Decorator.DataAccess.Repository.Decorator.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Decorator.Web.API
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
            services.AddMemoryCache();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Decorator.Web.API", Version = "v1" });
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("myDatabase");
            });

            services.AddScoped<IProductRepository, ProductRepository>().
                    Decorate<IProductRepository,CachingDecoratorRepository>().
                    Decorate<IProductRepository,LoggingDecoratorRepository>();

            services.AddEntityFrameworkInMemoryDatabase();

            services.AddScoped<IProductService, ProductService>();

            ////services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IProductRepository>(sp =>
            //{
            //    var appDbContext = sp.GetRequiredService<AppDbContext>();

            //    var productRepository = new ProductRepository(appDbContext);

            //    var memoryCache = sp.GetRequiredService<IMemoryCache>();
            //    var logProvider = sp.GetRequiredService<ILogger<LoggingDecoratorRepository>>();

            //    var cacheDecorator = new CachingDecoratorRepository(productRepository, memoryCache);

            //    var logDecorator = new LoggingDecoratorRepository(cacheDecorator, logProvider);

            //    return logDecorator;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Decorator.Web.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
