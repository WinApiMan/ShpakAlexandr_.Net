using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Services.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Taxi.BusinessLogic.Interfaces;
using Taxi.BusinessLogic.Services;
using Taxi.DAL.Interfaces;
using Taxi.DAL.Models;
using Taxi.WebUI.Mapper;
using TaxiDAL.Repositories;

namespace Taxi.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddDbContext<TaxiContext>(options => options
            .UseSqlServer(Configuration.GetConnectionString("TaxiConnection"))
            .UseLoggerFactory(TaxiLoggerFactory(Configuration)));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<TaxiContext>();

            services.AddTransient<IRepository<CarDto>, TaxiRepository<CarDto>>();
            services.AddTransient<IRepository<ClientDto>, TaxiRepository<ClientDto>>();
            services.AddTransient<IRepository<DriverDto>, TaxiRepository<DriverDto>>();
            services.AddTransient<IRepository<OrderDto>, TaxiRepository<OrderDto>>();

            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IDriverService, DriverService>();
            services.AddTransient<IOrderService, OrderService>();

            services.AddTransient<TaxiProfile>();
            services.AddTransient<FileFromDataBase>();
            services.AddTransient<DataBaseFromFile>();

            services.AddTransient<IReader, JsonReader>();
            services.AddTransient<IWriter, JsonWriter>();

            services.AddAutoMapper(typeof(TaxiProfile), typeof(TaxiUIProfile));
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Cars/Error");

                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "TaxiApi");
                swagger.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private ILoggerFactory TaxiLoggerFactory(IConfiguration configuration) => LoggerFactory.Create(builder =>
        {
            builder.AddConfiguration(configuration);
        });
    }
}