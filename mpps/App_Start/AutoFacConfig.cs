﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using mpps.domain;

namespace mpps
{
    public class AutoFacConfig
    {
        public static void Register()
        {
            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register other dependencies.
            builder.Register(c => new CyberSourcePaymentToken()).As<IPaymentTokenDomain>().InstancePerApiRequest();
            builder.Register(c => new CyberSourceCheckOutDomain()).As<ICheckOutDomain>().InstancePerApiRequest();
            builder.Register(c => new ProfileDomain()).As<IProfileDomain>().InstancePerApiRequest();

            // Build the container.
            var container = builder.Build();

            // Create the depenedency resolver.
            var resolver = new AutofacWebApiDependencyResolver(container);

            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}