using Autofac;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.WebAPI.Controllers;
using WebApplication2.Service;
using WebApplication2.Service.Common;
using Repository;
using WebApplication2.Repository.Common;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace WebApplication2.WebAPI.App_Start
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        { 

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<MachineService>().As<IMachineService>();
            builder.RegisterType<MachineRepository>().As<IMachineRepository>();

            builder.RegisterType<ManufacturerService>().As<IManufacturerService>();
            builder.RegisterType<ManufacturerRepository>().As<IManufacturerRepository>();


            
            var container = builder.Build();
            return container;
        }
        
    }
}