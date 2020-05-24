using AutoMapper;
using BusinessLogic.Services.Mapper;
using BusinessLogic.Services.ReadWriteServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ninject.Modules;
using Ninject.Web.Common;
using System.IO;
using Taxi.ConsoleUI.ConsoleServices;
using Taxi.ConsoleUI.Interfaces;
using Taxi.DAL.Interfaces;
using Taxi.DAL.Models;
using TaxiDAL.Repositories;

namespace Taxi.ConsoleUI.Configuration
{
    public class NinjectConfiguration : NinjectModule
    {
        public override void Load()
        {
            var config = GetConfig();
            var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<TaxiProfile>(); });
            Bind<IMapper>().ToConstructor(c => new Mapper(mapperConfiguration)).InSingletonScope();
            var options = new DbContextOptionsBuilder<TaxiContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer(config.GetConnectionString("TaxiConnection"))
                .UseLoggerFactory(TaxiLoggerFactory(config)).Options;
            Bind<DbContext>().To<TaxiContext>().InRequestScope().WithConstructorArgument("options", options);
            Bind<IRepository<CarDto>>().To<TaxiRepository<CarDto>>();
            Bind<IRepository<ClientDto>>().To<TaxiRepository<ClientDto>>();
            Bind<IRepository<DriverDto>>().To<TaxiRepository<DriverDto>>();
            Bind<IRepository<OrderDto>>().To<TaxiRepository<OrderDto>>();
            Bind<IConfiguration>().ToConstant(config);
            Bind<ILoggerFactory>().ToConstant(TaxiLoggerFactory(config));
            Bind<FileFromDataBase>().ToSelf();
            Bind<DataBaseFromFile>().ToSelf();
            Bind<TaxiProfile>().ToSelf();
            Bind<BusinessLogic.Processings.CarService>().ToSelf();
            Bind<BusinessLogic.Processings.DriverService>().ToSelf();
            Bind<BusinessLogic.Processings.OrderService>().ToSelf();
            Bind<IConsoleService<CarService>>().To<CarService>();
            Bind<IConsoleService<DriverService>>().To<DriverService>();
            Bind<IConsoleService<OrderService>>().To<OrderService>();
            Bind<IConsoleService<AdminService>>().To<AdminService>();
            Bind<RoleService>().ToSelf();
        }

        private IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config;
        }

        private ILoggerFactory TaxiLoggerFactory(IConfiguration configuration) => LoggerFactory.Create(builder =>
        {
            builder.AddConfiguration(configuration);
        });
    }
}