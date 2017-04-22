﻿using Ninject;
using OnlineStore.Domain;
using OnlineStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // Здесь размещаются привязки
          //  kernel.Bind<IStoreRepository>().To<StoreRepository>();
            kernel.Bind<IEntitiesDbContext>().To<NorthwindDbContext>().WithConstructorArgument("name=Northwind");
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}