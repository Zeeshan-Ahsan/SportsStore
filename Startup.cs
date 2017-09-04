using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using SportsStore.Controllers;
using Microsoft.AspNetCore.Http;

namespace SportsStore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(env.ContentRootPath)
                        .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();

            var connectionString = "Host=localhost;Database=SportsStore;Username=postgres;Password=123456;Port=5432;"; //Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSession();
            app.UseStatusCodePages();
            app.UseMvc(routes =>
            {
                // routes.MapRoute(
                // name: null,
                // template: "{controller=Product}/{action=List}/{category}/Page{page:int}"
                // //defaults: new { Controller = "Product", Action = "List" }
                // );

                // routes.MapRoute(
                // name: null,
                // template: "Page{page:int}",
                // defaults: new { Controller = "Product", Action = "List", page = 1 }
                // );

                routes.MapRoute(
                name: null,
                template: "{category}",
                defaults: new { controller = "Product", action = "List" }
                );

                // routes.MapRoute(
                // name: null,
                // template: "",
                // defaults: new { Controller = "Product", Action = "List", page = 1 });

                routes.MapRoute(name: null,
                template: "{controller}/{action}",
                defaults: new { controller = "Product", action = "List" });
            });

            //SeedData.EnsurePopulated(app);
        }
    }
}
