using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Services.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ninject.Modules;
using Ninject.Web.Common;
using System.IO;
using Taxi.BusinessLogic.Interfaces;
using Taxi.BusinessLogic.Processings;
using Taxi.ConsoleUI.Interfaces;
using Taxi.ConsoleUI.TaxiServices;
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
            Bind<IReader>().To<JsonReader>();
            Bind<IWriter>().To<JsonWriter>();
            Bind<TaxiProfile>().ToSelf();

            Bind<CarService>().ToSelf();
            Bind<DriverService>().ToSelf();
            Bind<OrderService>().ToSelf();
            Bind<RoleService>().ToSelf();

            Bind<IConsoleService<CarConsoleService>>().To<CarConsoleService>();
            Bind<IConsoleService<DriverConsoleService>>().To<DriverConsoleService>();
            Bind<IConsoleService<OrderConsoleService>>().To<OrderConsoleService>();
            Bind<IConsoleService<AdminConsoleService>>().To<AdminConsoleService>();

            Bind<ICarService>().To<CarService>();
            Bind<IDriverService>().To<DriverService>();
            Bind<IOrderService>().To<OrderService>();
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