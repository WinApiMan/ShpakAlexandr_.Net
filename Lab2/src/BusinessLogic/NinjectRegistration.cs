using AutoMapper;
using BusinessLogic.Services.Mapper;
using BusinessLogic.Services.ReadWriteServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ninject.Modules;
using Ninject.Web.Common;
using System.IO;
using Taxi.BusinessLogic.Processings;
using Taxi.DAL.Interfaces;
using Taxi.DAL.Models;
using TaxiDAL.Repositorie;

namespace BusinessLogic
{
    public class NinjectRegistration : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<TaxiProfile>(); });
            Bind<IMapper>().ToConstructor(c => new Mapper(mapperConfiguration)).InSingletonScope();
            var options = new DbContextOptionsBuilder<TaxiContext>().UseLazyLoadingProxies().UseSqlServer(GetConnectionString()).UseLoggerFactory(TaxiLoggerFactory).Options;
            Bind<DbContext>().To<TaxiContext>().InRequestScope().WithConstructorArgument("options", options);
            Bind<IRepository<Car>>().To<TaxiRepository<Car>>();
            Bind<IRepository<Client>>().To<TaxiRepository<Client>>();
            Bind<IRepository<Driver>>().To<TaxiRepository<Driver>>();
            Bind<IRepository<Order>>().To<TaxiRepository<Order>>();
            Bind<DataBaseFromFile>().ToSelf();
            Bind<FileFromDataBase>().ToSelf();
            Bind<CarService>().ToSelf();
            Bind<DriverService>().ToSelf();
            Bind<OrderService>().ToSelf();
            Bind<TaxiProfile>().ToSelf();
        }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config.GetConnectionString("TaxiConnection");
        }

        public static readonly ILoggerFactory TaxiLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder
            .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
            .AddFile("Logs/SQL-{Date}.txt");
        });
    }
}